using UnityEngine;

namespace Ore2Lib
{
    public sealed class SaveData
    {
        private const string PlayerNameKey = "PlayerName";
        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string SoundVolumeKey = "SoundVolume";
        private const string HorizontalSensitivityKey = "HorizontalSensitivity";
        private const string VerticalSensitivityKey = "VerticalSensitivity";
        private const string HorizontalInvertKey = "HorizontalInvert";
        private const string VerticalInvertKey = "VerticalInvert";

        public const float MaxVolume = 10f;
        public const float MaxSensitivity = 15f;
        private const float InitialVolume = 10f;
        private const float InitialSensitivity = 7f;

        private string playerName;
        public string PlayerName {
            get => playerName;
            set {
                playerName = value;
                PlayerPrefs.SetString(PlayerNameKey, playerName);
                PlayerPrefs.Save();
            }
        }

        private float masterVolume;
        public float MasterVolumeNormalized => masterVolume / MaxVolume;
        public float MasterVolume {
            get => masterVolume;
            set => masterVolume = Mathf.Clamp(value, 0f, MaxVolume);
        }

        private float musicVolume;
        public float MusicVolumeNormalized => musicVolume / MaxVolume;
        public float MusicVolume {
            get => musicVolume;
            set => musicVolume = Mathf.Clamp(value, 0f, MaxVolume);
        }

        private float soundVolume;
        public float SoundVolumeNormalized => soundVolume / MaxVolume;
        public float SoundVolume {
            get => soundVolume;
            set => soundVolume = Mathf.Clamp(value, 0f, MaxVolume);
        }

        private float horizontalSensitivity;
        public float HorizontalSpeed => ((HorizontalInvert) ? -1f : 1f) * (horizontalSensitivity * 0.125f + 0.125f);
        public float HorizontalSensitivity {
            get => horizontalSensitivity;
            set => horizontalSensitivity = Mathf.Clamp(value, 0f, MaxSensitivity);
        }

        private float verticalSensitivity;
        public float VerticalSpeed => ((VerticalInvert) ? -1f : 1f) * (verticalSensitivity * 0.125f + 0.125f);
        public float VerticalSensitivity {
            get => verticalSensitivity;
            set => verticalSensitivity = Mathf.Clamp(value, 0f, MaxSensitivity);
        }

        public bool HorizontalInvert { get; set; }
        public bool VerticalInvert { get; set; }

        public SaveData() {
            playerName = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
            masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, InitialVolume);
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, InitialVolume);
            soundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, InitialVolume);
            horizontalSensitivity = PlayerPrefs.GetFloat(HorizontalSensitivityKey, InitialSensitivity);
            verticalSensitivity = PlayerPrefs.GetFloat(VerticalSensitivityKey, InitialSensitivity);
            HorizontalInvert = PlayerPrefs.GetInt(HorizontalInvertKey, 0) > 0;
            VerticalInvert = PlayerPrefs.GetInt(VerticalInvertKey, 0) > 0;
        }

        public void Save() {
            PlayerPrefs.SetFloat(MasterVolumeKey, MasterVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
            PlayerPrefs.SetFloat(SoundVolumeKey, SoundVolume);
            PlayerPrefs.SetFloat(HorizontalSensitivityKey, HorizontalSensitivity);
            PlayerPrefs.SetFloat(VerticalSensitivityKey, VerticalSensitivity);
            PlayerPrefs.SetInt(HorizontalInvertKey, (HorizontalInvert) ? 1 : 0);
            PlayerPrefs.SetInt(VerticalInvertKey, (VerticalInvert) ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
