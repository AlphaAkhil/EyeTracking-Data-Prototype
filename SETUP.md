# Setup Guide

Follow the steps below to set up and run the **Eye Tracking Data Prototype**.

## Requirements

* Unity **21.1+**
* HTC VIVE Focus Vision
* XR Interaction Toolkit
* VIVE OpenXR Plugin

---

## 1. Download and Open the Project

Download or clone this repository and open the project using **Unity 21.1 or newer**.

Open the project through **Unity Hub** and allow Unity to finish importing and compiling the project.

---

## 2. Install XR Interaction Toolkit

Open:

**Window → Package Manager**

Search for:

**XR Interaction Toolkit**

Install the package into the project.

---

## 3. Add VIVE OpenXR Scoped Registry

Go to:

**Edit → Project Settings → Package Manager**

Under **Scoped Registries**, create a new registry with the following settings:

| Setting  | Value                           |
| -------- | ------------------------------- |
| Name     | `Vive OpenXR`                   |
| URL      | `https://npm-registry.vive.com` |
| Scope(s) | `com.htc`                       |

Click **Apply**.

### Scoped Registry Setup

![VIVE OpenXR Scoped Registry](Images/vive-scoped-registry.png)

> Replace the image path above with the screenshot included in this repository.

---

## 4. Install VIVE OpenXR Plugin

Open:

**Window → Package Manager**

Change the package source to:

**My Registries**

Find:

**VIVE OpenXR Plugin**

and click **Install**.

---

## 5. Install the Provided Tarball Package

After installing the VIVE OpenXR Plugin, install the additional package provided with this project.

In **Package Manager**:

1. Click the **+** button.
2. Select **Install package from tarball...**
3. Select the provided `.tgz` tarball file.
4. Wait for Unity to finish installing the package.

Example:

```text
Package Manager
      │
      ▼
     [+]
      │
      ▼
Install package from tarball...
      │
      ▼
Provided .tgz file
```

### Tarball Installation

![Install Tarball](Images/install-tarball.png)

> Replace the image path above with the screenshot included in this repository.

---

## 6. Configure OpenXR

Once the required packages are installed, open:

**Edit → Project Settings → XR Plug-in Management**

Make sure **OpenXR** is enabled for the target platform.

---

## 7. Configure OpenXR Interaction Profiles

Go to the OpenXR settings:

**Project Settings → XR Plug-in Management → OpenXR**

Under **Interaction Profiles**, add the required VIVE controller profile:

**VIVE Controller**

Make sure the required controller profile is enabled for the headset.

---

## 8. Enable VIVE XR Support Feature

In the OpenXR settings, check the available **VIVE XR Support** feature.

Enable:

**VIVE XR Support**

This enables the required VIVE-specific OpenXR functionality used by the prototype.

---

## 9. Final Setup

After completing the steps above, the project should be ready to run.

The setup flow is:

```text
Download Project
      │
      ▼
Open in Unity 21.1+
      │
      ▼
Install XR Interaction Toolkit
      │
      ▼
Add VIVE OpenXR Scoped Registry
      │
      ▼
Install VIVE OpenXR Plugin
      │
      ▼
Install Provided .tgz Package
      │
      ▼
Enable OpenXR
      │
      ▼
Add VIVE Controller Interaction Profile
      │
      ▼
Enable VIVE XR Support
      │
      ▼
Ready to Run
```

## Notes

The project requires the VIVE OpenXR packages and configuration described above.

Make sure the required VIVE software, headset connection, and OpenXR runtime are correctly configured before running the project.
