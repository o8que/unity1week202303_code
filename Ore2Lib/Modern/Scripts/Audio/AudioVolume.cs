using UnityEngine;
using UnityEngine.Audio;

namespace Ore2Lib
{
    public sealed class AudioVolume : MonoBehaviour
    {
        private const string MasterVolumeName = "MasterVolume";
        private const string MusicVolumeName = "MusicVolume";
        private const string SoundVolumeName = "SoundVolume";

        [SerializeField] private AudioMixer audioMixer;

        public void Init(float masterVolume, float musicVolume = 1f, float soundVolume = 1f) {
            SetMasterVolume(masterVolume);
            SetMusicVolume(musicVolume);
            SetSoundVolume(soundVolume);
        }

        private void SetVolume(string exposedName, float value) {
            float decibel = 20f * Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f));
            audioMixer.SetFloat(exposedName, decibel);
        }

        public void SetMasterVolume(float value) {
            SetVolume(MasterVolumeName, value);
        }

        public void SetMusicVolume(float value) {
            SetVolume(MusicVolumeName, value);
        }

        public void SetSoundVolume(float value) {
            SetVolume(SoundVolumeName, value);
        }
    }
}
