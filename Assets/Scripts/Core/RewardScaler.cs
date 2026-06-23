using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScaler
{
   public static int ScaleReward(int baseReward, int zone)
    {
        if (zone <= 1)
            return baseReward;
        float scaleFactor = 1 + (0.25f * (zone - 1));
        return Mathf.RoundToInt(baseReward * scaleFactor);
    }
}
