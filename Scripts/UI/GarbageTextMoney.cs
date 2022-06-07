using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarbageTextMoney : MonoBehaviour
{
    [SerializeField] private Worker _worker;
    [SerializeField] private TMP_Text _textMoney;

    public void SetTextMoney()
    {
        _textMoney.text = _worker.Target.Price.ToString();
    }
}
