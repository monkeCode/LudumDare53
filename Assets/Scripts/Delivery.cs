using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Delivery
    {
        public float Reward;
        public DeliveryPoint Destination;
        public Debuff Debuff;

        public Delivery(DeliveryPoint departure, DeliveryPoint destination, Debuff debuff)
        {
            Reward = CalculateReward(departure, destination, debuff);
            Destination = destination;
            Debuff = debuff;
        }

        private static float CalculateReward(DeliveryPoint departure, DeliveryPoint destination, Debuff debuff)
        {
            var distance = Vector2.Distance(departure.transform.position, destination.transform.position);
            var reward = distance/2 * debuff.RewardRatio;
            return reward;
        }
    }
}