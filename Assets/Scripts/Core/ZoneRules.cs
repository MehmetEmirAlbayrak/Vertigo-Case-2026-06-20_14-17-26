using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRules
{

    public static WheelTier GetWheelTierForZone(int zone)
    {
        if(zone % 30  == 0)
            return WheelTier.Golden;


        else if(zone % 5 == 0)
            return WheelTier.Silver;

        else
            return WheelTier.Bronze;
    }
}
