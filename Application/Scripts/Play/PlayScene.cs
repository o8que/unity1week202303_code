using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Photon.Realtime;
using Fusion.Sockets;
using Ore2Lib;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PlayScene : Scene<PlayScene.With>, INetworkRunnerCallbacks
{
    public sealed class With : SceneParameter
    {
        public With() : base("Play") {}
    }

    [SerializeField]
    private NetworkRunner networkRunnerPrefab;
    [SerializeField]
    private NetworkPrefabRef playerDataPrefab;

    private NetworkRunner networkRunner;
    private PlayPhaseManager phaseManager;
    private QAManager qaManager;
    private PlayerDataContainer container;
    private PlaySceneDisplay display;

    protected override async UniTask Enter(CancellationToken token) {
        var appSettings = PhotonAppSettings.Instance.AppSettings.GetCopy();
        appSettings.AppVersion = Application.version;

        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.ProvideInput = false;
        networkRunner.AddCallbacks(this);
        var result = await networkRunner.StartGame(new StartGameArgs {
            GameMode = GameMode.AutoHostOrClient,
            CustomLobbyName = Config.LobbyName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>(),
            CustomPhotonAppSettings = appSettings
        });

        if (!result.Ok) { return; }

        phaseManager = FindAnyObjectByType<PlayPhaseManager>();
        qaManager = FindAnyObjectByType<QAManager>();
        container = FindAnyObjectByType<PlayerDataContainer>();
        display = FindAnyObjectByType<PlaySceneDisplay>();

        PlayerData localData = null;
        await UniTask.WaitUntil(() => container.TryGetLocalPlayerData(out localData), cancellationToken: token);
        display.Init(localData, container);
        phaseManager.Init(localData, container, qaManager, display);
    }

    protected override void OnUpdate() {
        display.OnUpdate();

        if (Time.smoothDeltaTime >= 0.3f) {
            GameManager.Scene.NavigateTo(new TitleScene.With("動作が重くなっています"));
        }
    }

    protected override async UniTask Exit(CancellationToken token) {
        phaseManager.Dispose();

        if (!networkRunner.IsShutdown) {
            networkRunner.RemoveCallbacks(this);
            await networkRunner.Shutdown();
        }
    }

    void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        if (!runner.IsServer) { return; }
        var data = runner.Spawn(playerDataPrefab, Vector3.zero, Quaternion.identity, player);
        runner.SetPlayerObject(player, data);
    }

    void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        if (!runner.IsServer) { return; }
        if (!runner.TryGetPlayerObject(player, out var data)) { return; }
        runner.Despawn(data);
    }

    void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
        GameManager.Scene.NavigateTo(new TitleScene.With("通信が切断されました"));
    }

    // ReSharper disable once Unity.IncorrectMethodSignature
    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner) {}
    void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input) {}
    void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner) {}
    void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
    void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
    void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}
    void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {}
    void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {}
    void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {}
    void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {}
    void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner) {}
    void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner) {}
}
