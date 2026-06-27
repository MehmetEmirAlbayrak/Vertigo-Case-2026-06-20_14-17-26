using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject bombPanel;

    [SerializeField] private BombPopupView bombPopupView;

    [SerializeField] private ZoneManager zoneManager;

    [SerializeField] private RewardWallet wallet;

    [SerializeField] private CurrencyWallet currency;

    [SerializeField] private int reviveCost;

    private void HandleGiveUp()
    {
        wallet.ResetRewards();
        zoneManager.ResetToFirstZone();
    }

    private void HandleRevive()
    {
        currency.TrySpend(reviveCost);
    }

    private void OnEnable()
    {
        bombPopupView.OnGiveUp += HandleGiveUp;
        bombPopupView.OnRevive += HandleRevive;
    }

    private void OnDisable()
    {
        bombPopupView.OnGiveUp -= HandleGiveUp;
        bombPopupView.OnRevive -= HandleRevive;
    }

    public void ShowBombPopup()
    {
        bombPanel.SetActive(true);
        bombPopupView.SetReviveCost(reviveCost);
        bombPopupView.SetReviveAffordable(currency.Current >= reviveCost);
    }

   
}
