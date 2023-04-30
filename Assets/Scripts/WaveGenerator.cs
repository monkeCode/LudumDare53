using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Wave[] _specialWaves;
    [SerializeField] private Wave[] _bossWave;
    [SerializeField] private float _radius;
    [FormerlySerializedAs("_wayDelay")] [SerializeField] private float _delay;
    [SerializeField] private float _specialDelay;
    [SerializeField] private float _bossDelay;
    private float _lastTime;
    private float _activeSpecialDelay;
    private float _activeBossDelay;

    public static float Timer { get; private set; }

    private void Start()
    {
        _activeBossDelay = _bossDelay;
        _activeSpecialDelay = _specialDelay;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer - _lastTime >= _delay)
        {
            Wave[] selectedWaves;
            if (_activeBossDelay <= 0)
            {
                selectedWaves = _bossWave;
                _activeBossDelay = _bossDelay;
            }
            else if (_activeSpecialDelay <= 0)
            {
                selectedWaves = _specialWaves;
                _activeSpecialDelay = _specialDelay;
            }
            else
            {
                selectedWaves = _waves;
            }
            var i = Random.Range(0, selectedWaves.Length);
            selectedWaves[i].Spawn(Player.Player.Instance.transform.position, _radius);
            _lastTime = Timer;
            
        }
        _activeBossDelay -= Time.deltaTime;
        _activeSpecialDelay -= Time.deltaTime;
    }

}
