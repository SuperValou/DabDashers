using UnityEngine;

namespace Assets.Scripts
{
    public static class MiniMath
    {
        public static float GetSquaredDistance(Vector3 positionA, Vector3 positionB)
        {
            float X = positionA.x - positionB.x;
            float Y = positionA.y - positionB.y;
            float Z = positionA.z - positionB.z;
            return X * X + Y * Y + Z * Z;
        }
    }
}