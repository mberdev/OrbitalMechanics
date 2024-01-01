#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.definitions.tree
{
    public static class DictionaryExtensions
    {
        public static bool TryGetFloat(this Dictionary<string, string> d, string key, out float value)
        {
            if (!d.TryGetValue(key, out var strValue))
            {
                value = 0;
                return false;
            }

            if (!float.TryParse(strValue, out value))
            {
                value = 0;
                return false;
            }
    
            return true;
        }

        public static bool TryGetLong(this Dictionary<string, string> d, string key, out long value)
        {
            if (!d.TryGetValue(key, out var strValue))
            {
                value = 0;
                return false;
            }

            if (!long.TryParse(strValue, out value))
            {
                value = 0;
                return false;
            }

            return true;
        }

        public static bool TryGetFloats(this Dictionary<string, string> d, List<string> paramsNames, out Dictionary<string, float> values)
        {

            values = new Dictionary<string, float>();
            foreach (var paramName in paramsNames)
            {
                if (!d.TryGetFloat(paramName, out var value))
                {
                    Debug.LogError($"Could not find param {paramName} in dictionary");
                    return false;
                }
                values.Add(paramName, value);

            }
            return true;
        }
    }
}
