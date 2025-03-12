

namespace BetterReality.Framework.Extensions
{
    public static class FloatArrayExtensions
    {
        public static void Deconstruct(this float[] array, out float x, out float y, out float z)
        {
            x = array.Length > 0 ? array[0] : 0f;
            y = array.Length > 1 ? array[1] : 0f;
            z = array.Length > 2 ? array[2] : 0f;
        }
    }
}