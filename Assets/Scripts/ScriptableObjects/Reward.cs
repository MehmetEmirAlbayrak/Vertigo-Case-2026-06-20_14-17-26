using UnityEngine;
using UnityEngine.UI;

public enum RewardType
{
    Currency,
    Item,
    Multiplier,
    Bomb
}

[CreateAssetMenu(menuName = "Spin/Reward")]
public class Reward : ScriptableObject
{
    public string rewardName;

    public Sprite icon;

    public RewardType rewardType;

    public int baseAmount;
}