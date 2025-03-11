using System;
using System.Collections.Generic;
using UnityEngine;

namespace BetterReality
{
    static public class BetterUtils
    {
        public static void VectorToVars(Vector3 vector3, out float x, out float y, out float z)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        public static float[] VectorToVars(Vector3 vector3)
        {
            return new float[] { vector3.x, vector3.y, vector3.z };
        }

        public static void VectorToVars(Vector2 vector2, out float x, out float y)
        {
            x = vector2.x;
            y = vector2.y;
        }

        public static double Percentile(List<float> sortedValues, double percentile)
        {
            int n = sortedValues.Count;
            if (n == 0) return 0; // Handle empty list case

            sortedValues.Sort(); // Ensure the list is sorted
            double rank = percentile * (n - 1);
            int lowerIndex = (int)Math.Floor(rank);
            int upperIndex = (int)Math.Ceiling(rank);

            if (lowerIndex == upperIndex)
                return sortedValues[lowerIndex];

            return (sortedValues[lowerIndex] + sortedValues[upperIndex]) / 2.0;
        }

        public static System.Collections.IEnumerator WaitAndRun(float delay, System.Action action)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            action?.Invoke(); // Execute the lambda function
        }

        /// <summary>
        /// Returns a string of mm:ss 
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static string milisecsShow(int milliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
            string formattedTime = timeSpan.ToString(@"mm\:ss");
            return formattedTime;
        }

        /// <summary>
        /// For disabling compenents when on headset
        /// </summary>
        /// <returns></returns>
        public static bool IsEditorMode()
        {
#if UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }
}