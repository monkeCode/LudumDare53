using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager _manager;
    public static UiManager Instance => _manager;

    [SerializeField] private FlowingText _damageText;
    
    private void Awake()
    {
        if(_manager == null)
            _manager = this;
        else Destroy(gameObject);
    }

    public void ShowDamageText(int damage, Vector2 pos)
    {
        Instantiate(_damageText, pos, Quaternion.identity).Init(damage.ToString());
    }
}
