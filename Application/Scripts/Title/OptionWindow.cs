using Ore2Lib;
using UnityEngine;
using UnityEngine.UI;

public sealed class OptionWindow : MonoBehaviour
{
    [SerializeField]
    private Slider audioVolumeSlider;
    [SerializeField]
    private Button closeButton;

    public void Open() {
        audioVolumeSlider.maxValue = SaveData.MaxVolume;
        audioVolumeSlider.value = GameManager.SaveData.MasterVolume;
        audioVolumeSlider.onValueChanged.AddListener(OnAudioVolumeSliderValueChanged);
        closeButton.onClick.AddListener(OnClickCloseButton);
        GameManager.Sound.PlayClick();
    }

    private void OnAudioVolumeSliderValueChanged(float value) {
        GameManager.SaveData.MasterVolume = value;
        GameManager.AudioVolume.SetMasterVolume(GameManager.SaveData.MasterVolumeNormalized);
        GameManager.Sound.PlayClick();
    }

    private void OnClickCloseButton() {
        GameManager.SaveData.Save();
        Destroy(gameObject);
        GameManager.Sound.PlayClick();
    }
}
