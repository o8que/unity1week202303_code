using DG.Tweening;
using Fusion;

public sealed class InsightPhase : IPlayPhase
{
    private readonly PlayPhaseManager context;
    private readonly PlayerData localData;
    private readonly PlayerDataContainer container;
    private readonly QAManager qaManager;
    private readonly PlaySceneDisplay display;

    public InsightPhase(PlayPhaseManager context, PlayerData localData, PlayerDataContainer container, QAManager qaManager, PlaySceneDisplay display) {
        this.context = context;
        this.localData = localData;
        this.container = container;
        this.qaManager = qaManager;
        this.display = display;
    }

    PlayPhase IPlayPhase.Type => PlayPhase.Insight;

    void IPlayPhase.Enter() {
        var questions = (localData.Team) ? qaManager.Question0 : qaManager.Question1;
        display.InsightView.SetQuestionLabel(Config.Questions[questions.Get(context.CurrentRound - 1)]);

        // 各パネルに模倣者IDと回答文章を設定する
        var imitators = (localData.Team) ? qaManager.ImitatorId0 : qaManager.ImitatorId1;
        var aiAnswers = (localData.Team) ? qaManager.AiAnswer0 : qaManager.AiAnswer1;
        for (int i = 0; i < 4; i++) {
            int imitatorId = imitators.Get(i);
            string answer = (imitatorId >= 0) ? container.GetAnswer(imitatorId) : aiAnswers.Get(-(imitatorId + 1)).Value;
            display.InsightView.SetAnswerPanel(i, imitatorId, answer);
        }

        // 遷移アニメーション
        DOTween.Sequence()
            .AppendInterval(1f)
            .AppendCallback(() => display.ShowPhaseLabel("看破フェーズ"))
            .AppendInterval(2f)
            .AppendCallback(() => display.HidePhaseLabel())
            .AppendInterval(0.5f)
            .AppendCallback(() => {
                display.SetHeaderLabel($"ラウンド {context.CurrentRound}：AIが生成した回答を選択せよ");
                display.ShowTimeLabel();
                display.InsightView.Show();
            })
            .Play();
    }

    void IPlayPhase.FixedUpdateNetwork() {
        display.SetTimeLabel(context.Timer.RemainingTime(context.Runner));

        // 時間切れ時に選択した回答を送信する
        if (context.Timer.Expired(context.Runner)) {
            display.InsightView.SendSelectedImitatorId();
        }

        if (!context.Runner.IsServer) { return; }

        // 時間切れで遷移
        if (context.Timer.Expired(context.Runner)) {
            context.CurrentPhase = PlayPhase.Judge;
            context.Timer = TickTimer.CreateFromSeconds(context.Runner, Config.JudgeTime);
        }
    }

    void IPlayPhase.Exit() {
        display.SetHeaderLabel(string.Empty);
        display.HideTimeLabel();
        display.InsightView.Hide();
    }

    void IPlayPhase.Dispose() {}
}
