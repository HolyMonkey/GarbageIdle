using System;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    //[SerializeField] private Character _character;

    //private void OnEnable() => _character.ChangeMoney += OnChangeMoney;

    //private void OnDisable() => _character.ChangeMoney -= OnChangeMoney;

    private void OnChangeMoney(float money)
    {

        float value = (float)Math.Round(money, 1);
        _value.text = value.ToString() + "$";
    }
}
