using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public sealed class ChatSystem : MonoBehaviour
{
    [SerializeField]
    private Button chatButton;
    [SerializeField]
    private ProceduralImage chatButtonBadge;
    [SerializeField]
    private Canvas chatWindowCanvas;
    [SerializeField]
    private TextMeshProUGUI[] chatMessages;
    [SerializeField]
    private TMP_InputField chatInputField;
    [SerializeField]
    private Button sendButton;

    private PlayerData localPlayerData;

    private readonly Queue<string> messageQueue = new(12);

    public void Init(PlayerData data) {
        localPlayerData = data;

        for (int i = 0; i < chatMessages.Length; i++) {
            messageQueue.Enqueue(" ");
        }
        chatButton.onClick.AddListener(OnClickChatButton);
        chatButtonBadge.enabled = false;
        chatWindowCanvas.enabled = true;
        int j = 0;
        foreach (string m in messageQueue) {
            chatMessages[j++].text = m;
        }
        chatInputField.onValueChanged.AddListener(OnChatInputFieldValueChanged);
        sendButton.interactable = false;
        sendButton.onClick.AddListener(OnClickSendButton);
    }

    public void Show() {
        chatWindowCanvas.enabled = true;
        chatButtonBadge.enabled = false;
    }

    public void Hide() {
        chatWindowCanvas.enabled = false;
    }

    private void OnClickChatButton() {
        if (chatWindowCanvas.enabled) {
            Hide();
        } else {
            Show();
        }
        GameManager.Sound.PlayClick();
    }

    private void OnChatInputFieldValueChanged(string value) {
        sendButton.interactable = value.Length > 0;
    }

    private void OnClickSendButton() {
        localPlayerData.RpcSendChatMessage(chatInputField.text);
        chatInputField.text = string.Empty;
        GameManager.Sound.PlayClick();
    }

    public void ReceiveChatMessage(string nickName, string message) {
        messageQueue.Dequeue();
        messageQueue.Enqueue($"{nickName}：{message}");
        int i = 0;
        foreach (string m in messageQueue) {
            chatMessages[i++].text = m;
        }

        if (!chatWindowCanvas.enabled) {
            chatButtonBadge.enabled = true;
        }
        GameManager.Sound.PlayChat();
    }
}
