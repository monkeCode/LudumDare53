using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        Player.Player.Instance.MoneyChanged+= MoneyChanged;
    }

    private void MoneyChanged(int obj)
    {
        _text.SetText(obj.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
