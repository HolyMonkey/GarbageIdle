using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SizeChanger : MonoBehaviour
{
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato;
    [SerializeField] private float _randomness;
    [SerializeField] private float _duration;

    public void OnButtonClick()
    {
        transform.DOShakeScale(_duration, _strength, _vibrato, _randomness);
    }
}
