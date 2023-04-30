using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int heal;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<Player.Player>())
            return;
        UiManager.Instance.ShowHealingText(heal, Player.Player.Instance.transform.position);
        Player.Player.Instance.Heal(heal);
        Destroy(gameObject);
    }
}
