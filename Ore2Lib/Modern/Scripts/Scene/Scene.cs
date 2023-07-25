using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ore2Lib
{
    public abstract class Scene<T> : MonoBehaviour, IScene<T> where T : SceneParameter
    {
        private bool isPlaying = false;

        protected T Parameter { get; private set; }

        private void Update() {
            if (isPlaying) { OnUpdate(); }
        }

        private void LateUpdate() {
            if (isPlaying) { OnLateupdate(); }
        }

        private void FixedUpdate() {
            if (isPlaying) { OnFixedUpdate(); }
        }

        void IScene<T>.Init(T parameter) {
            Parameter = parameter;
        }

        async UniTask IScene.Open(CancellationToken token) {
            token.ThrowIfCancellationRequested();
            await Enter(token);
        }

        async UniTask IScene.Play(CancellationToken token) {
            token.ThrowIfCancellationRequested();
            isPlaying = true;
            await Show(token);
        }

        async UniTask IScene.Stop(CancellationToken token) {
            token.ThrowIfCancellationRequested();
            await Hide(token);
            isPlaying = false;
        }

        async UniTask IScene.Close(CancellationToken token) {
            token.ThrowIfCancellationRequested();
            await Exit(token);
        }

        protected virtual UniTask Enter(CancellationToken token) => UniTask.CompletedTask;
        protected virtual UniTask Show(CancellationToken token) => UniTask.CompletedTask;
        protected virtual UniTask Hide(CancellationToken token) => UniTask.CompletedTask;
        protected virtual UniTask Exit(CancellationToken token) => UniTask.CompletedTask;
        protected virtual void OnUpdate() {}
        protected virtual void OnLateupdate() {}
        protected virtual void OnFixedUpdate() {}
    }
}
