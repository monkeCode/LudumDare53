using System;

namespace DefaultNamespace
{
    public class Debuff
    {
        public Action<bool> Action;
        public float RewardRatio;

        public Debuff(Action<bool> action, float rewardRatio)
        {
            Action = action;
            RewardRatio = rewardRatio;
        }
    }
}