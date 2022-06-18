using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGarbage : MonoBehaviour
{
    [SerializeField] private Garbage[] _garbage;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PointStart _pointStart;
    [SerializeField] private AddRewardGarbage _addRewardGarbage;

    private int _count;
    private Garbage _spawned;
    int point = 0;
    int item = 0;
    private void Start()
    {
        _count = _pointStart.Count;
    }

    private void Update()
    {
        TimeToSpawn();
    }

    private void TimeToSpawn()
    {
        for (int i = 0; i < _count; i++)
        {
            point = Random.Range(0, _spawnPoint.Length);
            item = Random.Range(0, _garbage.Length);

            _spawned = Instantiate(_garbage[item], _spawnPoint[point]);

            _pointStart.AddListGarbage(_spawned);
            _addRewardGarbage.AddListGarbage(_spawned);

            _count--;
        }
    }
}
