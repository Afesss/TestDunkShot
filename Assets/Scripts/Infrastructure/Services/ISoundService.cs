using UnityEngine;

namespace Infrastructure.Services
{
    public interface ISoundService
    {
        public void MuteSound(bool value);
        public void Play(AudioClip clip);
    }
}