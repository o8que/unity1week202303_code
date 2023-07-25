using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ore2Lib
{
    public interface IScene<in T> : IScene where T : SceneParameter
    {
        void Init(T parameter);
    }

    public interface IScene
    {
        UniTask Open(CancellationToken token);
        UniTask Play(CancellationToken token);
        UniTask Stop(CancellationToken token);
        UniTask Close(CancellationToken token);
    }
}
