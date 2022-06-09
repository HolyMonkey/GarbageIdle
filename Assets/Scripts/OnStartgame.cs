using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class OnStartgame : MonoBehaviour
{
    IntegrationMetric _integrationMetric = new IntegrationMetric();

    private void Awake()
    {
        _integrationMetric.OnGameStart();
        _integrationMetric.SetUserProperty();
        SampleScene.Load();
    }
}
