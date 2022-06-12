using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Data))]
public class PointStart : MonoBehaviour
{
    private List<Garbage> _garbage = new List<Garbage>();
    private Data _data;
    private int _count = 100;
    public int Count => _count;

    private const string NameCountGarbage = "CountGarbagePointStart";

    private void Start()
    {
        _data = GetComponent<Data>();

        _count = _data.GetSave(NameCountGarbage, _count);
    }

    private void Update()
    {
        ClearList();
    }

    public void Reset(int count)
    {
        _count = count +1;
        PlayerPrefs.SetInt(NameCountGarbage, _count);
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
                PlayerPrefs.SetInt(NameCountGarbage, _count);
            }
        }
    }
}
