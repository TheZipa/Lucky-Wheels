using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LuckyWheels.Code.Extensions
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

        public static T GetLastWithRemove<T>(this List<T> list)
        {
            T element = list.Last();
            list.RemoveAt(list.Count - 1);
            return element;
        }
    }
}