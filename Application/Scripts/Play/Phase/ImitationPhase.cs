using DG.Tweening;
using Fusion;

public sealed class ImitationPhase : IPlayPhase
{
    private readonly PlayPhaseManager context;
    private readonly PlayerData localData;
    private readonly PlayerDataContainer container;
    private readonly QAManager qaManager;
    private readonly PlaySceneDisplay display;

    public ImitationPhase(PlayPhaseManager context, PlayerData localData, PlayerDataContainer container, QAManager qaManager, PlaySceneDisplay display) {
        this.context = context;
        this.localData = localData;
        this.container = container;
        this.qaManager = qaManager;
        this.display = display;
    }

    PlayPhase IPlayPhase.Type => PlayPhase.Imitation;

    void IPlayPhase.Enter() {
        int qi0 = qaManager.Question0.Get(context.CurrentRound - 1);
        int qi1 = qaManager.Question1.Get(context.CurrentRound - 1);

        // 自身のチームと現在ラウンドに対応した質問を表示する
        if (localData.Team) {
            display.ImitationView.SetQuestionLabel(Config.Questions[qi1]);
        } else {
            display.ImitationView.SetQuestionLabel(Config.Questions[qi0]);
        }

        if (context.Runner.IsServer) {
            // ホストはChatGPTに回答をリクエストする
            (int count0, int count1) = container.GetTeamCounts();
            qaManager.RequestAnswers(qi0, 4 - count0, qi1, 4 - count1).Forget();
            // ホストが回答表示順を決定する
            (int[] imitator0, int[] imitator1) = container.GetImitatorId();
            qaManager.SetImitatorId(imitator0, imitator1);
        }

        // 遷移アニメーション
        DOTween.Sequence()
            .AppendInterval(1f)
            .AppendCallback(() => display.ShowPhaseLabel($"ラウンド {context.CurrentRound}"))
            .AppendInterval(2f)
            .AppendCallback(() => display.HidePhaseLabel())
            .AppendInterval(0.5f)
            .AppendCallback(() => display.ShowPhaseLabel("模倣フェーズ"))
            .AppendInterval(2f)
            .AppendCallback(() => display.HidePhaseLabel())
            .AppendInterval(0.5f)
            .AppendCallback(() => {
                display.SetHeaderLabel($"ラウンド {context.CurrentRound}：質問にAIらしく回答せよ");
                display.ShowTimeLabel();
                display.ImitationView.Show();
            })
            .Play();
    }

    void IPlayPhase.FixedUpdateNetwork() {
        int answered = container.CountAnswered(context.CurrentRound);
        display.SetTimeLabel(context.Timer.RemainingTime(context.Runner));
        display.ImitationView.SetAnsweredLabel(answered, container.Count);

        // 時間切れ時に未回答なら、自動的に回答を送信する
        if (context.Timer.Expired(context.Runner)) {
            display.ImitationView.OnClickAnswerButton();
        }

        if (!context.Runner.IsServer) { return; }

        bool isAllAnswered = qaManager.IsAnswered && (answered == container.Count);
        // 時間切れか、全員回答済みで遷移
        if (context.Timer.Expired(context.Runner) || isAllAnswered) {
            context.CurrentPhase = PlayPhase.Insight;
            context.Timer = TickTimer.CreateFromSeconds(context.Runner, Config.InsightTime);
            container.ResetAnswered();
        }
    }

    void IPlayPhase.Exit() {
        display.SetHeaderLabel(string.Empty);
        display.HideTimeLabel();
        display.ImitationView.Hide();
    }

    void IPlayPhase.Dispose() {}
}
