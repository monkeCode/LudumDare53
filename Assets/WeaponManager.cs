using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weaponsToBuy; 
    public int buyPrice;
    [SerializeField] private Weapon defaultWeapon;
    [SerializeField] private int maxLvl;
    [SerializeField] private int lvl1UpgradePrice;
    public Dictionary<int, int> lvlToUpgradePrice;
    public Dictionary<Weapon, int> WeaponLvls;
    
    private static WeaponManager _instance;
    public static WeaponManager Instance => _instance;

    void Start()
    {
        if (Instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        WeaponLvls = new Dictionary<Weapon, int>();
        WeaponLvls.Add(defaultWeapon, 1);
        lvlToUpgradePrice = new Dictionary<int, int>();
        lvlToUpgradePrice[1] = lvl1UpgradePrice;
        for (var i = 2; i < maxLvl; i++)
            lvlToUpgradePrice[i] = lvlToUpgradePrice[i - 1] * 2;
    }

    public void UpgradeWeapon(Weapon weapon)
    {
        weapon.LvlUp();
        WeaponLvls[weapon]++;
    }

    public void BuyWeapon(Weapon weapon)
    {
        weaponsToBuy.Remove(weapon);
        var weaponInstance = Instantiate(weapon, Player.Player.Instance.transform);
        WeaponLvls.Add(weaponInstance, 1);
    }

    public Weapon GetRandomToBuyWeapon()
    {
        return weaponsToBuy.Count == 0 ? null : weaponsToBuy[Random.Range(0, weaponsToBuy.Count)];
    }

    public Weapon GetRandomToUpgradeWeapon()
    {
        var weapons = WeaponLvls.Select(x => x.Key).ToArray();
        return weapons.Length == 0 ? null : weapons[Random.Range(0, weapons.Length)];
    }

    public (Weapon, bool) GetRandomWeapon(Weapon toBuy, Weapon toUpgrade)
    {
        var isToBuy = true;
        if (weaponsToBuy.Count == 1 && WeaponLvls.Count == 1)
            return (null, true);
        if (WeaponLvls.Count == 1)
            return (GetRandomToBuyWeaponExcept(toBuy), isToBuy);
        if (weaponsToBuy.Count == 1)
            return (GetRandomToUpgradeWeaponExcept(toUpgrade), !isToBuy);
        var rand = Random.Range(0, 2);
        return rand == 0 ? (GetRandomToBuyWeaponExcept(toBuy), isToBuy) : (GetRandomToUpgradeWeaponExcept(toUpgrade), !isToBuy);
    }

    private Weapon GetRandomToBuyWeaponExcept(Weapon toBuy)
    {
        var weapon = toBuy;
        while (weapon == toBuy)
            weapon = weaponsToBuy[Random.Range(0, weaponsToBuy.Count)];
        return weapon;
    }

    private Weapon GetRandomToUpgradeWeaponExcept(Weapon toUpgrade)
    {
        var weapons = WeaponLvls.Select(x => x.Key).ToArray();
        var weapon = toUpgrade;
        while (weapon == toUpgrade)
            weapon = weapons[Random.Range(0, weapons.Length)];
        return weapon;
    }

    public bool ValidateWeapon(Weapon weapon)
    {
        return weaponsToBuy.Contains(weapon);
    }


}
