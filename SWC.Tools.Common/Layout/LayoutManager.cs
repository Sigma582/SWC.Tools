using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using SWC.Tools.Common.Networking.Json.Entities;

namespace SWC.Tools.Common.Layout
{
//    public class LayoutManager
//    {
//        public void Save(string fileName)
//        {
//
//        }
//
//        public void Load(string fileName)
//        {
//
//        }
//    }

    public class LayoutSaver
    {
        public void Save(IList<Building> buildings, string fileName)
        {
            buildings = buildings.Where(b => !b.IsJunk).OrderBy(b => b.Key).ToList();

            var serializer = new DataContractJsonSerializer(typeof(IList<Building>));

            string str;
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, buildings);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }

            using (var writer = new StreamWriter(fileName, false))
            {
                writer.Write(str);
                writer.Flush();
            }
        }
    }

    public class LayoutLoader
    {
        public IList<Building> Load(string fileName)
        {
            string str;
            using (var reader = new StreamReader(fileName))
            {
                str = reader.ReadToEnd();
            }

            var serializer = new DataContractJsonSerializer(typeof(IList<Building>));

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                var buildings = (IList<Building>) serializer.ReadObject(stream);
                return buildings;
            }

        }
    }
}