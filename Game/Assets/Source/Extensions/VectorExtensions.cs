using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2XZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
    }
}