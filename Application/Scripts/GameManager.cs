using DG.Tweening;
using Ore2Lib;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static SaveData SaveData { get; private set; }
    public static SceneNavigator Scene { get; private set; }
    public static AudioVolume AudioVolume { get; private set; }
    public static MusicPlayer Music { get; private set; }
    public static SoundPlayer Sound { get; private set; }

    private void Start() {
        Application.targetFrameRate = 60;
        DOTween.Init();

        SaveData = new SaveData();
        Scene = new SceneNavigator();
        AudioVolume = GetComponentInChildren<AudioVolume>();
        Music = GetComponentInChildren<MusicPlayer>();
        Sound = GetComponentInChildren<SoundPlayer>();

        AudioVolume.Init(SaveData.MasterVolumeNormalized);
        Scene.LaunchFrom(new TitleScene.With());

        // ChatTest();
    }

    private async void ChatTest() {
        string message = "あなたは人間ですか？", suffix = "30文字以内で答えて";
        message = message + suffix;
        print(message);
        var result = await ChatGPT.SendAsync(message, 4);
        print(result.ToJoinedString());
    }
}
