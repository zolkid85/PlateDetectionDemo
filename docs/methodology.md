# Methodology

## Pipeline Architecture

```
Input Image
    │
    ▼
[ Preprocessing Stage ]
    ├─ Enhanced Histogram Equalization
    ├─ Selective Gamma Correction   (adaptive, based on brightness)
    └─ Adaptive Normalization
    │
    ▼
[ OpenALPR Detection Engine ]
    ├─ Plate localization
    └─ Character recognition (pre-trained US plate template)
    │
    ▼
[ Post-processing ]
    ├─ Cropping of detected plate region
    └─ Confidence scoring
    │
    ▼
Output: Plate text + confidence + cropped region
```

## Preprocessing Techniques

- **Enhanced Histogram Equalization** — redistributes intensity values to
  improve local contrast on low-contrast plates.
- **Selective Gamma Correction** — applies gamma correction conditionally,
  based on measured image brightness, to avoid over-correcting already
  well-exposed images.
- **Adaptive Normalization** — normalizes pixel intensity ranges per-image
  rather than with a fixed global constant.

## Evaluation Protocol

- **Engine:** OpenALPR (baseline, unmodified) used as the recognition
  backend across all configurations — only the preprocessing stage varies.
- **Metrics:** Accuracy, Precision, Recall (see
  [`results/experiments-summary.md`](../results/experiments-summary.md)).
- **Comparison basis:** Each preprocessing configuration was evaluated on
  the same image set to isolate its individual contribution.

## Tools & Stack

- C# (.NET Framework 4.8), Windows Forms (demo GUI)
- OpenALPR SDK for .NET
- OpenCV (preprocessing operations)

## Limitations

- The demo application in this repository runs the baseline OpenALPR
  detection/GUI flow; the enhanced preprocessing pipeline described above
  was developed and evaluated as part of the research phase.
- The evaluation dataset is not included in this repository (see
  [`results/experiments-summary.md`](../results/experiments-summary.md)
  for dataset size and result summary).
