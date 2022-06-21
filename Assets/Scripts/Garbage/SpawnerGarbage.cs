using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Data))]
public class SpawnerGarbage : MonoBehaviour
{
    [SerializeField] private Garbage[] _garbage;
    [SerializeField] private int _count;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PointStart _pointStart;
    [SerializeField] private AddRewardGarbage _addRewardGarbage;

    private Data _data;

    private void Start()
    {
        _data = GetComponent<Data>();
        _count = _data.GetSave(_pointStart.CountGarbage, _pointStart.Count);
        TimeToSpawn();
       
    }

    private void TimeToSpawn()
    {
        Garbage spawned;
        int point = 0;
        int item = 0;

        for (int i ; _count != 0; _count--)
        {
            point = Random.Range(0, _spawnPoint.Length);
            item = Random.Range(0, _garbage.Length);

            spawned = Instantiate(_garbage[item], _spawnPoint[point]);

            _pointStart.AddListGarbage(spawned);
            _addRewardGarbage.AddListGarbage(spawned);
        }
    }
}
