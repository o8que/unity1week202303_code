using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class AnswerPanel : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private TextMeshProUGUI answerLabel;
    [SerializeField]
    private AnswerNamePanel imitator;
    [SerializeField]
    private AnswerNamePanel[] insighters;

    public int ImitatorId { get; private set; }
    public bool IsSelected => toggle.isOn;

    public void InitForInsight(ToggleGroup toggleGroup) {
        toggle.isOn = false;
        toggle.group = toggleGroup;
        imitator.Hide();
        foreach (var insighter in insighters) {
            insighter.Hide();
        }
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool value) {
        if (value) {
            GameManager.Sound.PlayClick();
        }
    }

    public void InitForJudge() {
        toggle.isOn = false;
        toggle.interactable = false;
    }

    public void SetAnswerForInsight(int id, string answer) {
        ImitatorId = id;
        answerLabel.text = answer;
    }

    public void SetAnswerForJudge(string imitatorNickName, string answer, List<string> insighterNames) {
        answerLabel.text = answer;
        imitator.SetName(imitatorNickName);
        imitator.Hide();
        for (int i = 0; i < insighters.Length; i++) {
            if (i < insighterNames.Count) {
                insighters[i].SetName(insighterNames[i]);
                insighters[i].Show();
            } else {
                insighters[i].Hide();
            }
        }
    }

    public void HideForInsight() {
        toggle.isOn = false;
    }

    public void ShowImitatorNickName() {
        imitator.Show();
    }
}
