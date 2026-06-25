using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZoneManager : MonoBehaviour
{
    private int currentZone;

    [SerializeField] private WheelConfig bronze;
    [SerializeField] private WheelConfig silver;
    [SerializeField] private WheelConfig golden;
    [SerializeField] private WheelController wheel;

    public event Action<int> OnZoneChanged;

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
                wheel.Configure(bronze,currentZone);
                break;
            case WheelTier.Silver:
                wheel.Configure(silver,currentZone);
                break;
            case WheelTier.Golden:
                wheel.Configure(golden, currentZone);
                break;
        }

        OnZoneChanged?.Invoke(currentZone);
    }

}
