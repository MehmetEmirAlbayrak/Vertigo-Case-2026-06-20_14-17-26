using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombPopupView : MonoBehaviour
{
    [SerializeField] private Button giveUpButton;

    [SerializeField] private Button reviveButton;

    [SerializeField] private TextMeshProUGUI reviveCostText;

    public event Action OnGiveUp;

    public event Action OnRevive;

    private void OnValidate()
    {
        if (giveUpButton == null)
        {
            Transform t = transform.Find("ui_button_giveup");
            if (t != null)
                giveUpButton = t.GetComponent<Button>();
        }
        if (reviveButton == null)
        {
            Transform t = transform.Find("ui_button_revive");
            if (t != null)
                reviveButton = t.GetComponent<Button>();
        }
    }

    private void Awake()
    {
        giveUpButton.onClick.AddListener(OnGiveUpClicked);

        reviveButton.onClick.AddListener(OnReviveClicked);

    }

    private void OnGiveUpClicked()
    {
        OnGiveUp?.Invoke();
        gameObject.SetActive(false);

    }

    private void OnReviveClicked()
    {
        OnRevive?.Invoke();
        gameObject.SetActive(false);

    }

    public void SetReviveCost(int cost)
    {
        reviveCostText.text = AmountFormatter.Format(cost);
    }

    public void SetReviveAffordable(bool canAfford)
    {
        reviveButton.interactable = canAfford;   // popup'taki kendi revive button ref'in
    }
}
