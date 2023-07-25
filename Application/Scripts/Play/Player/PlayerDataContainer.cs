using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerDataContainer : MonoBehaviour, IEnumerable<PlayerData>
{
    private readonly List<PlayerData> list = new(6);

    public PlayerData this[int index] => list[index];
    public int Count => list.Count;

    private void OnTransformChildrenChanged() {
        list.Clear();
        foreach (Transform child in transform) {
            list.Add(child.GetComponent<PlayerData>());
        }
    }

    public bool TryGetLocalPlayerData(out PlayerData localData) {
        foreach (var data in list) {
            if (data.IsLocal) {
                localData = data;
                return true;
            }
        }
        localData = null;
        return false;
    }

    // 指定したプレイヤーIDのデータ取得
    public bool TryGetPlayerData(int playerId, out PlayerData playerData) {
        foreach (var data in list) {
            if (data.PlayerId == playerId) {
                playerData = data;
                return true;
            }
        }
        playerData = null;
        return false;
    }

    // チーム分け
    public void SetTeam() {
        for (int i = 0; i < list.Count; i++) {
            list[i].Team = (i % 2 == 1);
        }
    }

    // チーム人数取得
    public (int count0, int count1) GetTeamCounts() {
        int count0 = 0, count1 = 0;
        foreach (var data in list) {
            if (data.Team) {
                count1++;
            } else {
                count0++;
            }
        }
        return (count0, count1);
    }

    // 模倣プレイヤーのID取得
    public (int[] imitator0, int[] imitator1) GetImitatorId() {
        int[] imitator0 = { -4, -3, -2, -1 };
        int[] imitator1 = { -4, -3, -2, -1 };
        int i0 = 0, i1 = 0;
        foreach (var data in list) {
            if (data.Team) {
                imitator1[i1++] = data.PlayerId;
            } else {
                imitator0[i0++] = data.PlayerId;
            }
        }
        return (imitator0, imitator1);
    }

    // 回答済みプレイヤー数
    public int CountAnswered(int round) {
        int result = 0;
        foreach (var data in list) {
            if (data.IsAnswered) {
                result++;
            }
        }
        return result;
    }

    // 回答済みリセット
    public void ResetAnswered() {
        foreach (var data in list) {
            data.IsAnswered = false;
        }
    }

    // プレイヤー名を取得
    public string GetNickName(int playerId) {
        foreach (var data in list) {
            if (data.PlayerId == playerId) {
                return data.NickNameValue;
            }
        }
        return Config.DefaultName;
    }

    // 回答を取得
    public string GetAnswer(int playerId) {
        foreach (var data in list) {
            if (data.PlayerId == playerId) {
                return data.Answer.Value;
            }
        }
        return Config.AnswerError;
    }

    // 看破プレイヤー名のリストを取得する
    public List<string> GetInsighterNames(bool imitatorTeam, int imitatorId) {
        var result = new List<string>();
        foreach (var data in list) {
            if (data.Team != imitatorTeam && data.SelectedImitatorId == imitatorId) {
                result.Add(data.NickNameValue);
            }
        }
        return result;
    }

    // 看破プレイヤーの人数を取得する
    public int GetInsighterCount(bool imitatorTeam, int imitatorId) {
        int result = 0;
        foreach (var data in list) {
            if (data.Team != imitatorTeam && data.SelectedImitatorId == imitatorId) {
                result++;
            }
        }
        return result;
    }

    // 勝者の名前を取得する
    public string GetWinnerName() {
        list.Sort((data1, data2) => {
            int scoreDiff = data2.Score - data1.Score;
            if (scoreDiff != 0) { return scoreDiff; }

            return data2.PlayerId - data1.PlayerId;
        });
        return list[0].NickNameValue;
    }

    public IEnumerator<PlayerData> GetEnumerator() {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
