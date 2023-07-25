using TMPro;
using UnityEngine;

public sealed class PlaySceneDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerLabel;
    [SerializeField]
    private TextMeshProUGUI timeLabel;
    [SerializeField]
    private Canvas timeLabelCanvas;
    [SerializeField]
    private TextMeshProUGUI phaseLabel;
    [SerializeField]
    private Canvas phaseLabelCanvas;
    [SerializeField]
    private ChatSystem chatSystem;
    [SerializeField]
    private PlayerListView playerListView;
    [SerializeField]
    private LobbyView lobbyView;
    [SerializeField]
    private ImitationView imitationView;
    [SerializeField]
    private InsightView insightView;
    [SerializeField]
    private JudgeView judgeView;
    [SerializeField]
    private ResultView resultView;

    public ChatSystem ChatSystem => chatSystem;
    public PlayerListView PlayerListView => playerListView;
    public LobbyView LobbyView => lobbyView;
    public ImitationView ImitationView => imitationView;
    public InsightView InsightView => insightView;
    public JudgeView JudgeView => judgeView;
    public ResultView ResultView => resultView;

    private int currentTimeValue = -1;

    public void Init(PlayerData data, PlayerDataContainer container) {
        headerLabel.text = string.Empty;
        timeLabel.text = string.Empty;
        timeLabelCanvas.enabled = false;
        phaseLabelCanvas.enabled = false;
        chatSystem.Init(data);
        playerListView.Init(container);
        lobbyView.Init();
        imitationView.Init(data);
        insightView.Init(data);
        judgeView.Init();
        resultView.Init();
    }

    public void OnUpdate() {
        playerListView.OnUpdate();
    }

    public void SetHeaderLabel(string text) {
        headerLabel.text = text;
    }

    public void SetTimeLabel(float? seconds) {
        if (seconds.HasValue) {
            int value = Mathf.CeilToInt(seconds.Value);
            if (value > 0) {
                if (value != currentTimeValue) {
                    currentTimeValue = value;
                    timeLabel.SetText("{0}", value);
                }
                return;
            }
        }

        if (currentTimeValue > 0) {
            currentTimeValue = -1;
            timeLabel.text = string.Empty;
        }
    }

    public void ShowTimeLabel() {
        timeLabelCanvas.enabled = true;
    }

    public void HideTimeLabel() {
        timeLabelCanvas.enabled = false;
    }

    public void ShowPhaseLabel(string text) {
        phaseLabel.text = text;
        phaseLabelCanvas.enabled = true;
        GameManager.Sound.PlayPhase();
    }

    public void HidePhaseLabel() {
        phaseLabelCanvas.enabled = false;
    }
}
