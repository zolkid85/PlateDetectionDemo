# License Plate Detection Demo (C# + OpenALPR)

A minimal yet functional Windows Forms application for vehicle license plate detection using OpenALPR and C# (.NET Framework 4.8).

This project is part of my master's thesis:  
“License Plate Detection and Enhancement” – Üsküdar University (Supervised by Dr. Ihab Elaff)

---

## 🧩 Overview

This is the minimal version of my license plate detection system.  
It demonstrates the essential pipeline of:

- Loading and processing images using OpenALPR
- Detecting and displaying license plates
- Cropping detected regions and visualizing them
- Supporting both US and EU plate formats

---

## 🚀 Features

✅ Detects license plates from static images  
✅ Displays recognition confidence and template matching  
✅ Crops and visualizes detected plates  
✅ Works with OpenALPR configuration and runtime data  
⚙️ Clean Windows Forms GUI for demonstration  

---

## 🔬 Advanced Version (Research Extension)

A more advanced version of this system was developed as part of my research. It includes:

- 📁 Batch processing: Iterates over all images in a folder  
- 🧠 Performance metrics: Computes precision, recall, and F1-score  
- 🧮 Enhanced image preprocessing:  
  - Enhanced Histogram Equalization  
  - Selective Gamma Correction  
  - Adaptive Normalization  
- 📈 Performance gain: Achieved **90.47% accuracy**,  
  which is +3% improvement over the baseline OpenALPR system.

---

## ⚙️ Requirements

- Windows 10 or later  
- Visual Studio 2022  
- .NET Framework 4.8  
- OpenALPR SDK for .NET  

---



## 📂 Project Structure


PlateDetectionDemo/
│
├─ AlprNetGuiTest.csproj
├─ plates.sln
├─ Form1.cs
├─ Form1.Designer.cs
├─ Program.cs
├─ App.config
├─ config.yaml # Configuration file for OpenALPR
├─ LICENSE
├─ README.md
├─ Properties/
├─ bin/ # Output binaries (ignored in Git)
├─ obj/ # Temporary files (ignored in Git)
