using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomLoot : MonoBehaviour, IDamageable
{
    [SerializeField] private int coinChance;
    [SerializeField] private int deliveryChance;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject delivery;
    [SerializeField] private GameObject medkit;
    

    public void TakeDamage(int damage)
    {
        Kill();
    }

    public void Kill()
    {
        GenerateLoot();
        RandomLootSpawnPoint.Destroy(this);
        Destroy(gameObject);
    }

    private void GenerateLoot()
    {
        var rand = Random.Range(0, 100);
        if (rand <= coinChance)
            Instantiate(coin, transform.position, quaternion.identity);
        else if (coinChance < rand && rand <= coinChance + deliveryChance)
            Instantiate(delivery, transform.position, quaternion.identity);
        else
            Instantiate(medkit, transform.position, quaternion.identity);
    }
}
