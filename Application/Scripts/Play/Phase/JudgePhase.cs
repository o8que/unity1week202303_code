using DG.Tweening;
using Fusion;

public sealed class JudgePhase : IPlayPhase
{
    private readonly PlayPhaseManager context;
    private readonly PlayerData localData;
    private readonly PlayerDataContainer container;
    private readonly QAManager qaManager;
    private readonly PlaySceneDisplay display;

    public JudgePhase(PlayPhaseManager context, PlayerData localData, PlayerDataContainer container, QAManager qaManager, PlaySceneDisplay display) {
        this.context = context;
        this.localData = localData;
        this.container = container;
        this.qaManager = qaManager;
        this.display = display;
    }

    PlayPhase IPlayPhase.Type => PlayPhase.Judge;

    void IPlayPhase.Enter() {
        // ホストがスコアを計算
        if (context.Runner.IsServer) {
            qaManager.CalculateScore(container);
        }

        display.JudgeView.SetQuestionLabel(
            Config.Questions[qaManager.Question0.Get(context.CurrentRound - 1)],
            Config.Questions[qaManager.Question1.Get(context.CurrentRound - 1)]
        );

        for (int i = 0; i < 4; i++) {
            int imitatorId0 = qaManager.ImitatorId0.Get(i);
            string imitatorNickName0 = (imitatorId0 >= 0) ? container.GetNickName(imitatorId0) : Config.AIName;
            string answer0 = (imitatorId0 >= 0) ? container.GetAnswer(imitatorId0) : qaManager.AiAnswer0.Get(-(imitatorId0 + 1)).Value;
            var insighterNames0 = container.GetInsighterNames(false, imitatorId0);
            display.JudgeView.SetAnswerPanel(false, i, imitatorNickName0, answer0, insighterNames0);

            int imitatorId1 = qaManager.ImitatorId1.Get(i);
            string imitatorNickName1 = (imitatorId1 >= 0) ? container.GetNickName(imitatorId1) : Config.AIName;
            string answer1 = (imitatorId1 >= 0) ? container.GetAnswer(imitatorId1) : qaManager.AiAnswer1.Get(-(imitatorId1 + 1)).Value;
            var insighterNames1 = container.GetInsighterNames(true, imitatorId1);
            display.JudgeView.SetAnswerPanel(true, i, imitatorNickName1, answer1, insighterNames1);
        }

        // 遷移アニメーション
        DOTween.Sequence()
            .AppendInterval(1f)
            .AppendCallback(() => display.ShowPhaseLabel("判定フェーズ"))
            .AppendInterval(2f)
            .AppendCallback(() => display.HidePhaseLabel())
            .AppendInterval(0.5f)
            .AppendCallback(() => {
                display.SetHeaderLabel($"ラウンド {context.CurrentRound}");
                display.JudgeView.Show();
            })
            .AppendInterval(3f)
            .AppendCallback(() => {
                display.JudgeView.ShowImitatorNickName();
                GameManager.Sound.PlayJudge();
            })
            .AppendInterval(1f)
            .AppendCallback(() => {
                display.PlayerListView.ShowAddScore();
                GameManager.Sound.PlayJudge();
            })
            .AppendInterval(1f)
            .AppendCallback(() => {
                display.PlayerListView.StartCountUpScore();
                GameManager.Sound.PlayCountUp();
            })
            .AppendInterval(1.5f)
            .AppendCallback(() => display.ShowTimeLabel())
            .Play();
    }

    void IPlayPhase.FixedUpdateNetwork() {
        display.SetTimeLabel(context.Timer.RemainingTime(context.Runner));

        if (!context.Runner.IsServer) { return; }

        if (context.Timer.Expired(context.Runner)) {
            if (context.CurrentRound == Config.TotalRound) {
                // 全ラウンドが終了していたら、結果フェーズに遷移
                context.CurrentPhase = PlayPhase.Result;
                context.Timer = TickTimer.CreateFromSeconds(context.Runner, Config.ResultTime);
            } else {
                // 次のラウンドの模倣フェーズに遷移
                context.CurrentPhase = PlayPhase.Imitation;
                context.Timer = TickTimer.CreateFromSeconds(context.Runner, Config.ImitationTime);
                context.CurrentRound++;
            }
        }
    }

    void IPlayPhase.Exit() {
        display.SetHeaderLabel(string.Empty);
        display.HideTimeLabel();
        display.PlayerListView.HideAddScore();
        display.JudgeView.Hide();
    }

    void IPlayPhase.Dispose() {}
}
