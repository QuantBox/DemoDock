using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuLoader
{
    public class JsonConfig
    {
        private static JsonSerializerSettings jSetting = new JsonSerializerSettings()
        {
            // json文件格式使用非紧凑模式
            //NullValueHandling = NullValueHandling.Ignore,
            //DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        public static void Save(string file, object obj)
        {
            using (FileStream fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.Write("{0}", JsonConvert.SerializeObject(obj, obj.GetType(), jSetting));
                    writer.Close();
                }
            }
        }
        public static object Load(string file, object obj)
        {
            try
            {
                object ret;
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextReader reader = new StreamReader(fs))
                    {
                        ret = JsonConvert.DeserializeObject(reader.ReadToEnd(), obj.GetType());
                        reader.Close();
                    }
                }

                return ret;
            }
            catch
            {
            }
            return obj;
        }
    }
}
