using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ore2Lib
{
    public sealed class SimpleLoadingScreen : LoadingScreenBase
    {
        public override UniTask Show(CancellationToken token) {
            token.ThrowIfCancellationRequested();
            SetActiveChildren(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide(CancellationToken token) {
            token.ThrowIfCancellationRequested();
            SetActiveChildren(false);
            return UniTask.CompletedTask;
        }

        private void SetActiveChildren(bool value) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(value);
            }
        }
    }
}
