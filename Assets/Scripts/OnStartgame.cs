using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class OnStartgame : MonoBehaviour
{
    private FinishScene _finishScene;

    IntegrationMetric _integrationMetric = new IntegrationMetric();

    private int _index; 

    private void Awake()
    {
        _integrationMetric.OnGameStart();
        _integrationMetric.SetUserProperty();
        //SampleScene.Load();
        _finishScene = GetComponent<FinishScene>();
    }

    private void Start()
    {
        _index = _finishScene.LevelIndex;
        _finishScene.CheckLevel(_index);
    }
}
