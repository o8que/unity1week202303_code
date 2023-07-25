namespace Ore2Lib
{
    public abstract class SceneParameter
    {
        public string SceneName { get; }

        protected SceneParameter(string sceneName) {
            SceneName = sceneName;
        }
    }
}
