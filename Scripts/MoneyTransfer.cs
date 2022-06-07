using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTransfer : MonoBehaviour
{
    public TMP_Text CurrencyConversion(float money,TMP_Text text)
    {
        if (money > 1000)
            text.text = (money / 1000).ToString("F") + "K";
        else
            text.text = money.ToString();

        return text;
    }
}
