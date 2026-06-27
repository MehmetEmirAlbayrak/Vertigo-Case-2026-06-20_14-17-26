using System;
using System.Collections.Generic;

public class WeightedSlotSelector : ISlotSelector
{

    private readonly Func<float> _rng;

    public WeightedSlotSelector(Func<float> rng)
    {

       _rng = rng;
    }

    public int Select(IReadOnlyList<float> weights)
    {
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        if(totalWeight <= 0f)
            return 0;

        float randomValue = _rng() * totalWeight;
        float cumulativeWeight = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }
        // Fallback in case of rounding errors
        return weights.Count - 1;
    }

}
