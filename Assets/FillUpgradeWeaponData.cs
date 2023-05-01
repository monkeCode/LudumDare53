using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class FillUpgradeWeaponData : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponLvl;
    [SerializeField] private TextMeshProUGUI weaponDesc;
    [SerializeField] private TextMeshProUGUI weaponPrice;
    [SerializeField] private Button upgradeButton;

    public void Fill(Weapon weapon)
    {
        image.sprite = weapon.weaponPic;
        weaponName.text = weapon.weaponName;
        var lvl = weapon.Lvl + 1;
        weaponLvl.text = "lvl" + lvl;
        weaponDesc.text = weapon.description;
        weaponPrice.text = WeaponManager.Instance.lvlToUpgradePrice[weapon.Lvl].ToString();
        upgradeButton.onClick.AddListener(() => UpgradeWeapon(weapon));
    }

    private void UpgradeWeapon(Weapon weapon)
    {
        var price = WeaponManager.Instance.lvlToUpgradePrice[weapon.Lvl];
        var money = Player.Player.Instance.Money;
        if (money < price)
            return;
        Player.Player.Instance.Money -= price;
        WeaponManager.Instance.UpgradeWeapon(weapon);
        Destroy(gameObject);
    }
}
