# FPScontroller-Unity
This script enables first-person movement including walking, running, jumping, and crouching. It features smooth mouse look and a stamina system that limits sprinting. The player’s movement is controlled using Unity’s CharacterController, and a UI slider displays stamina in real-time.

A customizable First-Person Controller for Unity using CharacterController. This script provides smooth movement, camera control, crouching, jumping, and a stamina system with UI integration.

---

## Features

- Walking, running, crouching, and jumping
- Stamina system with UI Slider
- Mouse look with adjustable sensitivity
- Uses Unity’s built-in CharacterController
- Cursor is locked and hidden for immersive gameplay

---

## How to Use

1. Attach the `SC_FPSController.cs` script to your Player GameObject.
2. Add a CharacterController component to the Player if not already present.
3. In the Inspector:
   - Assign the Player Camera
   - Assign the Stamina UI Slider
   - Add a Rigidbody component & tick all constraint

---

## Controls

| Action        | Key             |
|---------------|------------------|
| Move          | W A S D         |
| Jump          | Spacebar        |
| Sprint        | Left Shift      |
| Crouch        | C               |
| Look Around   | Mouse Movement  |

---

## Requirements

- Unity 2020 or newer (tested)
- UI Slider element in your canvas for stamina display

---

## Customization

- Set walking, running, and crouching speeds in the Inspector
- Modify stamina drain and regeneration rates
- Adjust mouse sensitivity via PlayerPrefs (default is 1.0)

---

## License

This project is licensed under the MIT License — you are free to use, modify, and distribute it for personal or commercial purposes.


