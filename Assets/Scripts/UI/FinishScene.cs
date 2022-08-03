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
    private int _scenelevel = 1;
    private MenuDisplay _menuDisplay;

    public const string LevelSceneIndex = "IndexLevelScene";
    public const string SceneLevelIndexNameFinishScene = "SceneLevelIndexNameFinishScen";
    public int LevelIndex => _levelIndex;
    public string LevelSceneIndexName => LevelSceneIndex;

    private void Awake()
    {
        _menuDisplay = FindObjectOfType<MenuDisplay>();
        _data = GetComponent<Data>();
        _startLevelTime = Time.time;
        _levelIndex = _data.GetSave(LevelSceneIndex, _levelIndex);
        _scenelevel = _data.GetSave(SceneLevelIndexNameFinishScene, _scenelevel);
    }

    private void Start()
    {
        
        //_integrationMetric.OnLevelStart(_scenelevel);
        Close();
    }

    public void OpenScrinFinish()
    {
        gameObject.SetActive(true);
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        FinishLevel(_startLevelTime, _scenelevel);
        SetLevelIdex();
        _menuDisplay.Show();
    }

    public void NextSceneTwo()
    {
        SceneTwo.Load();
        _menuDisplay.Hide();
    }

    public void NextSceneThree()
    {
        SceneThree.Load();
        _menuDisplay.Hide();
    }

    public void NextSceneOne()
    {
        SampleScene.Load();
        _menuDisplay.Hide();
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

    private void SetLevelIdex()
    {
        _levelIndex++;
        _scenelevel++;

        if (_levelIndex > _maxLevelIndex)
        {
            _levelIndex = 1;
        }
        PlayerPrefs.SetInt(SceneLevelIndexNameFinishScene, _scenelevel);
        PlayerPrefs.SetInt(LevelSceneIndex, _levelIndex);
    }

    private void FinishLevel(float startLevelTime, int levelIndex)
    {
        int completedLevelTime;
        completedLevelTime = (int)(startLevelTime -= Time.time);

        if (completedLevelTime < 0)
            completedLevelTime *= -1;

        //_integrationMetric.OnLevelComplete(completedLevelTime, levelIndex);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
