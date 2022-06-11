using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWorker : MonoBehaviour
{
    [SerializeField] private Worker _prefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PointStart _pointStart;
    [SerializeField] private Transform _pointFinish;
    [SerializeField] private Transform _junkyard;
    [SerializeField] private ValueHandler _valueHandler;
    [SerializeField] private SpeedWorkers _speedWorkers;

    private int _count = 0;
    private float _elepsedTime = 0;

    public const float Delay = 1f;

    private void Update()
    {
        _elepsedTime += Time.deltaTime;

        if(_elepsedTime>= Delay)
        {
            if (_count > 0)
            {
                Initilaze();
                _count--;
                _elepsedTime = 0;
            }
            else
                return;
        }
    }

    public void Initilaze()
    {
        Worker spawned = Instantiate(_prefabs, _spawnPoint);
        spawned.Init(_pointStart, _pointFinish, _junkyard,_valueHandler);
        _speedWorkers.AddListGarbage(spawned);
    }

    public void AddCount(int count)
    {
        _count = count;
    }
}
