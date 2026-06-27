using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    [SerializeField] private WheelController wheel;

    [SerializeField] private RewardWallet wallet;

    [SerializeField] private ZoneManager zoneManager;

    [SerializeField] private BombController bombController;

    [SerializeField] private SpinScreenView spinScreen;

    [SerializeField] private CurrencyWallet currency;

    [SerializeField] private Reward goldReward;

    private void OnRewardWon(Reward reward, int baseAmount)
    {
        if (reward == null) return;


        if (reward.rewardType == RewardType.Bomb)
        {
            bombController.ShowBombPopup();
        }

        else
        {
            int amount = RewardScaler.ScaleReward(baseAmount, zoneManager.CurrentZone);
            wallet.AddReward(reward,amount);
            zoneManager.AdvanceToNextZone();
        }
    }


    private void Collect()
    {
        foreach (var entry in wallet.GetRewards())
        {
            if (entry.reward == goldReward)
                currency.Add(entry.amount);
        }

        wallet.ResetRewards();
        zoneManager.ResetToFirstZone();
    }

    private void OnEnable()
    {
        wheel.OnSpinCompleted += OnRewardWon;
        spinScreen.OnLeaveRequested += Collect;
    }

    private void OnDisable()
    {
        wheel.OnSpinCompleted -= OnRewardWon;
        spinScreen.OnLeaveRequested -= Collect;
    }
}
