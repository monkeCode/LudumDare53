using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private Wave _wave;
    [SerializeField] private float _radius;
    void Start()
    {
        _wave.Spawn(Player.Player.Instance.transform.position, _radius);
    }
    
    void Update()
    {
        
    }
}
