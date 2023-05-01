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
        diagonalDistance = (float)Math.Sqrt(distanceToSpawnPoint * distanceToSpawnPoint);
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
        var x = transform.position.x;
        var y = transform.position.y;
        LootSpawnPoints[0].position = new Vector3(x, y + distanceToSpawnPoint);
        LootSpawnPoints[1].position = new Vector3(x + diagonalDistance, y +diagonalDistance);
        LootSpawnPoints[2].position = new Vector3(x + distanceToSpawnPoint, y);
        LootSpawnPoints[3].position = new Vector3(x + diagonalDistance, y + -diagonalDistance);
        LootSpawnPoints[4].position = new Vector3(x, y + -distanceToSpawnPoint);
        LootSpawnPoints[5].position = new Vector3(x + -diagonalDistance, y + -diagonalDistance);
        LootSpawnPoints[6].position = new Vector3(x + -distanceToSpawnPoint, y);
        LootSpawnPoints[7].position = new Vector3(x + -diagonalDistance, y + diagonalDistance);
    }
    
}
