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
    private int _levelIndex;
    private float _startLevelTime;

    public const string LevelSceneIndex = "IndexLevelScene";
    public int LevelIndex => _levelIndex;

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
        PlayerPrefs.SetInt(LevelSceneIndex,_levelIndex);
    }

    public void NextSceneTwo()
    {
        FinishLevel(_startLevelTime, _levelIndex);
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
        SceneTwo.Load();
    }

    public void NextSceneThree()
    {
        FinishLevel(_startLevelTime, _levelIndex);
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
        SceneThree.Load();
    }

    public void NextSceneOne()
    {
        FinishLevel(_startLevelTime, _levelIndex);
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
        SampleScene.Load();
    }

    public void CheckLevel(int index)
    {
        if (index == 0)
        {
            SampleScene.Load();
        }
        if (index == 1)
        {
            SceneTwo.Load();
        }
        else if (index == 2)
        {
            SceneThree.Load();
        }
        else if (index == 3)
        {
            SampleScene.Load();

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
