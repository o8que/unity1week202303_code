using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ore2Lib
{
    public interface ILoadingScreen
    {
        UniTask Show(CancellationToken token);
        UniTask Hide(CancellationToken token);
    }
}
