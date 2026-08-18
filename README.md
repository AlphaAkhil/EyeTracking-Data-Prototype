# Eye Tracking Data Prototype

A Unity XR prototype created to demonstrate **real-time eye-tracking data collection, gaze analysis, and CSV-based data recording** in an interactive XR environment.

## Overview

The prototype demonstrates how eye-tracking information can be captured, processed, analyzed, and recorded during an XR experience.

The system focuses on collecting and processing data such as:

* Left and right eye rotation
* Pupil diameter
* Eye openness
* Gaze direction
* Gaze position
* Distance from viewed objects
* Area of Interest (AOI) tracking
* CSV-based data recording

## Prototype Purpose

The goal of this project is to demonstrate the technical concepts involved in building an **XR eye-tracking data collection pipeline**.

Rather than recreating a production system, this prototype focuses on showcasing the core workflow:

**Eye Tracker → Data Collection → Gaze Analysis → AOI Detection → Data Recording**

## Technology

* **Unity**
* **C#**
* **XR Interaction Toolkit**
* **OpenXR**
* **VIVE OpenXR Eye Tracker**
* **HTC VIVE Focus Vision**
* **CSV Data Storage**

## Data Pipeline

```text
XR Eye Tracker
      │
      ▼
Eye Data Acquisition
      │
      ├── Eye Rotation
      ├── Eye Openness
      ├── Pupil Diameter
      └── Gaze Position
      │
      ▼
Gaze Analysis
      │
      ├── Object Detection
      ├── Distance Calculation
      └── AOI Detection
      │
      ▼
CSV Data Recording
```

## Eye Tracking

The prototype uses eye-tracking information from the XR device to determine where the user is looking.

Both eyes can be evaluated independently to analyze their gaze information and determine the relevant viewed object.

For example:

```text
Left Eye  → Car
Right Eye → Road

Current AOI → None
```

When both eyes are sufficiently aligned toward the same object, that object can be identified as the current **Area of Interest (AOI)**.

## Data Recording

Collected eye-tracking and gaze information can be recorded into CSV format for later analysis.

Example data fields include:

| Data                 | Description                             |
| -------------------- | --------------------------------------- |
| Timestamp            | Time of data capture                    |
| Left Eye Rotation    | Left eye orientation                    |
| Right Eye Rotation   | Right eye orientation                   |
| Left Pupil Diameter  | Left pupil measurement                  |
| Right Pupil Diameter | Right pupil measurement                 |
| Left Eye Openness    | Left eye openness value                 |
| Right Eye Openness   | Right eye openness value                |
| Object Name          | Currently detected object               |
| Object ID            | Identifier of the detected object       |
| Object Distance      | Distance between gaze origin and object |
| Previous AOI         | Previously detected area of interest    |
| Current AOI          | Currently detected area of interest     |

## Screenshots / Demo

Add screenshots, GIFs, or a video demonstration of the prototype here.

Example:

```text
[Eye Tracking / Gaze Detection]

[AOI Detection]

[Data Collection]
```

## Project Structure

```text
Assets/
├── Prefabs/
├── Scenes/
├── Materials/
└── Scripts/
```

## License

This project is licensed under the **MIT License**.
