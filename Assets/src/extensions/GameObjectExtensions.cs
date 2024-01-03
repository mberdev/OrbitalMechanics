#nullable enable

using UnityEngine;

namespace Assets.src.extensions
{
    internal static class GameObjectExtensions
    {
        public static T SafeGetComponent<T>(this GameObject gameObject)
        {
            var c = gameObject.GetComponent<T>();
            if (c == null)
            {
                Debug.LogError($"Missing component {typeof(T)} on {gameObject.name}");
                Debug.Break();
                Application.Quit();
                return default(T)!;
            }

            return c;

        }
    }
}
