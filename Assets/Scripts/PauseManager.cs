using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager: MonoBehaviour
{
    public UnityEvent onPauseOn = new();
    public UnityEvent onPauseOff = new();
    private bool _isPauseOn;
    public static PauseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPauseOn)
            {
                PauseOff();
            }
            else
            {
                PauseOn();
            }
        }
    }

    public void PauseOn()
    {
        Time.timeScale = 0;
        _isPauseOn = true;
        onPauseOn.Invoke();
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
        _isPauseOn = false;
        onPauseOff.Invoke();
    }
}