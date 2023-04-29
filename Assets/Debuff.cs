using System;

namespace DefaultNamespace
{
    public class Debuff
    {
        public Action Action;
        public float RewardRatio;

        public Debuff(Action action, float rewardRatio)
        {
            Action = action;
            RewardRatio = rewardRatio;
        }
    }
}