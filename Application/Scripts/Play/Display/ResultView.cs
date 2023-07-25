using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ResultView : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI resultLabel;
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

    public void SetResultLabel(string text) {
        resultLabel.text = text;
    }
}
