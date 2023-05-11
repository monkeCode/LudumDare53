using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHeal : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private int heal;

    public void BuyHealing()
    {
        if (Player.Player.Instance.Money < price) return;
        Player.Player.Instance.Money -= price;
        Player.Player.Instance.Heal((int) ((float)heal/100 * Player.Player.Instance.MaxHp));
    }
}
