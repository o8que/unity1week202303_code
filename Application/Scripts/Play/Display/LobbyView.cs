using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LobbyView : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI lobbyLabel;
    [SerializeField]
    private Button leaveButton;

    public void Init() {
        canvas.enabled = false;
        leaveButton.onClick.AddListener(OnClickLeaveButton);
    }

    private void OnClickLeaveButton() {
        GameManager.Scene.NavigateTo(new TitleScene.With());
        GameManager.Sound.PlayClick();
    }

    public void Show() {
        canvas.enabled = true;
    }

    public void Hide() {
        canvas.enabled = false;
    }

    public void SetLobbyLabel(string text) {
        lobbyLabel.text = text;
    }
}
