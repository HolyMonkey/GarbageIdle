using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxing : MonoBehaviour
{
    private float _elepsedTime = 0;
    public const float Delay = 2f; 

    void Update()
    {
        _elepsedTime += Time.deltaTime;
        if (_elepsedTime >= Delay)
            gameObject.SetActive(false);
    }
}
