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
        else if (money > 10000)
            text.text = (money / 10000).ToString("f") + "B";
        else
            text.text = money.ToString();

        return text;
    }
}
