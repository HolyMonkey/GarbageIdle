using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBuffActivator : MonoBehaviour
{
    private Worker[] _workers;

    public void OnButtonClick()
    {
        _workers = FindObjectsOfType<Worker>();

        foreach(var worker in _workers)
        {
            worker.ActivateProfitUpgradeEffect();
        }
    }
}
