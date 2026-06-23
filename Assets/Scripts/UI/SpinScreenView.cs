using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpinScreenView : MonoBehaviour
{
    [SerializeField] private RewardWallet wallet;

    [SerializeField] private ZoneManager zoneManager;

    [SerializeField] private WheelController wheel;

    [SerializeField] private GameObject rewardPanel;

    [SerializeField] private GameObject rewardPrefab;

    [SerializeField] private Button leaveButton;

    [SerializeField] private TextMeshProUGUI zoneText;



    private void OnValidate()
    {
        if (rewardPanel == null)
            rewardPanel = transform.Find("RewardPanel").gameObject;
        if (rewardPrefab == null)
            rewardPrefab = Resources.Load<GameObject>("Prefabs/Reward");
        if (zoneText == null)
            zoneText = transform.Find("Zone_Text").GetComponent<TextMeshProUGUI>();
        if (leaveButton == null)
            leaveButton = transform.Find("Leave_Button").GetComponent<Button>();

    }


    private void Awake()
    {
        if(leaveButton != null)
        {
            leaveButton.onClick.AddListener(OnLeaveClicked);
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

            reward.GetComponentInChildren<TextMeshProUGUI>().text = "x" + rew.amount.ToString();

            reward.GetComponentInChildren<Image>().sprite = rew.reward.icon;
        }


    }

    private void OnSpinCompleted(Reward reward)
    {
        if (reward == null)
            return;

        

    }

    private void OnEnable()
    {
        wallet.OnChanged += OnChanged;
    }

    private void OnDisable()
    {
        wallet.OnChanged -= OnChanged;
    }

    private void OnLeaveClicked()
    {
        print("Leave button clicked. Implement leaving logic here.");
    }
}
