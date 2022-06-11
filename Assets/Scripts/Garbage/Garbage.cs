using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Garbage : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _highlighted = false;
    private bool _removed = false;
    private bool _visible = false;
    private int _price = 20;

    public bool InVisible => _visible;
    public int Price => _price;
    public bool Removed => _removed;
    public bool Highlighted => _highlighted;
    public Rigidbody Rigidbody => _rigidbody;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Price"))
            _price = PlayerPrefs.GetInt("Price");

        _rigidbody = GetComponent<Rigidbody>();
    }

    public void CheckVisibleGarbage()
    {
        _visible = true;
    }

    public void CheckInVisible()
    {
        _visible = false;
    }

    public void BeenSelected()
    {
        _highlighted = true;
    }

    public void RisePrice()
    {
        _price += 20;
        PlayerPrefs.SetInt("Price", _price);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GarbageCan garbageCan))
        {
            if (_removed == false)
            {
                _removed = true;
                garbageCan.AddCount();
                _price = 0;
            }
            else
                return;
           
        }
    }
}
