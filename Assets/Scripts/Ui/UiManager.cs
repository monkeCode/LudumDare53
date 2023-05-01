using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weapons;

public class UiManager : MonoBehaviour
{
    private static UiManager _manager;
    public static UiManager Instance => _manager;

    [SerializeField] private FlowingText _damageText;
    [SerializeField] private FlowingText goldText;
    [SerializeField] private FlowingText healingText;
    [SerializeField] private NoticeText textText;
    [SerializeField] private GameObject iconExample;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject ShopUI;
    [SerializeField] private GameObject ShopHelper;
    [SerializeField] private PauseMenu PauseMenu;
    [SerializeField] private DeathMenu DeathMenu;
    [SerializeField] private Vector2 damageTextOffset = new Vector2(0.1f, 0.1f);
    [SerializeField] private GameObject shopToBuyTab;
    [SerializeField] private GameObject shopToUpgradeTab;
    [SerializeField] private Transform shopUiPanel;
    [SerializeField] private Image flashbang;
    private GameObject toBuyTab;
    private GameObject toUpgradeTab;
    private GameObject randomTab;
     private Vector3 ShopUIOffset = new Vector3(0, 120, 0);
     [SerializeField] private int _TotalTextCount;
     private int _textCountNow = 0;

    private List<GameObject> currentDebuffIcons;

    private float Yborder = 30;
    private Vector3 iconStep;
    private Vector3 DebuffIconPos; 

    private void Awake()
    {
        if(_manager == null)
            _manager = this;
        else Destroy(gameObject);
        iconStep = new Vector3(iconExample.GetComponent<RectTransform>().rect.width, 0, 0);
        currentDebuffIcons = new List<GameObject>();
        DebuffIconPos = new Vector3(0, Screen.height - Yborder);
    }

    private void Start()
    {
        Player.Player.Instance.onDeath.AddListener(ShowDeathMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.gameObject.activeSelf)
            {
                ClosePauseMenu();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }
    
    private void ShowPauseMenu()
    {
        PauseManager.Instance.PauseOn();
        PauseMenu.gameObject.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        PauseManager.Instance.PauseOff();
        PauseMenu.gameObject.SetActive(false);
    }

    private void ShowDeathMenu()
    {
        DeathMenu.gameObject.SetActive(true);
    }

    public void ShowDamageText(int damage, Vector2 pos)
    {
        if(_textCountNow > _TotalTextCount) return;
        _textCountNow++;
        Instantiate(_damageText, pos+damageTextOffset, Quaternion.identity).Init(damage.ToString());
    }

    public void ShowRewardText(int money, Vector2 pos)
    {
        if(_textCountNow > _TotalTextCount) return;
        _textCountNow++;
        Instantiate(goldText, pos, Quaternion.identity).Init(money.ToString());
    }

    public void ShowHealingText(int heal, Vector2 pos)
    {
        if(_textCountNow > _TotalTextCount) return;
        _textCountNow++;
        Instantiate(healingText, pos + damageTextOffset, Quaternion.identity).Init(heal.ToString());
    }

    public void ShowJustText(string text)
    {
        textText.Show(text);
    }
    
    public void ShowDebuffIcons()
    {
        foreach (var debuff in currentDebuffIcons)
        {
            Destroy(debuff);
        }
        currentDebuffIcons.Clear();
        var debuffs = Player.Player.Instance.GetDebuffsFromDeliveries();
        if (debuffs.Length == 0)
            return;
        var debuffIndex = 0;
        foreach (var debuff in debuffs)
        {
            var icon = Instantiate(iconExample, canvas.transform);
            icon.transform.position += iconStep * debuffIndex;
            icon.GetComponent<Image>().sprite = debuff.Icon;
            currentDebuffIcons.Add(icon);
            debuffIndex++;
        }
    }

    public void ShopUISetActive(bool value)
    {
        ShopUI.SetActive(value);
        ShopHelper.SetActive(!value);
        PauseManager.Instance.canOffPause = !value;
        if (value)
        {
            PauseManager.Instance.PauseOn();
        }
        else
        {
            PauseManager.Instance.PauseOff();
            ClearShop();
        }
    }

    public void CreateShopTab(Weapon toBuy, Weapon toUpgrade, Weapon random, bool isTobuy)
    {
        if (toBuy != null)
        {
            toBuyTab = Instantiate(shopToBuyTab, shopUiPanel);
            toBuyTab.transform.position += ShopUIOffset;
            toBuyTab.GetComponent<FillBuyWeaponData>().Fill(toBuy);
        }

        if (toUpgrade != null)
        {
            toUpgradeTab = Instantiate(shopToUpgradeTab, shopUiPanel);
            toUpgradeTab.GetComponent<FillUpgradeWeaponData>().Fill(toUpgrade);
        }

        if (random != null)
        {
            var lastTabType = shopToBuyTab;
            if (!isTobuy)
                lastTabType = toUpgradeTab;
            randomTab = Instantiate(lastTabType, shopUiPanel);
            randomTab.transform.position -= ShopUIOffset;
            if (isTobuy)
                randomTab.GetComponent<FillBuyWeaponData>().Fill(random);
            else
                randomTab.GetComponent<FillUpgradeWeaponData>().Fill(random);
        }
    }

    public void ClearShop()
    {
        Destroy(toBuyTab);
        Destroy(toUpgradeTab);
        Destroy(randomTab);
    }

    public void FlashBang()
    {
        flashbang.gameObject.SetActive(true);
        StartCoroutine(Flashbang());
    }

    private IEnumerator Flashbang()
    {
        for (var i = 0; i < 10; i++)
        {
            var color = flashbang.color;
            color.a += 0.1f;
            flashbang.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShopHelperSetActive(bool value) => ShopHelper.SetActive(value);

    public void DeletingText()
    {
        _textCountNow--;
    }
}
