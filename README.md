# vr-fair
VR Fair experience programmed with Unity, the current implementation contains a shooting stand game

## Startup
Open the the `MainScene.unity` file in the `Assets/Scenes/` folder and start the scene with a connected VR Headset.

## VR Controls
- Teleportation: `Right Trackpad click`
- Interact with UI Elements: A ray will appear if the left hand is aimed at interactable UI Elements. Use `Left Trackpad click` to trigger the interaction.
- Grabbing objects: Interactable objects will give a haptic feedback when hovered over. Toggle `Grip Button` to select/unselect interactable objects
- Trigger object: Grabbed objects (e.g. Pistols) can be triggered via `Trigger` click

## Shooting stand game
The goal of the shooting stand game is to shoot as many targets and get as many points as possible in a limited amount of time. For this the player can use up to two pistols. The game has 3 levels which introduce different types of targets:

- **Level 1:** This level contains normal wooden targets, which only need one hit to fall over
- **Level 2:** This level additionally contains heavy metal targets, which need multiple hits too fall over. Heavy targets give a point multiplier
- **Level 3:** This level additionally contains colored targets, which need to be shot with a pistol of the matching color to give any points
