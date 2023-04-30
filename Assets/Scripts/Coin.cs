using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    private int money;
    private void Start()
    {
        money = Random.Range(1, 50);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<Player.Player>())
            return;
        Player.Player.Instance.Money += money;
        Destroy(gameObject);
    }
}
