using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    [SerializeField] private Sprite SlowDebuff;
    [SerializeField] private Sprite InvertMovementDebuff;
    public static List<Debuff> Debuffs = new List<Debuff>();
    void Awake()
    {
        var speedDebuff = new Debuff((x) => { Player.MovementsController.Instance.SpeedDebuff(x); }, 2, SlowDebuff);
        var invertDebuff = new Debuff((x) => { Player.MovementsController.Instance.InvertMovementDebuff(x); }, 2, InvertMovementDebuff);
        Debuffs.Add(speedDebuff);
        Debuffs.Add(invertDebuff);
    }

}
