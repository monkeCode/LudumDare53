using System;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField]private Sprite[] _sprites;
    [SerializeField] private UnityEngine.UI.Image _image;

    private void Start()
    {
        Player.Player.Instance.HpChanged += HpChanged;
    }
    
    private void OnDisable()
    {
        Player.Player.Instance.HpChanged -= HpChanged;
    }

    private void HpChanged(int arg1, int arg2)
    {
        var coef = (float)arg1 / arg2;
        var i = (int)(coef * (_sprites.Length-1));
        _image.sprite = _sprites[i];
        _image.SetNativeSize();
    }
}
