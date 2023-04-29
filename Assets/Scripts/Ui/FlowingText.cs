using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FlowingText : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        _text.alpha -= 1 / _lifetime * Time.deltaTime;
    }

    
    public void Init(string text)
    {
        _text.SetText(text);
    }
}
