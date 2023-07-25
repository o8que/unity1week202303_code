using Fusion;

public sealed class PlayPhaseManager : NetworkBehaviour
{
    [Networked]
    public PlayPhase CurrentPhase { get; set; }
    [Networked]
    public TickTimer Timer { get; set; }
    [Networked]
    public int CurrentRound { get; set; }

    private LobbyPhase lobbyPhase;
    private ImitationPhase imitationPhase;
    private InsightPhase insightPhase;
    private JudgePhase judgePhase;
    private ResultPhase resultPhase;
    private bool initialized;
    private IPlayPhase currentPhase;

    public void Init(PlayerData localData, PlayerDataContainer container, QAManager qaManager, PlaySceneDisplay display) {
        lobbyPhase = new LobbyPhase(this, localData, container, qaManager, display);
        imitationPhase = new ImitationPhase(this, localData, container, qaManager, display);
        insightPhase = new InsightPhase(this, localData, container, qaManager, display);
        judgePhase = new JudgePhase(this, localData, container, qaManager, display);
        resultPhase = new ResultPhase(this, localData, container, qaManager, display);
        initialized = true;
    }

    public override void FixedUpdateNetwork() {
        if (!initialized) { return; }
        if (CurrentPhase != currentPhase?.Type) {
            currentPhase?.Exit();
            currentPhase = CurrentPhase switch {
                PlayPhase.Lobby => lobbyPhase,
                PlayPhase.Imitation => imitationPhase,
                PlayPhase.Insight => insightPhase,
                PlayPhase.Judge => judgePhase,
                _ => resultPhase
            };
            currentPhase.Enter();
        }
        currentPhase.FixedUpdateNetwork();
    }

    public void Dispose() {
        currentPhase?.Dispose();
    }
}

public interface IPlayPhase
{
    PlayPhase Type { get; }
    void Enter();
    void FixedUpdateNetwork();
    void Exit();
    void Dispose();
}

public enum PlayPhase
{
    Lobby,
    Imitation,
    Insight,
    Judge,
    Result
}
