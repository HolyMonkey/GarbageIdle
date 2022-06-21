using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;
using GameAnalyticsSDK;

[RequireComponent(typeof(Data))]
public class OnStartgame : MonoBehaviour
{
    [SerializeField] private FinishScene _finishScene;

    private Data _data;
    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private int _idexLevel;

    private void Awake()
    {
        _integrationMetric.OnGameStart();
        _integrationMetric.SetUserProperty();
        _data = GetComponent<Data>();
        _idexLevel = _data.GetSave(_finishScene.LevelSceneIndexName, _finishScene.LevelIndex);
        _finishScene.CheckLevel(_idexLevel);
    }
}
