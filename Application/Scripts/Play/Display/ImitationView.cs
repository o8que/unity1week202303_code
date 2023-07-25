using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ImitationView : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI questionLabel;
    [SerializeField]
    private TMP_InputField answerInputField;
    [SerializeField]
    private Button answerButton;
    [SerializeField]
    private TextMeshProUGUI answeredLabel;

    private PlayerData localPlayerData;

    public void Init(PlayerData data) {
        localPlayerData = data;

        canvas.enabled = false;
        answerInputField.onValueChanged.AddListener(OnAnswerInputFieldValueChanged);
        answerButton.interactable = false;
        answerButton.onClick.AddListener(OnClickAnswerButton);
    }

    private void OnAnswerInputFieldValueChanged(string value) {
        answerButton.interactable = value.Length > 0;
        if (value.Length > 50) {
            answerInputField.text = value[..50];
        }
    }

    public void OnClickAnswerButton() {
        // 二重送信されないようにする
        if (answerInputField.interactable) {
            localPlayerData.RpcSendAnswer(answerInputField.text);
            GameManager.Sound.PlayClick();
        }
        answerInputField.interactable = false;
        answerButton.interactable = false;
    }

    public void Show() {
        canvas.enabled = true;
        answerInputField.text = string.Empty;
        answerButton.interactable = false;
    }

    public void Hide() {
        answerInputField.interactable = true;
        canvas.enabled = false;
    }

    public void SetQuestionLabel(string text) {
        questionLabel.text = text;
    }

    public void SetAnsweredLabel(int answered, int players) {
        answeredLabel.SetText("回答済み {0}/{1}", answered, players);
    }
}
