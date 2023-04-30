using System;
using TMPro;
using UnityEngine;

public class LvlBar : Bar
{
    [SerializeField] private TextMeshProUGUI _lvlText;
    protected override void Subscribe(Action<int, int> action)
    {
        Player.Player.Instance.ExpChanged += action;
        Player.Player.Instance.ExpChanged += UpdateLvl;
    }

    protected override void Describe(Action<int, int> action)
    {
        Player.Player.Instance.ExpChanged -= action;
        Player.Player.Instance.ExpChanged -= UpdateLvl;
    }

    private void UpdateLvl(int arg1, int arg2)
    {
        _lvlText.SetText(Player.Player.Instance.Lvl.ToString());
    }
}
