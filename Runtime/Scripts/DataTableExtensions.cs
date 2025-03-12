using System;
using System.Data;
using UnityEngine;

namespace BetterReality.Framework.Extensions
{
    public static class DataTableExtensions
    {
        public static float GetFloat(this DataRow obj, string propertyName)
           {
               // i did this for reflection but didn't need it as it's an array not a class
               // still it's quite handy in the context of my csv tables only having strings in and
               // keeping code readable
               object value = obj[propertyName];
               return Convert.ToSingle(value);
           }       
        public static float GetInt(this DataRow obj, string propertyName)
        {
            object value = obj[propertyName];
            return (int)value;
        }    
        public static bool GetBool(this DataRow obj, string propertyName)
        {
            object value = obj[propertyName];
            return Boolean.Parse(value.ToString());
        }
        public static Vector3 GetVector(this DataRow obj, string pX,string pY,string pZ)
        {
            float x = Convert.ToSingle(obj[pX]);
            float y = Convert.ToSingle(obj[pY]);
            float z = Convert.ToSingle(obj[pZ]);
            return new Vector3(x, y, z);
        }
        public static Vector2 GetVector(this DataRow obj, string pX,string pY)
        {
            float x = Convert.ToSingle(obj[pX]);
            float y = Convert.ToSingle(obj[pY]);
            return new Vector3(x, y);
        }

    }
}