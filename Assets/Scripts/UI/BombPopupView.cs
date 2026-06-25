using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombPopupView : MonoBehaviour
{
    [SerializeField] private Button giveUpButton;

    [SerializeField] private Button reviveButton;

    public event Action OnGiveUp;

    public event Action OnRevive;

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
}
