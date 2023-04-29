using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

public class DeliveryPoint : MonoBehaviour
{
    private static List<DeliveryPoint> DeliveryPoints = new List<DeliveryPoint>();
    private static int TakeDeliveryCD = 30;
    private float NextDeliveryTime;
    public List<Debuff> debuffs;

    private void Start()
    {
        DeliveryPoints.Add(this);
        debuffs = new List<Debuff>();
        var speedDebuff = new Debuff((x) => { Player.MovementsController.Instance.SpeedDebuff(x); }, 2);
        var invertDebuff = new Debuff((x) => { Player.MovementsController.Instance.InvertMovementDebuff(x); }, 2);
        debuffs.Add(speedDebuff);
        debuffs.Add(invertDebuff);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if (!col.gameObject.GetComponent<Player.Player>())
            return;
        
        CompleteDeliveries();

        if (NextDeliveryTime > 0)
        {
            NextDeliveryTime -= Time.deltaTime;
            return;
        }

        Player.Player.Instance.StartDelivery(GetNewDelivery());
    }

    private void CompleteDeliveries()
    {
        var deliveries = Player.Player.Instance.CompleteDelivery(this);
        if (deliveries.Length == 0)
            return;
        foreach (var delivery in deliveries)
            Player.Player.Instance.money += delivery.Reward;
    }

    private Delivery GetNewDelivery()
    {
        var destination = this;
        while (destination == this)
            destination = DeliveryPoints[Random.Range(0, DeliveryPoints.Count)];
        var debuff = debuffs[Random.Range(0, debuffs.Count)];
        var delivery = new Delivery(this, destination, debuff);
        
        NextDeliveryTime = TakeDeliveryCD;

        return delivery;
    }
}
