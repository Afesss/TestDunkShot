using System;
using Infrastructure.Services;
using UnityEngine;

namespace Baskets
{
    public class TorusColliderDetected : MonoBehaviour
    {
        [SerializeField] private AudioClip _boundsClip;
        private ISoundService _sound;

        public void Construct(ISoundService soundService)
        {
            _sound = soundService;
        }
        
        public bool Detected { get; private set; } = false;
        
        private void OnCollisionEnter(Collision collision)
        {
            _sound.Play(_boundsClip);
            Detected = true;
        }
    }
}
