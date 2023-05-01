using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class FillBuyWeaponData : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponLvl;
    [SerializeField] private TextMeshProUGUI weaponDesc;
    [SerializeField] private TextMeshProUGUI weaponPrice;
    [SerializeField] private Button buyButton;

    public void Fill(Weapon weapon)
    {
        
        weaponName.text = weapon.name;
         weaponLvl.text = "lvl" + 1;
         weaponDesc.text = "unknown desc";
         weaponPrice.text = WeaponManager.Instance.buyPrice.ToString();
         buyButton.onClick.AddListener(() => BuyWeapon(weapon));
    }

    private void BuyWeapon(Weapon weapon)
    {
        var price = WeaponManager.Instance.buyPrice;
        var money = Player.Player.Instance.Money;
        if (money < price)
            return;
        Player.Player.Instance.Money -= price;
        WeaponManager.Instance.BuyWeapon(weapon);
        Destroy(gameObject);
    }
}
