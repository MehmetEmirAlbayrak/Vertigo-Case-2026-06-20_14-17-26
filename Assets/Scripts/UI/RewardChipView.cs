using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardChipView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;

    public void Setup(CollectedReward entry)
    {
        icon.sprite = entry.reward.icon;

        if (entry.reward.rewardType == RewardType.Currency)
            amountText.text = entry.amount.ToString();
        else
            amountText.text = "x" + entry.amount;
    }
}
