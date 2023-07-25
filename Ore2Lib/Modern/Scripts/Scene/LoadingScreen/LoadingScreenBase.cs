using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ore2Lib
{
    public abstract class LoadingScreenBase : MonoBehaviour, ILoadingScreen
    {
        public abstract UniTask Show(CancellationToken token);
        public abstract UniTask Hide(CancellationToken token);
    }
}
