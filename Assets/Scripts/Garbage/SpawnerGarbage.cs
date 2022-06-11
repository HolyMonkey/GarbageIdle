using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGarbage : MonoBehaviour
{
    [SerializeField] private Garbage[] _garbage;
    [SerializeField] private int _count;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PointStart _pointStart;
    [SerializeField] private AddRewardGarbage _addRewardGarbage;

    private void Start()
    {
        _count = _pointStart.Count;
    }

    private void Update()
    {
        if (_count != 0)
            TimeToSpawn();

    }

    private void TimeToSpawn()
    {
        for (int i = 0; i < _count; i++)
        {
            var point = Random.Range(0, _spawnPoint.Length);
            var item = Random.Range(0, _garbage.Length);
            Garbage spawned = Instantiate(_garbage[item], _spawnPoint[point]);;
            _pointStart.AddListGarbage(spawned,this);
            _addRewardGarbage.AddListGarbage(spawned);
            _count--;
        }
    }
}
