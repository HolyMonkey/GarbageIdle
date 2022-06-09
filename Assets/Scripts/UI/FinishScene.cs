using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScene : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenScrinFinish()
    {
        gameObject.SetActive(true);
    }
}
