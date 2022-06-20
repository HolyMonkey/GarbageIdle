using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarbageShowAddMoney : MonoBehaviour
{
    [SerializeField] private Canvas _prefab;
    [SerializeField] private SpawnerWorker _spawnerWorker;

    private List<Canvas> _texts = new List<Canvas>();
    private List<Worker> _workers = new List<Worker>();
    private float _delayAnimation = 1.2f;

    private void Awake()
    {
        foreach (var item in _texts)
            item.gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        _spawnerWorker = FindObjectOfType<SpawnerWorker>();
    }

    private void OnEnable()
    {
        _spawnerWorker.WorkerSpawned += SubscribeOnWorker;
    }

    private void OnDisable()
    {
        _spawnerWorker.WorkerSpawned -= SubscribeOnWorker;
    }

    private void SubscribeOnWorker(Worker worker)
    {
        if (_workers.Contains(worker) == true)
            return;

        _workers.Add(worker);
        var canvas = Instantiate(_prefab);
        _texts.Add(canvas);
        canvas.gameObject.SetActive(false);

        worker.Died += UnSubscribeOnWorker;
        worker.AddedMoney += ShowText;
    }

    private void UnSubscribeOnWorker(Worker worker)
    {
        worker.AddedMoney -= ShowText;
    }

    private void ShowText(int money)
    {
        foreach (var item in _texts)
        {
            if(item.gameObject.activeSelf == false)
            {
                item.gameObject.SetActive(true);
                StartCoroutine(ActiveShowAddMoney(item));
                return;
            }
        }

        var spawned = Instantiate(_prefab);
        StartCoroutine(ActiveShowAddMoney(spawned));
    }

    private IEnumerator ActiveShowAddMoney(Canvas prefab)
    {
        yield return new WaitForSeconds(_delayAnimation);

        prefab.gameObject.SetActive(false);
    }
}
