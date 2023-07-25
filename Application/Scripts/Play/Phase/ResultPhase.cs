using DG.Tweening;

public sealed class ResultPhase : IPlayPhase
{
    private readonly PlayPhaseManager context;
    private readonly PlayerData localData;
    private readonly PlayerDataContainer container;
    private readonly QAManager qaManager;
    private readonly PlaySceneDisplay display;

    public ResultPhase(PlayPhaseManager context, PlayerData localData, PlayerDataContainer container, QAManager qaManager, PlaySceneDisplay display) {
        this.context = context;
        this.localData = localData;
        this.container = container;
        this.qaManager = qaManager;
        this.display = display;
    }

    PlayPhase IPlayPhase.Type => PlayPhase.Result;

    void IPlayPhase.Enter() {
        string winnerName = container.GetWinnerName();
        display.ResultView.SetResultLabel($"{winnerName}\nはAIと判定された");

        // 遷移アニメーション
        DOTween.Sequence()
            .AppendInterval(1f)
            .AppendCallback(() => display.ShowPhaseLabel("テスト結果"))
            .AppendInterval(2f)
            .AppendCallback(() => display.HidePhaseLabel())
            .AppendInterval(0.5f)
            .AppendCallback(() => {
                display.SetHeaderLabel("テスト結果");
                display.ShowTimeLabel();
                display.ChatSystem.Show();
                display.ResultView.Show();
                GameManager.Sound.PlayResult();
            })
            .Play();
    }

    void IPlayPhase.FixedUpdateNetwork() {
        display.SetTimeLabel(context.Timer.RemainingTime(context.Runner));

        if (context.Timer.Expired(context.Runner)) {
            GameManager.Scene.NavigateTo(new TitleScene.With());
        }
    }

    void IPlayPhase.Exit() {}

    void IPlayPhase.Dispose() {}
}
