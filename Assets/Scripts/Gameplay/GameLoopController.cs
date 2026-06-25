using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    [SerializeField] private WheelController wheel;

    [SerializeField] private RewardWallet wallet;

    [SerializeField] private ZoneManager zoneManager;

    [SerializeField] private BombController bombController;

    private void OnRewardWon(Reward reward)
    {
        if (reward == null) return;


        if (reward.rewardType == RewardType.Bomb)
        {
            bombController.ShowBombPopup();
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
    }

    private void OnDisable()
    {
        wheel.OnSpinCompleted -= OnRewardWon;
    }
}
