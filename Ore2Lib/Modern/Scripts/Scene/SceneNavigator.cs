using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ore2Lib
{
    public sealed class SceneNavigator
    {
        private readonly ILoadingScreen loadingScreen;
        private CancellationTokenSource cancellationTokenSource;
        private string currentSceneName;

        public SceneNavigator() {
            loadingScreen = GameObjectEx.FindObjectOfInterface<ILoadingScreen>() ?? new NoLoadingScreen();
        }

        public void LaunchFrom<T>(T parameter) where T : SceneParameter {
            cancellationTokenSource = new CancellationTokenSource();
            LoadSceneAsync(parameter, cancellationTokenSource.Token).Forget();
        }

        public void NavigateTo<T>(T parameter) where T : SceneParameter {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            cancellationTokenSource = new CancellationTokenSource();
            ChangeSceneAsync(parameter, cancellationTokenSource.Token).Forget();
        }

        private async UniTask LoadSceneAsync<T>(T parameter, CancellationToken token) where T : SceneParameter {
            token.ThrowIfCancellationRequested();

            currentSceneName = parameter.SceneName;
            await SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentSceneName));

            var scene = GameObjectEx.FindObjectOfInterface<IScene<T>>();
            scene.Init(parameter);
            await scene.Open(token);
            await loadingScreen.Hide(token);
            await scene.Play(token);
        }

        private async UniTask UnloadSceneAsync(CancellationToken token) {
            token.ThrowIfCancellationRequested();

            var scene = GameObjectEx.FindObjectOfInterface<IScene>();
            await scene.Stop(token);
            await loadingScreen.Show(token);
            await scene.Close(token);

            await SceneManager.UnloadSceneAsync(currentSceneName);
            await Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        private async UniTaskVoid ChangeSceneAsync<T>(T parameter, CancellationToken token) where T : SceneParameter {
            await UnloadSceneAsync(token);
            await LoadSceneAsync(parameter, token);
        }
    }
}
