using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Data))]
public class FinishScene : MonoBehaviour
{
    IntegrationMetric _integrationMetric = new IntegrationMetric();
    private Data _data;
    private int _levelIndex =1 ;
    private float _startLevelTime;
    private int _maxLevelIndex = 3;

    public const string LevelSceneIndex = "IndexLevelScene";
    public int LevelIndex => _levelIndex;
    public string LevelSceneIndexName => LevelSceneIndex;

    private void Awake()
    {
        _data = GetComponent<Data>();
        _startLevelTime = Time.time;
        _levelIndex = _data.GetSave(LevelSceneIndex, _levelIndex);
        _integrationMetric.OnLevelStart(_levelIndex);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenScrinFinish()
    {
        gameObject.SetActive(true);
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        FinishLevel(_startLevelTime, _levelIndex);
        SetLevelIdex();
    }

    public void NextSceneTwo()
    {
        SceneTwo.Load();
    }

    public void NextSceneThree()
    {
        SceneThree.Load();
    }

    public void NextSceneOne()
    {
        SampleScene.Load();
    }

    private void SetLevelIdex()
    {
        _levelIndex++;

        if (_levelIndex > _maxLevelIndex)
        {
            _levelIndex = 1;
        }

        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
    }

    public void CheckLevel(int index)
    {
        if (index == 1)
        {
            SampleScene.Load();
        }
        if (index == 2)
        {
            SceneTwo.Load();
        }
        else if (index == 3)
        {
            SceneThree.Load();
        }
    }

    private void FinishLevel(float startLevelTime, int levelIndex)
    {
        int completedLevelTime;
        completedLevelTime = (int)(startLevelTime -= Time.time);

        if (completedLevelTime < 0)
            completedLevelTime *= -1;

        _integrationMetric.OnLevelComplete(completedLevelTime, levelIndex);
    }
}
