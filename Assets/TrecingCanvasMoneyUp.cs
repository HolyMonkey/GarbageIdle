using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrecingCanvasMoneyUp : MonoBehaviour
{
     private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 targetDistance = _camera.transform.position - transform.position;

        transform.forward = -targetDistance.normalized;

        Vector3 euler = transform.eulerAngles;
        euler.z = 0;

        transform.eulerAngles = euler;
    }
}
