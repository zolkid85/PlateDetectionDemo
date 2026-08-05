# Thesis Summary — License Plate Detection and Enhancement

**Author:** Mahamadou Allachi Boukar Allachi
**Program:** M.Sc. in Computer Engineering, Üsküdar University, Istanbul
**Supervisor:** Dr. Ihab Elaff

## Problem

Automatic License Plate Recognition (ALPR) systems degrade significantly under
non-ideal image conditions — low contrast, uneven illumination, motion blur,
low resolution, and varying plate formats. This thesis investigates
**image enhancement techniques applied upstream of an ALPR engine (OpenALPR)**
to improve detection and recognition accuracy without retraining the
underlying recognition model.

## Objective

Design and evaluate a preprocessing pipeline that improves plate detection
accuracy on degraded images, and quantify the gain against a standard
OpenALPR baseline.

## Approach

1. **Baseline** — raw images fed directly into OpenALPR.
2. **Normalization** — pixel intensity normalization applied before detection.
3. **Selective Gamma Correction + Normalization** — an adaptive gamma
   correction step (applied selectively based on image brightness
   statistics) combined with normalization, intended to correct
   under/over-exposed plates before detection.

Each stage was implemented in C# (with OpenCV bindings) as a preprocessing
module ahead of the OpenALPR recognition call, and evaluated on the same
image set to isolate the effect of preprocessing from the recognition engine
itself.

## Key Result

The combined **Selective Gamma Correction + Normalization** pipeline improved
detection accuracy from **87.21% (baseline) to 90.47%**, a **+3.26 point**
gain, while maintaining high precision (96.60%) and recall (93.43%).
See [`results/experiments-summary.md`](../results/experiments-summary.md)
for the full comparison table.

## Status

The thesis was successfully defended and validated. This repository
contains a demonstration build of the system (Windows Forms GUI) plus the
documentation and results summarized here; the full experimental
dataset is not redistributed in this repository.
