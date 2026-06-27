using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wheel_", menuName = "Spin/Wheel Config")]
public class WheelConfig : ScriptableObject
{
    public WheelTier tier;

    public List<WheelSlot> slots;

    public Sprite baseSprite;

    public Sprite indicatorSprite;
}
