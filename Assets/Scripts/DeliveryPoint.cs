using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

public class DeliveryPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;
    public static List<DeliveryPoint> DeliveryPoints = new List<DeliveryPoint>();
    private static int TakeDeliveryCD = 30;
    private float NextDeliveryTime;
    public List<Debuff> debuffs;
    private bool PlayerInPoint;
    private bool shopGenerated;
    private Weapon weaponToBuy;
    private Weapon weaponToUpgrade;
    private Weapon weapon;
    private bool isToBuy;

    private void Start()
    {
        DeliveryPoints.Add(this);
        debuffs = DebuffManager.Debuffs;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<Player.Player>())
            return;

        PlayerInPoint = true;
        UiManager.Instance.ShopHelperSetActive(true);
        
        CompleteDeliveries();

        if (NextDeliveryTime > 0)
        {
            return;
        }

        Player.Player.Instance.StartDelivery(GetNewDelivery());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Player.Player>())
            return;

        PlayerInPoint = false;
        UiManager.Instance.ShopHelperSetActive(false);
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
            Player.Player.Instance.Money += (int)delivery.Reward;
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

    private void OpenShopMenu()
    {
        UiManager.Instance.ShopUISetActive(true);
        if (!shopGenerated)
            GetShopGeneration();
        GenerateShop();
    }

    private void GetShopGeneration()
    {
        weaponToBuy = WeaponManager.Instance.GetRandomToBuyWeapon();
        weaponToUpgrade = WeaponManager.Instance.GetRandomToUpgradeWeapon();
        var random = WeaponManager.Instance.GetRandomWeapon();
        weapon = random.Item1;
        isToBuy = random.Item2;
    }

    private void GenerateShop()
    {
        UiManager.Instance.CreateShopTab(weaponToBuy, weaponToUpgrade, weapon, isToBuy);
        shopGenerated = true;
    }

    private void Update()
    {
        if (NextDeliveryTime > 0)
        {
            _timer.alpha = 1;
            _timer.SetText(((int)NextDeliveryTime).ToString());
        }
        else
        {
            _timer.alpha = 0;
            shopGenerated = false;
        }

        NextDeliveryTime = Mathf.Clamp(NextDeliveryTime - Time.deltaTime, 0, NextDeliveryTime);
        
        if (PlayerInPoint && Input.GetKeyDown(KeyCode.E))
            OpenShopMenu();
    }
}
