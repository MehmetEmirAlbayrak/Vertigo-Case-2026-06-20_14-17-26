using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    private int currentZone;

    [SerializeField] private WheelConfig bronze;
    [SerializeField] private WheelConfig silver;
    [SerializeField] private WheelConfig golden;
    [SerializeField] private WheelController wheel;

    public int CurrentZone => currentZone;

    private void Start()
    {
        currentZone = 1;
        SetupWheelForCurentZone();
    }

    public void AdvanceToNextZone()
    {
        currentZone++;
        SetupWheelForCurentZone();
    }

    public void ResetToFirstZone()
    {
        currentZone = 1;
        SetupWheelForCurentZone();
    }

    private void SetupWheelForCurentZone()
    {
        switch (ZoneRules.GetWheelTierForZone(currentZone))
        {
            case WheelTier.Bronze:
                wheel.Configure(bronze);
                break;
            case WheelTier.Silver:
                wheel.Configure(silver);
                break;
            case WheelTier.Golden:
                wheel.Configure(golden);
                break;
        }

        print("Advanced to Zone " + currentZone + " with " + ZoneRules.GetWheelTierForZone(currentZone) + " wheel.");
    }
}
