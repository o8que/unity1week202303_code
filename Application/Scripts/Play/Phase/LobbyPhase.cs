using Fusion;

public sealed class LobbyPhase : IPlayPhase
{
    private readonly PlayPhaseManager context;
    private readonly PlayerData localData;
    private readonly PlayerDataContainer container;
    private readonly QAManager qaManager;
    private readonly PlaySceneDisplay display;

    public LobbyPhase(PlayPhaseManager context, PlayerData localData, PlayerDataContainer container, QAManager qaManager, PlaySceneDisplay display) {
        this.context = context;
        this.localData = localData;
        this.container = container;
        this.qaManager = qaManager;
        this.display = display;
    }

    PlayPhase IPlayPhase.Type => PlayPhase.Lobby;

    void IPlayPhase.Enter() {
        display.SetHeaderLabel("ロビー");
        display.ShowTimeLabel();
        display.ChatSystem.Show();
        display.LobbyView.Show();
        display.LobbyView.SetLobbyLabel("参加者4人以上\nでテストが開始されます");
    }

    void IPlayPhase.FixedUpdateNetwork() {
        display.SetTimeLabel(context.Timer.RemainingTime(context.Runner));

        if (!context.Runner.IsServer) { return; }

        if (!context.Timer.IsRunning && container.Count >= Config.StartPlayers) {
            context.Timer = TickTimer.CreateFromSeconds(context.Runner, Config.LobbyTime);
            // 問題設定
            qaManager.SetQuestions();
        }

        if (context.Timer.Expired(context.Runner)) {
            context.CurrentPhase = PlayPhase.Imitation;
            context.Timer = TickTimer.CreateFromSeconds(context.Runner, Config.ImitationTime);
            context.CurrentRound = 1;

            // セッションのクローズ
            context.Runner.SessionInfo.IsOpen = false;
            // チーム分け
            container.SetTeam();
        }
    }

    void IPlayPhase.Exit() {
        display.SetHeaderLabel(string.Empty);
        display.HideTimeLabel();
        display.ChatSystem.Hide();
        display.LobbyView.Hide();
    }

    void IPlayPhase.Dispose() {}
}
