using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int GetSave(string name, int value)
    {
        if (PlayerPrefs.HasKey(name))
        {
            value = PlayerPrefs.GetInt(name);
        }
        return value;
    }

    public float GetSaveFloat(string name,float value)
    {
        if (PlayerPrefs.HasKey(name))
        {
            value = PlayerPrefs.GetFloat(name);
        }
        return value;
    }
}
