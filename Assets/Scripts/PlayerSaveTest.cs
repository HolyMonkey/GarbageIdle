using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSaveTest
{
    public static int LevelCompleted
    {
        get { return PlayerPrefs.GetInt("LevelCompleted"); }
        set { PlayerPrefs.SetInt("LevelCompleted", value); }
    }
}
