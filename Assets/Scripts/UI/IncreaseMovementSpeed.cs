using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BigTapping))]
public class IncreaseMovementSpeed : MonoBehaviour
{
    private BigTapping _bigTapping;

    private List<Worker> _workers = new List<Worker>();

    private void Start()
    {
        gameObject.SetActive(true);
        _bigTapping = GetComponent<BigTapping>();
    }

    public void EnableAccelerations()
    {
        foreach (var item in _workers)
        {
            item.Movement.AccelerationEmployee();
        }
        _bigTapping.BigEnableTapping();
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
    }

    public void DisableButton()
    {
        gameObject.SetActive(false);
    }
}
