using TMPro;
using UnityEngine;

public sealed class AnswerNamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    public void SetName(string nickName) {
        nameLabel.text = nickName;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
