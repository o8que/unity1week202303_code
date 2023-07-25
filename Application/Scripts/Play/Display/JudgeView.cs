using System.Collections.Generic;
using UnityEngine;

public sealed class JudgeView : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private ChoiceView choiceView0;
    [SerializeField]
    private ChoiceView choiceView1;

    public void Init() {
        canvas.enabled = false;
        choiceView0.InitForJudge();
        choiceView1.InitForJudge();
    }

    public void Show() {
        canvas.enabled = true;
    }

    public void Hide() {
        canvas.enabled = false;
    }

    public void SetQuestionLabel(string text0, string text1) {
        choiceView0.SetQuestionLabel(text0);
        choiceView1.SetQuestionLabel(text1);
    }

    public void SetAnswerPanel(bool team, int index, string imitatorNickName, string answer, List<string> insighterNames) {
        var target = (team) ? choiceView1 : choiceView0;
        target.SetAnswerPanelForJudge(index, imitatorNickName, answer, insighterNames);
    }

    public void ShowImitatorNickName() {
        choiceView0.ShowImitatorNickName();
        choiceView1.ShowImitatorNickName();
    }
}
