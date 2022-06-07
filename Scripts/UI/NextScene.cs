using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class NextScene : MonoBehaviour
{
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
}
