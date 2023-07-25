using UnityEngine;

public sealed class InsightView : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private ChoiceView choiceView;

    private PlayerData localPlayerData;
    private AnswerPanel selectedAnswerPanel;

    public void Init(PlayerData data) {
        localPlayerData = data;

        canvas.enabled = false;
        choiceView.InitForInsight();
    }

    public void Show() {
        choiceView.ShowForInsight();
        canvas.enabled = true;
    }

    public void Hide() {
        choiceView.HideForInsight();
        canvas.enabled = false;
    }

    public void SetQuestionLabel(string text) {
        choiceView.SetQuestionLabel(text);
    }

    public void SetAnswerPanel(int index, int imitatorId, string answer) {
        choiceView.SetAnswerPanelForInsight(index, imitatorId, answer);
    }

    public void SendSelectedImitatorId() {
        if (choiceView.Interactable) {
            int imitatorId = choiceView.GetSelectedImitatorId();
            localPlayerData.RpcSelectImitatorId(imitatorId);
        }
    }
}
