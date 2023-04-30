using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class GameTimer : MonoBehaviour
{
    private TextMeshProUGUI _text;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var time = 15*60 - WaveGenerator.Timer;
        var min = (int)time / 60;
        var sec = ((int)(time - min*60)).ToString().PadLeft(2, '0');
        _text.SetText($"{min}:{sec}");
    }
}
