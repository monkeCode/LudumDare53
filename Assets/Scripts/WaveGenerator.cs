using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Wave[] _specialWaves;
    [SerializeField] private float _radius;
    [FormerlySerializedAs("_wayDelay")] [SerializeField] private float _delay;
    void Start()
    {
        StartCoroutine(Spawner());
    }
    
    IEnumerator Spawner()
    {
        while (true)
        {
            
            if (Random.Range(0, 100) <= 15)
            {
                var i = Random.Range(0, _specialWaves.Length);
                _specialWaves[i].Spawn(Player.Player.Instance.transform.position, _radius);
            }
            else
            {
                var i = Random.Range(0, _waves.Length);
                _waves[i].Spawn(Player.Player.Instance.transform.position, _radius);
            }
            yield return new WaitForSeconds(_delay);
        }
    }
}
