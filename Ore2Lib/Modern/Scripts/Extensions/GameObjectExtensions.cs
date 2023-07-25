using UnityEngine;

namespace Ore2Lib
{
    public static class GameObjectExtensions
    {
        public static void RemoveComponent<T>(this GameObject self) where T : Component {
            if (self.TryGetComponent<T>(out var component)) {
                Object.Destroy(component);
            }
        }

        public static void RemoveComponent<T>(this Component self) where T : Component {
            if (self.TryGetComponent<T>(out var component)) {
                Object.Destroy(component);
            }
        }
    }

    public static class GameObjectEx
    {
        public static T FindObjectOfInterface<T>() where T : class {
            foreach (var component in Object.FindObjectsOfType<Component>()) {
                if (component is T result) {
                    return result;
                }
            }
            return null;
        }
    }
}
