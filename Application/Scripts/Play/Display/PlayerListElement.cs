using TMPro;
using UnityEngine;

public sealed class PlayerListElement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    [SerializeField]
    private TextMeshProUGUI scoreLabel;
    [SerializeField]
    private TextMeshProUGUI addScoreLabel;

    private string currentName;
    private int currentScore;
    private int targetScore;

    public void Init() {
        currentName = string.Empty;
        currentScore = 0;
        targetScore = 0;
        nameLabel.text = string.Empty;
        scoreLabel.SetText("{0}", 0);
        addScoreLabel.enabled = false;
    }

    public void Show(string nickName) {
        if (nickName != currentName) {
            currentName = nickName;
            nameLabel.text = nickName;
        }

        if (currentScore < targetScore) {
            currentScore = Mathf.Min(currentScore + 3, targetScore);
            scoreLabel.SetText("{0}", currentScore);
        }

        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
    }

    public void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public void ShowAddScore(int addScore) {
        if (addScore > 0) {
            addScoreLabel.SetText("+{0}", addScore);
            addScoreLabel.enabled = true;
        }
    }

    public void HideAddScore() {
        addScoreLabel.enabled = false;
    }

    public void SetScore(int score) {
        targetScore = score;
    }
}
