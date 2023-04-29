using System;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Waves/Horde")]
public class HordeWave : Wave
{
    [SerializeField] private Entity _entity;
    [SerializeField] private int _count;
    [SerializeField] private float _density;
    public override void Spawn(Vector2 position, float radius)
    {
        var randomAngle = Random.Range(0, 2 * Mathf.PI);
        var point = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle))*radius + position;
        for (int i = 0; i < _count; i++)
        {
            var pos = point + _count / _density * Random.insideUnitCircle;
            HeinzDoofenshmirtzInstantinator.Instantiate(_entity, pos);
        }
    }
}
