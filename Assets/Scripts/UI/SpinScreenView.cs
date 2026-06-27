using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpinScreenView : MonoBehaviour
{
    [SerializeField] private RewardWallet wallet;

    [SerializeField] private ZoneManager zoneManager;

    [SerializeField] private WheelController wheel;

    [SerializeField] private GameObject rewardPanel;

    [SerializeField] private GameObject rewardPrefab;

    [SerializeField] private Button leaveButton;

    public event Action OnLeaveRequested;

    private void OnValidate()
    {
        if (leaveButton == null)
        {
            Transform t = transform.Find("ui_button_leave");
            if (t != null)
                leaveButton = t.GetComponent<Button>();
        }
    }

    private void Awake()
    {
        if (leaveButton != null)
        {
            leaveButton.onClick.AddListener(LeaveClicked);
        }

    }

    private void OnChanged()
    {
        foreach(Transform child in rewardPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var rew in wallet.GetRewards())
        {
            var reward = Instantiate(rewardPrefab, rewardPanel.transform);
            reward.GetComponentInChildren<TextMeshProUGUI>().text = "x" + AmountFormatter.Format(rew.amount);
            reward.GetComponentInChildren<Image>().sprite = rew.reward.icon;
        }
    }

   
    private void OnSpinStarted()
    {
        leaveButton.gameObject.SetActive(false);
    }

    private void OnZoneChanged(int zone)
    {
        leaveButton.gameObject.SetActive(ZoneRules.GetWheelTierForZone(zone) != WheelTier.Bronze);
    }

    private void OnEnable()
    {
        wheel.OnSpinStarted += OnSpinStarted;
        wallet.OnChanged += OnChanged;
        zoneManager.OnZoneChanged += OnZoneChanged;
    }

    private void OnDisable()
    {
        wheel.OnSpinStarted -= OnSpinStarted;
        wallet.OnChanged -= OnChanged;
        zoneManager.OnZoneChanged -= OnZoneChanged;
    }

    public void LeaveClicked()
    {
        OnLeaveRequested?.Invoke();
    }

}
