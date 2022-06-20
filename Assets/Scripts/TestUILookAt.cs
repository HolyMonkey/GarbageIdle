using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUILookAt : MonoBehaviour
{
    private void OnValidate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
