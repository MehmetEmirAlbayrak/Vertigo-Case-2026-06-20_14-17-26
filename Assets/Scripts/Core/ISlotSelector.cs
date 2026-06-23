using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotSelector
{
    public int Select(IReadOnlyList<float> weights);

}
