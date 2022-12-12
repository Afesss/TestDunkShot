using UnityEngine;

namespace Infrastructure.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        [SerializeField] private AudioSource _audioSource;

        public void MuteSound(bool value)
        {
            _audioSource.mute = value;
        }

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}