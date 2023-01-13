# Foundry Open Photon PUN example

## MIT Reality Hack
If you're at the MIT Reality Hack, we're here to help mentor and guide you with your project. Find us in person, on Discord - channel is #Foundry, or text Daniel at 415-690-9681

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. Then you can run the actual app by deploying to an APK for Oculus or an exe for Windows PCVR.

## Prerequisites

What things you need to install, the software, and how to install them.

You will want to fork this Github repo so that any future updates to the SDK, we can push to your project through this repo.
More info on forking:
https://docs.github.com/en/get-started/quickstart/fork-a-repo

```
Unity Version 2021.3.11f1
OpenXR
XR Interaction Toolkit (preferably 2.3)
Input system

Photon API keys for Photon PUN and Photon Voice (both FREE)
YOU MUST GO to the following links to generate your own API Keys and use them to build your own networked app (so as not to conflict with other apps):
https://www.photonengine.com/en/PUN#
https://www.photonengine.com/en-us/Voice

Add your keys to /Assets/Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset
```

## Basic Controls
1. Once you have PUN setup, you can connect a VR headset to the PC and hit play in Editor.
2. Click Connect button in VR, you will connect to the Online scene.
3. Once in the scene, you can connect with other players if they are in the same PUN server.
4. You can walk around with left joystick, rotate with right joystick.
5. You can grab, grabbable objects (weapons) in the scene using the Grab button in VR.
6. Press trigger to shoot.
7. Select Menu items on the left wrist with the right controller and pointing at it and pressing trigger to select.

## Deployment
If your headset is connected, you can test in the Unity editor and it should work, even networked. Two people testing on two computers should be able to connect.
You can build to an exe for Windows/SteamVR or you can push to an Oculus APK.

## Photon PUN Scene Management
You need to have the scenes you want added in the build settings
Photon works by going from an Offline lobby to an Online scene. By default we have scenes named: Offline and Online. In the Network Manager Script - NM - you control the offline and online scene by setting the scene index. 

## Ready Player Me
By default you have a Ready Player Me default avatar rigged correctly. 
You can swap in your own Ready Player Me avatars by downloading and adding, or while running in PCVR you can even copy paste the URL of your Ready Player Me avatar. You can paste the RPM avatar URL on the PC monitor screen if connected through PC.
Please make a RPM avatar at this link:
https://vr.readyplayer.me/avatar
