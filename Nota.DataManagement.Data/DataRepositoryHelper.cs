using Newtonsoft.Json;
using Nota.DataManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nota.DataManagement.Data
{
    public static class DataRepositoryHelper
    {
        public static IDictionary<int, T> Deserialize<T>(string file) 
        {
            IDictionary<int, T> dictionary;
            JsonSerializer serialiser = new JsonSerializer();
            using (StreamReader sr = new StreamReader(@file))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                dictionary = serialiser.Deserialize<IDictionary<int, T>>(reader);
            }
            return dictionary;
        }

        public static bool Serialize<T>(IDictionary<int, T> dictionary, string file) where T:BaseData
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Include;
                using (StreamWriter sw = new StreamWriter(@file))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, dictionary);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
