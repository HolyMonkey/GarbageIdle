using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tapping : MonoBehaviour
{
    [SerializeField] private Image _circle;
    [SerializeField] private Image _finger;
    [SerializeField] private Animator _animator;

    private float _elepsedTime = 0;

    private const string AnimatorCircle = "Circle";
    private const string AnimatorCircleIdle = "Idle";

    private void Start()
    {
        _circle.enabled = false;
        _finger.enabled = false;
    }

    private void Update()
    {
        if (_circle.enabled)
        {
            _elepsedTime += Time.deltaTime;
            DisableTapping();
        }
        else
            return;
    }

    public void EnableTapping()
    {
        if(_circle.enabled == false)
        {
            _circle.enabled = true;
            _finger.enabled = true;
            _animator.SetTrigger(AnimatorCircle);
        }
        else
        {
            _elepsedTime = 1;
            DisableTapping();
            EnableTapping();
        }
    }

    public void DisableTapping()
    {
        if (_elepsedTime >= 0.4f)
        {
            _animator.SetTrigger(AnimatorCircleIdle);
            _circle.enabled = false;
            _finger.enabled = false;
            _elepsedTime = 0;
        }
    }
}
