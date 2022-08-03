using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigTapping : MonoBehaviour
{
    [SerializeField] private Image _circle;
    [SerializeField] private Image _finger;
    [SerializeField] private Animator _animatorClick;
    [SerializeField] private Image _circleBig;
    [SerializeField] private Image _fingerBig;
    [SerializeField] private Animator _animatorBigClick;

    private float _elepsedTime = 0;
    private bool _ready = false;

    private const string AnimatorBigCircle = "BigClick";
    private const string AnimatorCircleIdle = "Idle";

    private void Start()
    {
        _circle.enabled = false;
        _finger.enabled = false;
        _circleBig.enabled = false;
        _fingerBig.enabled = false;
    }

    private void Update()
    {
        if (_ready)
        {
            _elepsedTime += Time.deltaTime;
            EnableTapping();
        }
        else
            return;
    }

    public void EnableTapping()
    {
        if (_elepsedTime >= 0.5f)
        {
            InvisibleBig();
            Visible();
            _animatorClick.SetTrigger(AnimatorBigCircle);
            CheckingTime();
        }
    }

    public void BigEnableTapping()
    {
        if (_circleBig.enabled == false)
        {
            VisibleBig();
            _animatorBigClick.SetTrigger(AnimatorBigCircle);
            _ready = true;
        }
        else
        {
            _elepsedTime = 1;
            InvisibleBig();
            BigEnableTapping();
        }
    }

    public void DisableTapping()
    {
        Invisible();
        _ready = false;
        _elepsedTime = 0;
    }

    private void Visible()
    {
        _circle.enabled = true;
        _finger.enabled = true;
    }
    private void VisibleBig()
    {
        _circleBig.enabled = true;
        _fingerBig.enabled = true;
    }

    private void Invisible()
    {
        _animatorClick.SetTrigger(AnimatorCircleIdle);
        _circle.enabled = false;
        _finger.enabled = false;
    }

    private void InvisibleBig()
    {
        _animatorBigClick.SetTrigger(AnimatorCircleIdle);
        _circleBig.enabled = false;
        _fingerBig.enabled = false;
    }

    private void CheckingTime()
    {
        if (_elepsedTime >= 0.7f)
        {
            DisableTapping();
        }
    }
}
