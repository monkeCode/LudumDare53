using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Wave[] _specialWaves;
    [SerializeField]  private Wave[] _bossWave;
    [SerializeField] private float _radius;
    [FormerlySerializedAs("_wayDelay")] [SerializeField] private float _delay;
    private float _lastTime;

    public static float Timer { get; private set; }
    
    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer - _lastTime >= _delay)
        {
            if (Random.Range(0, 100) <= 1)
            {
                var i = Random.Range(0, _specialWaves.Length);
                _specialWaves[i].Spawn(Player.Player.Instance.transform.position, _radius);
            }
            else
            {
                var i = Random.Range(0, _waves.Length);
                _waves[i].Spawn(Player.Player.Instance.transform.position, _radius);
            }

            _lastTime = Timer;
        }
    }

}
