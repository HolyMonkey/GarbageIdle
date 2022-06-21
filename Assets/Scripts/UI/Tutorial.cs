using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
            Destroy();

        _button.onClick.AddListener(Destroy);
    }

    private void Destroy()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        _button.onClick.RemoveListener(Destroy);
        Destroy(gameObject);
    }
}
