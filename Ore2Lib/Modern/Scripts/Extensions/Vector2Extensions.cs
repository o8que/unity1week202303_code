using UnityEngine;

namespace Ore2Lib
{
    public static class Vector2Ex
    {
        public static float Cross(Vector2 lhs, Vector2 rhs) {
            return lhs.x * rhs.y - lhs.y * rhs.x;
        }
    }
}
