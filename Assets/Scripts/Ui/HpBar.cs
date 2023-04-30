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
        if (arg2 < arg1)
            arg1 = arg2 - 1;
        var i = (int)Math.Floor((float)arg1 / arg2 * _sprites.Length);
        _image.sprite = _sprites[i];
        _image.SetNativeSize();
    }
}
