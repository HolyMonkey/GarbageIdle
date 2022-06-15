using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tapping))]
public class IncreaseMovementSpeed : MonoBehaviour
{
    private Tapping _tapping;

    private List<Worker> _workers = new List<Worker>();

    private void Start()
    {
        _tapping = GetComponent<Tapping>();
    }

    public void EnableAccelerations()
    {
        foreach (var item in _workers)
        {
            item.Movement.AccelerationEmployee();
        }
        _tapping.EnableTapping();
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
    }
}
