using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoneMapView : MonoBehaviour
{
    [SerializeField] private ZoneManager zoneManager;
    [SerializeField] private Transform tileContainer;
    [SerializeField] private GameObject tilePrefab;     
    [SerializeField] private int visibleCount = 5;      

    [Header("Tile sprites by state")]
    [SerializeField] private Sprite currentSprite;
    [SerializeField] private Sprite comingSprite;      
    [SerializeField] private Sprite safeSprite;         
    [SerializeField] private Sprite superSprite;        

    private void OnEnable()
    {
        zoneManager.OnZoneChanged += Rebuild;
    }

    private void OnDisable()
    {
        zoneManager.OnZoneChanged -= Rebuild;
    }

    private void Rebuild(int currentZone)
    {
        foreach (Transform child in tileContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < visibleCount; i++)
        {
            int zone = currentZone + i;
            GameObject tile = Instantiate(tilePrefab, tileContainer);
            tile.GetComponent<Image>().sprite = SpriteForZone(zone, i == 0);
            tile.GetComponentInChildren<TextMeshProUGUI>().text = "Zone:" + zone.ToString();
        }
    }

    private Sprite SpriteForZone(int zone, bool isCurrent)
    {
        if (isCurrent)
            return currentSprite;

        switch (ZoneRules.GetWheelTierForZone(zone))
        {
            case WheelTier.Golden: return superSprite;
            case WheelTier.Silver: return safeSprite;
            default:               return comingSprite;
        }
    }
}
