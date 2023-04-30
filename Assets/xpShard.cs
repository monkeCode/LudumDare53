using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class xpShard : MonoBehaviour
{
    private int xp;
    [SerializeField] private Color lowXp;
    [SerializeField] private Color mediumXp;
    [SerializeField] private Color highXp;
    
    public enum Type
    {
        LowXp,
        MediumXp,
        HighXp
    }

    public void InitRandomShard(int mediumXpChance = 10, int highXpChance = 1)
    {
        var rand = Random.Range(0, 100);
        if (rand <= mediumXpChance)
            InitXpShardOfType(Type.MediumXp);
        else if (mediumXpChance < rand && rand <= mediumXpChance + highXpChance)
            InitXpShardOfType(Type.HighXp);
        else
            InitXpShardOfType(Type.LowXp);
    }

    public void InitXpShardOfType(Type xp)
    {
        switch (xp)
        {
            case Type.LowXp:
                gameObject.GetComponent<SpriteRenderer>().material.color = lowXp;
                this.xp = 10;
                break;
            case Type.MediumXp:
                gameObject.GetComponent<SpriteRenderer>().material.color = mediumXp;
                this.xp = 25;
                break;
            case Type.HighXp:
                gameObject.GetComponent<SpriteRenderer>().material.color = highXp;
                this.xp = 50;
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().material.color = lowXp;
                this.xp = 10;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<Player.Player>())
            return;
        Player.Player.Instance.AddExperience(xp);
        Destroy(gameObject);
    }
}
