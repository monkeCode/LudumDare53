using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FragileDebuff : Debuff
    {
        public FragileDebuff(Action<bool> action, float rewardRatio, Sprite icon) : base(action, rewardRatio, icon)
        {
            Action = action;
            RewardRatio = rewardRatio;
            Icon = icon;
            Player.Player.Instance.HpChangedFromTo += TakeDamage;
        }

        private void TakeDamage(int before, int after)
        {
            RewardRatio -= 0.01f * before-after;
        }
    }
}