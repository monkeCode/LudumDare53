using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Unity.VisualScripting;
using UnityEngine;

public class HeinzDoofenshmirtzInstantinator: MonoBehaviour
{
    [SerializeField] private Entity _zombie;
    [SerializeField] private Entity _bat;
    [SerializeField] private Entity _pumpkin;
    
    public const int START_MOSTERS_COUNT = 100;
    
    private static HeinzDoofenshmirtzInstantinator _instance;
    public static HeinzDoofenshmirtzInstantinator Instance => _instance;
    
    private Dictionary<MonsterType, Queue<Entity>> _bufferEntities = new()
    {
        {MonsterType.Zombie, new Queue<Entity>()},
        {MonsterType.Bat, new Queue<Entity>()},
        {MonsterType.FuckingPumpkin, new Queue<Entity>()},
    };
    
    private List<Entity> _sceneEntities = new();

    private void Awake()
    {
        if (Instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (var key in _bufferEntities.Keys)
        {
            var en = key switch
            {
                MonsterType.Bat => _bat,
                MonsterType.Zombie => _zombie,
                MonsterType.FuckingPumpkin => _pumpkin,
                _ => throw new ArgumentOutOfRangeException()
            };
            for (int i = 0; i < START_MOSTERS_COUNT; i++)
            {
                var entity = Instantiate(en, new Vector2(10000, 10000), Quaternion.identity);
                entity.gameObject.SetActive(false);
                _bufferEntities[key].Enqueue(entity);
            }
        }
    }
    
    public static Entity Instantiate<T>(T en, Vector2 pos) where T:Entity
    {
        if (Instance._bufferEntities[en.Type].Count > 0)
        {
            var entity =  Instance._bufferEntities[en.Type].Dequeue();
            entity.gameObject.SetActive(true);
            entity.transform.position = pos;
            Instance._sceneEntities.Add(entity);
            return entity;
        }
        
        var entity2 = MonoBehaviour.Instantiate(en, pos, Quaternion.identity);
        Instance._sceneEntities.Add(entity2);
        return entity2;
    }

    public static void Destroy(Entity en)
    {
        en.ResetHp();
        en.gameObject.SetActive(false);
        en.transform.position = new Vector3(10000, 10000);
        Instance._sceneEntities.Remove(en);
        Instance._bufferEntities[en.Type].Enqueue(en);
    }

    public IReadOnlyList<Entity> GetAllEntities()
    {
        return _sceneEntities;
    }
}
