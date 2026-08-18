# Setup Guide

Follow the steps below to set up and run the **Eye Tracking Data Prototype**.

## Requirements

* Unity **2021.3+**
* HTC VIVE Focus Vision
* XR Interaction Toolkit
* VIVE OpenXR Plugin

---

## 1. Download and Open the Project

Download or clone this repository and open the project using **Unity 2021.3 or newer**.

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

![VIVE OpenXR Scoped Registry](IMAGES/Scope-Registry.png)

---

## 4. Install VIVE OpenXR Plugin

Open:

**Window → Package Manager**

Change the package source to:

**My Registries**

Find:

**VIVE OpenXR Plugin**

and click **Install**.

### VIVE OpenXR Plugin

![VIVE OpenXR Plugin](IMAGES/Vive-Registry.png)

---

## 5. Download and Install VIVE OpenXR Package

Download **VIVE OpenXR 2.5.1** from the official VIVE OpenXR Unity releases:

https://github.com/ViveSoftware/VIVE-OpenXR-Unity/releases

Download the required `.tgz` package.

### Download Package

![VIVE OpenXR Package](IMAGES/Com.htc.vive.png)

After downloading the package, open **Package Manager** in Unity:

1. Click the **+** button.
2. Select **Install package from tarball...**
3. Select the downloaded `.tgz` file.
4. Wait for Unity to finish installing the package.

### Install Tarball

![Install Tarball](IMAGES/tarball.png)

---

## 6. Configure OpenXR

Once the required packages are installed, open:

**Edit → Project Settings → XR Plug-in Management**

Make sure **OpenXR** is enabled for the target platform.

---

## 7. Configure OpenXR Interaction Profiles

Go to:

**Project Settings → XR Plug-in Management → OpenXR**

Under **Interaction Profiles**, add:

**VIVE Controller**

### Enabled Interaction Profiles

![Enabled Interaction Profiles](IMAGES/Enabled-Interaction-Profiles.png)

Make sure the required controller profile is enabled for the headset.

---

## 8. Enable VIVE XR Support

In the OpenXR settings, locate the available **VIVE XR Support** feature.

Enable:

**VIVE XR Support**

### OpenXR Feature Group

![OpenXR Feature Group](IMAGES/OpenXR-Feature-Group.png)

This enables the required VIVE-specific OpenXR functionality used by the prototype.

---

## 9. Final Setup

After completing the steps above, the project should be ready to run.

The setup flow is:

```text
Download Project
      │
      ▼
Open in Unity 2021.3+
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
Download VIVE OpenXR 2.5.1
      │
      ▼
Install .tgz Package
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
