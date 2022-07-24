# Simple Topdown Camera Controller for Unity
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/LICENSE.md)

A simple topdown camera controller made for Unity that works with minimum setup and no programming. Perfect for beginners.
## Disclaimer
This project was made as a study case. Feel free to leave your suggestions for enhancing it. It's not guaranteed it was made using Unity best practices or it doesn't have critical flaws.

## Table of content:
- [Features](#features)
  - [Camera](#camera)
  - [Controller](#controller)
- [Images](#images)
  - [Gameobject Transparency](#gameobject-transparency)
- [How to setup and use](#how-to-setup-and-use)
  - [Camera and Controller](#camera-and-controller)
  - [Animator](#animator)
  - [Basic controllers](#basic-controllers)
- [Troubleshooting](#troubleshooting)
- [Known issues](#known-issues)

## Features
### Camera
- **Customizable Camera attributes:** You can set the speed to camera follow, the distance and rotation of the Target (Player). 
- **Adds transparency when your Player is behind other Gameobjects:** You can set transparency to Gameobjects that are in between your Camera and Target (Player). See images below.
- **No need to position your camera:** The script will automatically set the Camera's position on the Target (Player).
- **Smooth camera movement:** Camera will smoothly follow your Target (Player).
- **Uses default camera:** Uses default Unity's Camera.
- **All public attributes are documented:** All public attributes to customize your Camera has a big description of what it does.

### Controller
- **Customizable Controller attributes:** You can set Player's: walking speed (running speed is the walking speed doubled), the gravity influence and turn speed.
- **Using Unity's Input Axis:** It uses Unity's Input system, movement and rotation are based on Input.Axis of Horizontal and Vertical.
- **Animation management for Walking and Running:** It has the animation management, to simply transition between your idle, walking and running animations.
- **Easily customizable for new features:** Simple and clean code, easily customizable for any use.
- **All public attributes are documented:** All public attributes to customize your Controller has a big description of what it does.

## Images
### Gameobject Transparency
A Cube and a PolyShape opaque:<br/>
[![Opaque Gameobjects](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/features/OpaqueGameobjects.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/features/OpaqueGameobjects.png)<br/>
The same Cube when Player is behind it:<br/>
[![Transparent Cube](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/features/TransparentCube.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/features/TransparentCube.png)<br/>
The same PolyShape when Player is behind it:<br/>
[![Transparent Shape](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/features/TransparentShape.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/features/TransparentShape.png)

## How to setup and use
### Camera and Controller
- Create your Scene.
- Add the `TopDownCamera.cs` script in your Camera Gameobject.<br/>
[![Camera Script Setup](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/CameraScriptSetup.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/CameraScriptSetup.png)
- Create your Player object (in e.g. a Capsule).<br/>
[![Player Creation](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/PlayerCreation.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/PlayerCreation.png)
- Select your Camera object, Drag your Player Object (in Hierarchy) to Camera's Target attribute.<br/>
[![Drag and Drop Player on Camera](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/DragAndDropPlayerOnCamera.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/DragAndDropPlayerOnCamera.png)
- Add the `PlayerController.cs` script on your Player Gameobject.
- Add the following components: `RigidBody`, `CharacterController`, `CapsuleCollider`;
  - Adjust both your `CharacterController` and `CapsuleCollider` with same `Center`, `Radius` and `Height` to fit your Player asset/Capsule. 
  - Check `RigidBody`'s Freeze Rotation on `X`, `Y` and `Z` axis.<br/>
[![Player Setup](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/PlayerSetup.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/PlayerSetup.png)

### Animator
- Create an `AnimatorController`.
- Add an `Animator Component` in your Player Gameobject and drag and drop your created `AnimatorController` into `Controller` field.<br/>
[![Player Animator](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/PlayerAnimator.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/PlayerAnimator.png)
- Open Animator window (if it's not opened already): Window -> Animation -> Animator.
- Drag and Drop your animations (idle, walk and run).
- Right-click the idle animation and `Set as Layer Default State`.
- Right-click idle animation again and `Make Transition`, and Left-Click your Walk animation.
  - Create trasitions for: Walk to Idle, Walk to Running and Running to Walk animations.<br/>
[![Animator Overview](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/AnimatorOverview.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/AnimatorOverview.png)
- On `Parameters` tab, create two Bool values: `isWalking` and `isRunning`<br/>
[![Animator Parameters](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/AnimatorBoolean.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/AnimatorBoolean.png)
- Add the following `conditions` to your transitions:
  - Idle to Walk: `isWalking` -> `true`
  - Walk to Idle: `isWalking` -> `false`
  - Walk to Run: `isRunning` -> `true`
  - Run to Walk: `isRunning` -> `false`
    - Animation condition example:<br/>
[![Animation Condition Example](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/AnimationTransitionConditionExample.png)](https://github.com/matheus-rib/SimpleTopdownCameraControllerForUnity/blob/main/images/setup/AnimationTransitionConditionExample.png)

### Basic controllers
 - WASD: Move
 - Left shift: Run

## Troubleshooting
If you are using URP and getting no shadows from your Transparent Gameobjects, check this link: [Fixing Shadows on Transparent Meterials in URP](https://forum.unity.com/threads/urp-lit-shader-will-not-receive-shadows-while-transparency-is-enabled.790193/#post-6601705).

## Known issues
- Console keeps logging "Look rotation viewing vector is zero" comment, over the Player rotation.