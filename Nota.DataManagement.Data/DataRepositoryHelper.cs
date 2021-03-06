﻿using Newtonsoft.Json;
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
        public static Dictionary<int, T> Deserialize<T>(string file) 
        {
            Dictionary<int, T> dictionary;
            JsonSerializer serialiser = new JsonSerializer();
            using (StreamReader sr = new StreamReader(@file))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                dictionary = serialiser.Deserialize<Dictionary<int, T>>(reader);
            }
            return dictionary;
        }

        public static bool Serialize<T>(Dictionary<int, T> dictionary, string file) where T:BaseData
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
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
