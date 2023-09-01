using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    private bool _ready = false;
    private GameObject _fish;

    public bool Ready => _ready;
    
    public void WakeUp()
    {
        _ready = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_ready) return;
        if(!other.gameObject.CompareTag("Fish") || _fish != null)  return;
        _fish = other.gameObject;
        Debug.Log("Fish!!!");
    }

    private void OnTriggerExit(Collider other)
    {
        if(!_ready) return;
        if (_fish == other.gameObject) _fish = null;
        Debug.Log("No Fish!!!");
    }

    public void ReelIn()
    {
        if (_fish != null)
        {
            Destroy(_fish.gameObject);
            GameData.Instance.Fish++;
        }
        Destroy(gameObject);
    }
}
