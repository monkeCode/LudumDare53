using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    [SerializeField] private Sprite SlowDebuff;
    [SerializeField] private Sprite InvertMovementDebuff;
    [SerializeField] private Sprite HealthDebuff;
    [SerializeField] private Sprite FragileDeliveryDebuff;
    [SerializeField] private Sprite LessAttacksDebuff;
    [SerializeField] private Sprite LessAttackSpeedDebuff;
    public static List<Debuff> Debuffs = new List<Debuff>();
    void Awake()
    {
        var speedDebuff = new Debuff((x) => { Player.MovementsController.Instance.SpeedDebuff(x); }, 3, SlowDebuff);
        var invertDebuff = new Debuff((x) => { Player.MovementsController.Instance.InvertMovementDebuff(x); }, 2, InvertMovementDebuff);
        var healthDebuff = new Debuff(x => { Player.Player.Instance.HealthDebuff(x); }, 3, HealthDebuff);
        var fragileDeliveryDebuff = new FragileDebuff(x => { }, 3, FragileDeliveryDebuff);
        var lessAttacksDebuff = new Debuff(x => { Player.Player.Instance.LessAttacksDebuff(x); }, 2, LessAttacksDebuff);
        var lessAttackSpeedDebuff = new Debuff(x => { Player.Player.Instance.LessAttackSpeedDebuff(x); }, 2,
            LessAttackSpeedDebuff);
            //Двойной урон
            // Уменьшен урон
            Debuffs.Add(speedDebuff);
        Debuffs.Add(invertDebuff);
        Debuffs.Add(healthDebuff);
        Debuffs.Add(fragileDeliveryDebuff);
        Debuffs.Add(lessAttacksDebuff);
        Debuffs.Add(lessAttackSpeedDebuff);
    }

}
