using Fusion;

public sealed class PlayerData : NetworkBehaviour
{
    [Networked]
    private NetworkString<_8> NickName { get; set; }
    [Networked]
    public NetworkBool Team { get; set; }
    [Networked]
    public int Score { get; set; }
    [Networked]
    public int AddScore { get; set; }
    // 模倣フェーズ
    [Networked]
    public NetworkBool IsAnswered { get; set; }
    [Networked]
    public NetworkString<_64> Answer { get; set; }
    // 看破フェーズ
    [Networked]
    public int SelectedImitatorId { get; set; }

    public int PlayerId => Object.InputAuthority.PlayerId;
    public bool IsLocal => Object.HasInputAuthority;
    private string nickNameCache;
    public string NickNameValue {
        get {
            NickName.Get(ref nickNameCache);
            return nickNameCache;
        }
    }

    private PlayerDataContainer container;
    private PlaySceneDisplay display;

    public override void Spawned() {
        container = FindAnyObjectByType<PlayerDataContainer>();
        display = FindAnyObjectByType<PlaySceneDisplay>();

        transform.SetParent(container.transform);
        if (Object.HasInputAuthority) {
            RpcSetNickName(GameManager.SaveData.PlayerName);
        }
    }

    // プレイヤー名送信
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickName(NetworkString<_8> nickName) {
        NickName = string.IsNullOrWhiteSpace(nickName.Value) ? Config.DefaultName : nickName;
    }

    // チャット送信
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RpcSendChatMessage(NetworkString<_32> message) {
        display.ChatSystem.ReceiveChatMessage(NickNameValue, message.Value);
    }

    // 模倣回答送信
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RpcSendAnswer(NetworkString<_64> answer) {
        IsAnswered = true;
        Answer = answer;
    }

    // 看破回答選択
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RpcSelectImitatorId(int id) {
        SelectedImitatorId = id;
    }
}
