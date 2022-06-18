using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class OnStartgame : MonoBehaviour
{
    [SerializeField] private FinishScene _finishScene;

    IntegrationMetric _integrationMetric = new IntegrationMetric();

    private int _index =0; 

    private void Awake()
    {
        _integrationMetric.OnGameStart();
        _integrationMetric.SetUserProperty();
        _index = _finishScene.LevelIndex;
        _finishScene.CheckLevel(_index);
    }
}
