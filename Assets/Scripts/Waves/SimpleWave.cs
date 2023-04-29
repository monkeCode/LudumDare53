using UnityEngine;

[CreateAssetMenu(menuName = "Waves/Simple")]
public class SimpleWave : Wave
{
    [SerializeField] private Entity _entity;
    [SerializeField] private int _count;
    
    public override void Spawn(Vector2 position, float radius)
    {
        for (int i = 0; i < _count; i++)
        {
            var spawnPos = new Vector2(Mathf.Cos((float)i / _count * 2 * Mathf.PI), Mathf.Sin((float)i / _count * 2 * Mathf.PI)) *
                radius + position;
            Instantiate(_entity, spawnPos, Quaternion.identity);
        }
    }
}
