using UnityEngine;

public sealed class PlayerListView : MonoBehaviour
{
    [SerializeField]
    private PlayerListElement[] elements;

    private PlayerDataContainer container;

    public void Init(PlayerDataContainer playerDataContainer) {
        container = playerDataContainer;
        foreach (var element in elements) {
            element.Init();
        }
    }

    public void OnUpdate() {
        for (int i = 0; i < 6; i++) {
            int playerId = (i + 5) % 6;
            if (container.TryGetPlayerData(playerId, out var data)) {
                elements[i].Show(data.NickNameValue);
            } else {
                elements[i].Hide();
            }
        }
    }

    public void ShowAddScore() {
        for (int i = 0; i < 6; i++) {
            int playerId = (i + 5) % 6;
            if (container.TryGetPlayerData(playerId, out var data)) {
                elements[i].ShowAddScore(data.AddScore);
            }
        }
    }

    public void HideAddScore() {
        foreach (var element in elements) {
            element.HideAddScore();
        }
    }

    public void StartCountUpScore() {
        for (int i = 0; i < 6; i++) {
            int playerId = (i + 5) % 6;
            if (container.TryGetPlayerData(playerId, out var data)) {
                elements[i].SetScore(data.Score);
            }
        }
    }
}
