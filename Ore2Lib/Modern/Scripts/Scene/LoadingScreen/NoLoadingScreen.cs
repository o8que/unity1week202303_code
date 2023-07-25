using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ore2Lib
{
    internal sealed class NoLoadingScreen : ILoadingScreen
    {
        UniTask ILoadingScreen.Show(CancellationToken token) => UniTask.CompletedTask;
        UniTask ILoadingScreen.Hide(CancellationToken token) => UniTask.CompletedTask;
    }
}
