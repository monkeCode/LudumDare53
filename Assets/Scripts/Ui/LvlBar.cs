using System;

public class LvlBar : Bar
{
    protected override void Subscribe(Action<int, int> action)
    {
        Player.Player.Instance.ExpChanged += action;
    }

    protected override void Describe(Action<int, int> action)
    {
        Player.Player.Instance.ExpChanged -= action;
    }
}
