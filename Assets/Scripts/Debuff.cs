using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Debuff
    {
        public Action<bool> Action;
        public float RewardRatio;
        public Sprite Icon;

        public Debuff(Action<bool> action, float rewardRatio, Sprite icon)
        {
            Action = action;
            RewardRatio = rewardRatio;
            Icon = icon;
        }
    }
}