using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStart : MonoBehaviour
{
    private List<Garbage> _garbage = new List<Garbage>();

    private SpawnerGarbage _spawnerGarbage;
    private int _count = 100;
    private bool _blankSheek = false;
    public int Count => _count;
    public bool BlankSheet => _blankSheek;

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

    public void AddListGarbage(Garbage garbage,SpawnerGarbage spawnerGarbage)
    {
        _garbage.Add(garbage);
        _spawnerGarbage = spawnerGarbage;
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

            if (_garbage.Count == 0)
                _blankSheek = true;
        }
    }
}
