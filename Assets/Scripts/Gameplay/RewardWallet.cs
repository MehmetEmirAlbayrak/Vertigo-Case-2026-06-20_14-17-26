using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollectedReward
{
    public Reward reward;

    public int amount = 0;
}

public class RewardWallet : MonoBehaviour
{

    public event Action OnChanged;
    
    
    public List<CollectedReward> entries;

    public void AddReward(Reward reward, int amount)
    {
        CollectedReward entry = FindEntry(reward);
        if (entry != null)
        {
            entry.amount += amount;
        }
        else
        {
            entries.Add(new CollectedReward { reward = reward, amount = amount });
        }

        OnChanged?.Invoke();
    }

    public void ResetRewards()
    {
        entries.Clear();
        OnChanged?.Invoke();
    }

    public IReadOnlyList<CollectedReward> GetRewards()
    {
        return entries;
    }


    private CollectedReward FindEntry(Reward reward)
    {
        return entries.Find(w => w.reward == reward);
    }

}
