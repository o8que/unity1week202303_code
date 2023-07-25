using UnityEngine;
using UnityEngine.UI;

public sealed class ManualWindow : MonoBehaviour
{
    [SerializeField]
    private Button closeButton;

    public void Open() {
        closeButton.onClick.AddListener(OnClickCloseButton);
        GameManager.Sound.PlayClick();
    }

    private void OnClickCloseButton() {
        Destroy(gameObject);
        GameManager.Sound.PlayClick();
    }
}
