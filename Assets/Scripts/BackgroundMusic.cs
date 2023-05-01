using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BackgroundMusic: MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        private void Start()
        {
            Player.Player.Instance.onDeath.AddListener(StopMusic);
        }
        
        private void StopMusic()
        {
            audioSource.Stop();
        }
    }
}