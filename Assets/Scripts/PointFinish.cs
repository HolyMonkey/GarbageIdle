using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointFinish : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMoney;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Worker worker))
        {
            _textMoney.alpha = Mathf.Lerp(0, 1, 0.5f);
            _textMoney.text ="+" + worker.Target.Price.ToString();
            _textMoney.alpha = Mathf.Lerp(1, 0, 0.5f);
        }
    }
}
