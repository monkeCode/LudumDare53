using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RandomLootSpawnPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> LootSpawnPoints;
    [SerializeField] private RandomLoot RandomLoot;
    private int SpawnLootCD = 30;
    public List<RandomLoot> allSpawnedLoots = new List<RandomLoot>();
    [FormerlySerializedAs("DistanceToSpawnPoint")] [SerializeField] private float distanceToSpawnPoint;
    private float diagonalDistance;
    
    private static RandomLootSpawnPoint _instance;
    public static RandomLootSpawnPoint Instance => _instance;

    private void Start()
    {
        if (Instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        diagonalDistance = (float)Math.Sqrt(distanceToSpawnPoint);
        SetSpawnPointsPositions();
        StartCoroutine(SpawnLoot());
    }

    private IEnumerator SpawnLoot()
    {
        while (true)
        {
            var spawnPoint = LootSpawnPoints[Random.Range(0, LootSpawnPoints.Count)];
            var loot =  Instantiate(RandomLoot, spawnPoint.position, Quaternion.identity);
            allSpawnedLoots.Add(loot);
            yield return new WaitForSeconds(SpawnLootCD);
        }
    }

    public static void Destroy(RandomLoot loot) => Instance.allSpawnedLoots.Remove(loot);

    private void SetSpawnPointsPositions()
    {
        for (int i = 0; i < LootSpawnPoints.Count; i++)
        {
            var angle = 2 * Mathf.PI / LootSpawnPoints.Count * i;
            LootSpawnPoints[i].position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) *
                                          distanceToSpawnPoint + transform.position;
        }
    }
    
}
