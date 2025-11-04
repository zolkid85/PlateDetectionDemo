using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using openalprnet;

namespace AlprNetGuiTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public Rectangle boundingRectangle(List<Point> points)
        {
            if (points == null || !points.Any())
                return Rectangle.Empty;

            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);

            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        public static Bitmap combineImages(List<Image> images)
        {
            if (images == null || images.Count == 0)
                return null;

            Bitmap finalImage = null;
            try
            {
                int width = 0;
                int height = 0;

                foreach (var bmp in images)
                {
                    width += bmp.Width;
                    height = Math.Max(height, bmp.Height);
                }

                finalImage = new Bitmap(width, height);

                using (var g = Graphics.FromImage(finalImage))
                {
                    g.Clear(Color.Black);
                    int offset = 0;
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
                        offset += image.Width;
                    }
                }

                return finalImage;
            }
            finally
            {
                foreach (var image in images)
                {
                    image.Dispose();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                processImageFile(openFileDialog.FileName);
            }
        }

        private void processImageFile(string fileName)
        {
            resetControls();
            var region = rbUSA.Checked ? "us" : "eu";
            string config_file = Path.Combine(AssemblyDirectory, "openalpr.conf");
            string runtime_data_dir = Path.Combine(AssemblyDirectory, "runtime_data");

            using (var alpr = new AlprNet(region, config_file, runtime_data_dir))
            {
                if (!alpr.IsLoaded())
                {
                    lbxPlates.Items.Add("Error initializing OpenALPR");
                    return;
                }

                picOriginal.ImageLocation = fileName;
                picOriginal.Load();

                var results = alpr.Recognize(fileName);
                var images = new List<Image>();
                int i = 1;

                foreach (var result in results.Plates)
                {
                    List<Point> points = new List<Point>();
                    foreach (var c in result.PlatePoints)
                        points.Add(new Point(c.X, c.Y));

                    var rect = boundingRectangle(points);
                    var img = Image.FromFile(fileName);
                    var cropped = cropImage(img, rect);
                    images.Add(cropped);

                    lbxPlates.Items.Add($"\t\t-- Plate #{i++} --");

                    // Utilisation correcte des propriétés de AlprPlateNet
                    foreach (var plate in result.TopNPlates)
                    {
                        lbxPlates.Items.Add(string.Format("{0} {1:N1}% {2}",
                                                          plate.Characters.PadRight(12),
                                                          plate.OverallConfidence,
                                                          plate.MatchesTemplate));
                    }
                }

                if (images.Any())
                {
                    picLicensePlate.Image = combineImages(images);
                }
            }
        }

        private void resetControls()
        {
            picOriginal.Image = null;
            picLicensePlate.Image = null;
            lbxPlates.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resetControls();
        }
    }
}
