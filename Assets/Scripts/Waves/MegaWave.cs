using Entities;
using UnityEngine;

namespace Waves
{   
    [CreateAssetMenu(menuName = "Waves/MegaWave")]
    public class MegaWave : Wave
    {
        [SerializeField] private float _bigRadius;
        [SerializeField] private int _count;
        [SerializeField] private Entity _entity;

        public override void Spawn(Vector2 position, float radius)
        {
            for (int i = 0; i < _count; i++)
            {
                var angle = Random.Range(0, Mathf.PI * 2);
                var linPos = Random.Range(0, _bigRadius);
                var pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * (radius + linPos) + position;
                HeinzDoofenshmirtzInstantinator.Instantiate(_entity, pos);
            }
        }
    }
}
