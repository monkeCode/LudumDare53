using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

public class DeliveryPoint : MonoBehaviour
{
    public static List<DeliveryPoint> DeliveryPoints;
    public static int TakeDeliveryCD;
    private static float NextDeliveryTime;
    public List<Debuff> debuffs;


    private void OnCollisionEnter(Collision collision)
    {
        
        // TakeDelivery();

        if (NextDeliveryTime > 0)
        {
            NextDeliveryTime -= Time.deltaTime;
            return;
        }

        GiveDelivery();
    }

    private void TakeDelivery(Delivery delivery)
    {
        var reward = delivery.Reward;
    }

    private void GiveDelivery()
    {
        var destination = this;
        while (destination == this)
            destination = DeliveryPoints[Random.Range(0, DeliveryPoints.Count)];
        var debuff = debuffs[Random.Range(0, debuffs.Count)];
        var delivery = new Delivery(this, destination, debuff);
        
        NextDeliveryTime = TakeDeliveryCD;
    }
}
