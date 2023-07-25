using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ChoiceView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI questionLabel;
    [SerializeField]
    private ToggleGroup toggleGroup;
    [SerializeField]
    private AnswerPanel[] answerPanels;

    public bool Interactable => canvasGroup.interactable;

    public void InitForInsight() {
        foreach (var answerPanel in answerPanels) {
            answerPanel.InitForInsight(toggleGroup);
        }
    }

    public void InitForJudge() {
        foreach (var answerPanel in answerPanels) {
            answerPanel.InitForJudge();
        }
    }

    public void SetQuestionLabel(string text) {
        questionLabel.text = text;
    }

    public void SetAnswerPanelForInsight(int index, int imitatorId, string answer) {
        answerPanels[index].SetAnswerForInsight(imitatorId, answer);
    }

    public void SetAnswerPanelForJudge(int index, string imitatorNickName, string answer, List<string> insighterNames) {
        answerPanels[index].SetAnswerForJudge(imitatorNickName, answer, insighterNames);
    }

    public void ShowForInsight() {
        canvasGroup.interactable = true;
    }

    public void HideForInsight() {
        foreach (var answerPanel in answerPanels) {
            answerPanel.HideForInsight();
        }
    }

    public int GetSelectedImitatorId() {
        canvasGroup.interactable = false;
        foreach (var answerPanel in answerPanels) {
            if (answerPanel.IsSelected) {
                return answerPanel.ImitatorId;
            }
        }
        return Config.NobodyId;
    }

    public void ShowImitatorNickName() {
        foreach (var answerPanel in answerPanels) {
            answerPanel.ShowImitatorNickName();
        }
    }
}
