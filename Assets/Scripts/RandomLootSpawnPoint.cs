using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomLootSpawnPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> LootSpawnPoints;
    [SerializeField] private GameObject RandomLoot;
    private int SpawnLootCD = 30;

    private void Start()
    {
        StartCoroutine(SpawnLoot());
    }

    private IEnumerator SpawnLoot()
    {
        while (true)
        {
            var spawnPoint = LootSpawnPoints[Random.Range(0, LootSpawnPoints.Count)];
            Instantiate(RandomLoot, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnLootCD);
        }
    }
}
