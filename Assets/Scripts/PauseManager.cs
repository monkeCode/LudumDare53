using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager: MonoBehaviour
{
    public UnityEvent onPauseOn = new();
    public UnityEvent onPauseOff = new();
    public bool isPauseOn;
    public bool canOffPause = true;
    public static PauseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        canOffPause = true;
        PauseOff();
    }

    public void PauseOn()
    {
        Time.timeScale = 0;
        isPauseOn = true;
        onPauseOn.Invoke();
    }

    public void PauseOff()
    {
        if(!canOffPause) return;
        Time.timeScale = 1;
        isPauseOn = false;
        onPauseOff.Invoke();
    }
}