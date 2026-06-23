using UnityEngine;
using UnityEngine.UI;
using System;                   
using System.Collections;

public class WheelController : MonoBehaviour
{
    [Header("Data (editor-changeable content)")]
    [SerializeField] private WheelConfig config;
    [SerializeField] private Image wheelBase;
    [SerializeField] private Image _indicator;


    [Header("Refs — auto-filled in OnValidate, do NOT drag by hand")]
    [SerializeField] private RectTransform wheel;
    [SerializeField] private Button spinButton;

    [Header("Spin feel")]
    [SerializeField] private int fullSpins = 5;
    [SerializeField] private float spinDuration = 4f;
    [SerializeField] private AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float indicatorAngle = 90f;

    public event Action<Reward> OnSpinCompleted;

    private float[] slotBaseAngles;
    private float currentZ;
    private bool isSpinning;
    private Sprite baseImage;
    private Sprite indicatorImage;
    public bool IsSpinning => isSpinning;

    private void OnValidate()
    {
        if (wheel == null)
        {
            wheel = transform.Find("Wheel") as RectTransform;
        }

        if (spinButton == null)
        {
            spinButton = GetComponentInChildren<Button>(true);
        }
    }

    private void Awake()
    {
        if (spinButton != null)
        {
            spinButton.onClick.AddListener(Spin);
        }
        currentZ = wheel.localEulerAngles.z;

        if (config != null)
            BuildWheel();


    }

    public void Configure(WheelConfig newConfig)
    {
        config = newConfig;
        BuildWheel();
        wheelBase.sprite = newConfig.baseSprite;
        _indicator.sprite = newConfig.indicatorSprite;
    }

    private void BuildWheel()
    {
        slotBaseAngles = new float[config.slots.Count];
        for (int i = 0; i < config.slots.Count; i++)
        {
            Transform c = wheel.GetChild(i);
            slotBaseAngles[i] = Mathf.Atan2(c.localPosition.y, c.localPosition.x) * Mathf.Rad2Deg;
            Image img = c.GetComponent<Image>();
            img.sprite = config.slots[i].reward.icon;
            img.preserveAspect = true;
        }
    }

    public void Spin()
    {

        if (isSpinning || config == null) return;
        int index = PickWeightedIndex();
        StartCoroutine(SpinRoutine(index));
    }

    public int PickWeightedIndex()
    {
        float total = 0f;
        foreach (var slot in config.slots)
            total += slot.weight;

        if (total <= 0f) return 0;

        float r = UnityEngine.Random.Range(0f, total);
        float cum = 0f;
        for (int i = 0; i < config.slots.Count; i++)
        {
            cum += config.slots[i].weight;
            if (r <= cum) return i;

        }
        return config.slots.Count - 1;
    }

    private IEnumerator SpinRoutine(int index)
    {
        isSpinning = true;

        float a_i     = slotBaseAngles[index];
        float delta   = Mathf.Repeat(indicatorAngle - a_i - currentZ, 360f);
        float targetZ = currentZ + 360f * fullSpins + delta;

        //wheel.DOLocalRotate(new Vector3(0, 0, targetZ), spinDuration, RotateMode.FastBeyond360)
        // .SetEase(Ease.OutCubic).OnComplete(() => Finish(index));   // then `yield break;`


        float startZ = currentZ;
        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;

            float k = ease.Evaluate(Mathf.Clamp01(elapsed / spinDuration));
            float z = Mathf.Lerp(startZ, targetZ, k);
            wheel.localEulerAngles = new Vector3(0f, 0f, z);
            yield return null;

        }

        wheel.localEulerAngles = new Vector3(0f, 0f, targetZ);
        currentZ = Mathf.Repeat(targetZ, 360f);
        isSpinning = false;
        OnSpinCompleted?.Invoke(config.slots[index].reward);     
        

        
    }
}
