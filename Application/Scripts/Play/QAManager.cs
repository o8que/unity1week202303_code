using Cysharp.Threading.Tasks;
using Fusion;
using Ore2Lib;
using UnityEngine;

public sealed class QAManager : NetworkBehaviour
{
    [Networked, Capacity(3)]
    public NetworkArray<int> Question0 => default;
    [Networked, Capacity(3)]
    public NetworkArray<int> Question1 => default;
    [Networked]
    public NetworkBool IsAnswered { get; private set; }
    [Networked, Capacity(4)]
    public NetworkArray<NetworkString<_64>> AiAnswer0 => default;
    [Networked, Capacity(4)]
    public NetworkArray<NetworkString<_64>> AiAnswer1 => default;
    [Networked, Capacity(4)]
    public NetworkArray<int> ImitatorId0 => default;
    [Networked, Capacity(4)]
    public NetworkArray<int> ImitatorId1 => default;

    public void SetQuestions() {
        for (int i = 0; i < 3; i++) {
            Question0.Set(i, Random.Range(0, Config.Questions.Length));
            Question1.Set(i, Random.Range(0, Config.Questions.Length));
        }
    }

    public async UniTaskVoid RequestAnswers(int index0, int n0, int index1, int n1) {
        IsAnswered = false;

        // var task0 = ChatGPT.SendAsync($"{Config.QuestionPrefix}{Config.Questions[index0]}", n0);
        // var task1 = ChatGPT.SendAsync($"{Config.QuestionPrefix}{Config.Questions[index1]}", n1);
        // (string[] answer0, string[] answer1) = await UniTask.WhenAll(task0, task1);

        string[] answer0 = Config.Answers[index0];
        string[] answer1 = Config.Answers[index1];
        answer0.Shuffle();
        answer1.Shuffle();
        await UniTask.NextFrame();

        for (int i = 0; i < n0; i++) {
            AiAnswer0.Set(i, answer0[i]);
        }
        for (int i = 0; i < n1; i++) {
            AiAnswer1.Set(i, answer1[i]);
        }

        IsAnswered = true;
    }

    public void SetImitatorId(int[] imitator0, int[] imitator1) {
        imitator0.Shuffle();
        imitator1.Shuffle();
        for (int i = 0; i < 4; i++) {
            ImitatorId0.Set(i, imitator0[i]);
            ImitatorId1.Set(i, imitator1[i]);
        }
    }

    public void CalculateScore(PlayerDataContainer container) {
        (int count0, int count1) = container.GetTeamCounts();
        foreach (var data in container) {
            int addScore = 0;

            // AIの回答を看破したら得点
            if (data.SelectedImitatorId < 0) {
                int div = (data.Team) ? (4 - count0) : (4 - count1);
                addScore += Config.ScoreInsight / div;
            }

            // 自身の回答で他プレイヤーを騙したらその人数に応じた得点
            int count = container.GetInsighterCount(data.Team, data.PlayerId);
            switch (count) {
            case 1:
                addScore += Config.ScoreImitate1;
                break;
            case 2:
                addScore += Config.ScoreImitate2;
                break;
            case 3:
                addScore += Config.ScoreImitate3;
                break;
            }

            data.Score += addScore;
            data.AddScore = addScore;
        }
    }
}
