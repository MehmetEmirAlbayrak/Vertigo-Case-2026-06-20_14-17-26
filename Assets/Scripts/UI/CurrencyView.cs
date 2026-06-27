using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] CurrencyWallet wallet;

    [SerializeField] TextMeshProUGUI currencyText;

    [SerializeField] Image currencyIcon;


    private void Start()
    {
        currencyText.text = wallet.Current.ToString();
    }

    private void OnEnable()
    {
        wallet.OnChanged += Refresh;
        Refresh();
    }


    private void OnDisable()
    {
        wallet.OnChanged -= Refresh;
    }

    private void Refresh()
    {
        currencyText.text = wallet.Current.ToString();
    }

}
