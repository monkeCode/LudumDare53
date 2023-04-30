using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryLoot : MonoBehaviour
{

    private Delivery GetNewDelivery()
    {
        var deliveryPoints = DeliveryPoint.DeliveryPoints;
        var destination = deliveryPoints[Random.Range(0, deliveryPoints.Count)];
        var debuffs = DebuffManager.Debuffs;
        var debuff = debuffs[Random.Range(0, debuffs.Count)];
        var departure = destination;
        while(departure == destination)
            departure = deliveryPoints[Random.Range(0, deliveryPoints.Count)];
        var delivery = new Delivery(departure, destination, debuff);
        
        return delivery;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<Player.Player>())
            return;
        Player.Player.Instance.StartDelivery(GetNewDelivery());
        Destroy(gameObject);
    }
}
