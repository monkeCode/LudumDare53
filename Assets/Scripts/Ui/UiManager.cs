using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _manager;
    public static UiManager Instance => _manager;

    [SerializeField] private FlowingText _damageText;
    [SerializeField] private FlowingText goldText;
    [SerializeField] private FlowingText healingText;
    [SerializeField] private FlowingText textText;
    [SerializeField] private GameObject iconExample;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject ShopUI;
    [SerializeField] private GameObject ShopHelper;
    [SerializeField] private PauseMenu PauseMenu;
    [SerializeField] private Vector2 damageTextOffset = new Vector2(0.1f, 0.1f);
    
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

    public void ShowDamageText(int damage, Vector2 pos)
    {
        Instantiate(_damageText, pos+damageTextOffset, Quaternion.identity).Init(damage.ToString());
    }

    public void ShowRewardText(int money, Vector2 pos)
    {
        Instantiate(goldText, pos, Quaternion.identity).Init(money.ToString());
    }

    public void ShowHealingText(int heal, Vector2 pos)
    {
        Instantiate(healingText, pos + damageTextOffset, Quaternion.identity).Init(heal.ToString());
    }

    public void ShowJustText(string text, Vector2 pos)
    {
        Instantiate(textText, pos + damageTextOffset, Quaternion.identity).Init(text);
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
        }
    }

    public void ShopHelperSetActive(bool value) => ShopHelper.SetActive(value);
}
