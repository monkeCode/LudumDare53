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
        debuffs = DebuffManager.Debuffs;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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
        var sum = 0f;
        foreach (var delivery in deliveries)
        {
            sum += delivery.Reward;
            Player.Player.Instance.money += delivery.Reward;
        }
        UiManager.Instance.ShowRewardText(Convert.ToInt32(sum), transform.position + new Vector3(0, 0.5f, 0));
        UiManager.Instance.ShowDebuffIcons();
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