using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] bool IsActive = false;
    [SerializeField] Action<String> OnPlayerEnter;
    [SerializeField] String NextArena;
    public void SetHole(bool value)
    {
        IsActive = value;
    }
    public void SetEnterEvent(Action<String> ev)
    {
        OnPlayerEnter -= ev;
        OnPlayerEnter += ev;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!IsActive) return;
        if(other.tag == "Player")
        {
            OnPlayerEnter?.Invoke(NextArena);
        }
    }
}
