using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class NextScene : MonoBehaviour
{
    IntegrationMetric _integrationMetric = new IntegrationMetric();

    private int _levelIndex;
    private float _startLevelTime;

    private const string LevelSceneIndex = "IndexLevelScene";

    private void Awake()
    {
        SetLevelIndex();
    }

    private void Start()
    {
        _startLevelTime = Time.time;
    }

    public void NextSceneTwo()
    {
        FinishLevel(_startLevelTime, _levelIndex);
        int indexLevel = 2;
        _levelIndex = indexLevel;
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
        SceneTwo.Load();
    }

    public void NextSceneThree()
    {
        FinishLevel(_startLevelTime, _levelIndex);
        int indexLevel = 3;
        _levelIndex = indexLevel;
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
        SceneThree.Load();
    }

    public void NextSceneOne()
    {
        FinishLevel(_startLevelTime,_levelIndex);
        int indexLevel = 1;
        _levelIndex = indexLevel;
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
        SampleScene.Load();
    }

    private void SetLevelIndex()
    {
        if (PlayerPrefs.HasKey(LevelSceneIndex))
            _levelIndex = PlayerPrefs.GetInt(LevelSceneIndex);
        else
            _levelIndex = 1;

        _integrationMetric.OnLevelStart(_levelIndex);
    }

    private void FinishLevel(float startLevelTime,int levelIndex)
    {
        int completedLevelTime;
        completedLevelTime = (int)(startLevelTime -=Time.time);

        if(completedLevelTime <0)
            completedLevelTime *= -1;

        _integrationMetric.OnLevelComplete(completedLevelTime, levelIndex);
    }
}
