using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NoticeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _time;
    private Coroutine _anim;

    private RectTransform _rect;
    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void Show(string text)
    {
        _text.SetText(text);
        if(_anim is not null)
            StopCoroutine(_anim);
        _anim = StartCoroutine(Move());

    }

    private IEnumerator Move()
    {
        while (_rect.anchoredPosition.y <= 69.8)
        {
            _rect.anchoredPosition = Vector2.Lerp(_rect.anchoredPosition, new Vector2(0, 70), _time);
            yield return null;
        }

        _rect.anchoredPosition = new Vector2(0, 70);
        yield return new WaitForSeconds(5);
        while (_rect.anchoredPosition.y >= 0.2)
        {
            _rect.anchoredPosition = Vector2.Lerp(_rect.anchoredPosition, new Vector2(0, 0), _time);
            yield return null;
        }
        _rect.anchoredPosition = new Vector2(0, 0);
    }
}
