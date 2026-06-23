using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    [SerializeField] private WheelController wheel;

    [SerializeField] private RewardWallet wallet;

    [SerializeField] private ZoneManager zoneManager;

    private void OnRewardWon(Reward reward)
    {
        if (reward == null) return;


        if (reward.rewardType == RewardType.Bomb)
        {
            wallet.ResetRewards();
            zoneManager.ResetToFirstZone();
        }

        else
        {
            int amount = RewardScaler.ScaleReward(reward.baseAmount, zoneManager.CurrentZone);
            wallet.AddReward(reward,amount);
            zoneManager.AdvanceToNextZone();
        }
    }


    private void OnEnable()
    {
        wheel.OnSpinCompleted += OnRewardWon;
        print("GameLoopController enabled and subscribed to wheel events.");
        print("Current rewards in wallet: " + wallet.GetRewards());
    }

    private void OnDisable()
    {
        wheel.OnSpinCompleted -= OnRewardWon;
    }
}
