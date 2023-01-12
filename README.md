# Foundry Open Photon PUN example

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. Then you can run the actual app by deploying to an APK for Oculus or an exe for Windows PCR.

## Prerequisites

What things you need to install the software and how to install them.

```
Unity Version 2021.3.11f1,
OpenXR,
XR Interaction Toolkit (preferably 2.3),
Input system,
Photon API keys
```

## Deployment
You can test in the Unity editor and it will work, even networked. Two people testing on two computers can connect.
You can build to an exe for Windows/SteamVR or you can push to an Oculus APK

## Photon PUN Scene Management
You need to have the scenes you want added in the build settings
Photon works by going from an Offline lobby to an Online scene. By default we have scenes named: Offline and Online. In the Network Manager Script - NM - you control the offline and online scene by setting the scene index. 

## Ready Player Me
By default you have a Ready Player Me default avatar rigged correctly. 
You can swap in your own Ready Player Me avatars by downloading and adding, or while running in PCVR you can even copy paste the URL of your Ready Player Me avatar. 
