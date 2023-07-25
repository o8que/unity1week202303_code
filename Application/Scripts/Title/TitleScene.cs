using System.Threading;
using Cysharp.Threading.Tasks;
using Ore2Lib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class TitleScene : Scene<TitleScene.With>
{
    public sealed class With : SceneParameter
    {
        public string Message { get; }

        public With(string message = "") : base("Title") {
            Message = message;
        }
    }

    [SerializeField]
    private TMP_InputField nameInputField;
    [SerializeField]
    private TextMeshProUGUI errorLabel;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button manualButton;
    [SerializeField]
    private Button optionButton;
    [SerializeField]
    private TextMeshProUGUI versionLabel;
    [Header("Canvas")]
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private ManualWindow manualWindowPrefab;
    [SerializeField]
    private OptionWindow optionWindowPrefab;

    protected override UniTask Enter(CancellationToken token) {
        nameInputField.text = GameManager.SaveData.PlayerName;
        errorLabel.text = Parameter.Message;
        playButton.onClick.AddListener(OnClickPlayButton);
        manualButton.onClick.AddListener(() => Instantiate(manualWindowPrefab, canvas.transform).Open());
        optionButton.onClick.AddListener(() => Instantiate(optionWindowPrefab, canvas.transform).Open());
        versionLabel.text = $"ver {Application.version}";

        return UniTask.CompletedTask;
    }

    private void OnClickPlayButton() {
        playButton.interactable = false;
        GameManager.SaveData.PlayerName = nameInputField.text;
        GameManager.Scene.NavigateTo(new PlayScene.With());
        GameManager.Sound.PlayClick();
    }
}
