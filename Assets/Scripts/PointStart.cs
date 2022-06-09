using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStart : MonoBehaviour
{
    private int _count = 100;

    private List<Garbage> _garbage = new List<Garbage>();

    public int Count => _count;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CountGarbage"))
        {
            _count = PlayerPrefs.GetInt("CountGarbage");
        }
    }

    private void Update()
    {
        ClearList();
    }

    public void AddListGarbage(Garbage garbage)
    {
        _garbage.Add(garbage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Worker worker))
        {
            worker.ListGarbage(_garbage);
        }
    }

    private void ClearList()
    {
        for (int i = 0; i < _garbage.Count; i++)
        {
            if (_garbage[i].Removed)
            {
                _garbage.RemoveAt(i);
                _count--;
                PlayerPrefs.SetInt("CountGarbage", _count);
            }  
        }
    }
}
