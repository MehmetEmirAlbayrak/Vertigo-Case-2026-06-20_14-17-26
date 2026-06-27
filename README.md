# Wheel of fortune — Vertigo Games Game Developer case.

Demo: https://youtu.be/HBo9K56-Ga0

![16:9](Screenshots/16_9.png)
![20:9](Screenshots/20_9.png)
![4:3](Screenshots/4_3.png)

## How to run
- APK: see the **Releases** tab
- Or open the project in Unity 2021 LTS

## Features
- Spin the wheel to earn rewards. But be careful, you can lose everything you won if you find a bomb.
- In safe or super zones, rewards are bigger and you can collect the items you won.
- In the editor you can change rewards, reward amounts, and weights.

## Architecture
- SOLID + event-driven decoupling (views fire events, controllers handle).
- Testability: WeightedSlotSelector takes a Func<float> for the RNG, which makes it easy to test.
- ScriptableObject: Wheels and rewards are driven from SO. Creating a new wheel or reward is easy. You configure a reward's sprite, name, and type; and a wheel's icon, reward weights, and amounts.
- The Core is isolated in its own asmdef and tested with NUnit.


Tests: "Window → General →Test Runner → EditMode → Run All"


Tech: Unity 2021 LTS, TextMeshPro, Sprite Atlas.