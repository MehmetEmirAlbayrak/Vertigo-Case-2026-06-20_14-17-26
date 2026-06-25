using System.Collections.Generic;

public interface ISlotSelector
{
    public int Select(IReadOnlyList<float> weights);

}
