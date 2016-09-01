using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Piksel.Kai
{
    public class ProjectSettings
    {
        public string RemoteHost { get; set; }
        public int MomentIndex { get; set; }

        public void Save(string filename)
        {
            var ys = new Serializer(namingConvention: NamingConvention);
            using (var sw = new StreamWriter(filename))
            {
                ys.Serialize(sw, this);
            }
        }

        public static ProjectSettings Load(string filename)
        {
            var yd = new Deserializer(namingConvention: NamingConvention);
            using (var sr = new StreamReader(filename))
            {
                return yd.Deserialize<ProjectSettings>(sr);
            }
        }

        public static INamingConvention NamingConvention = 
            new YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention();

        public static ProjectSettings Default { get
            {
                return new ProjectSettings()
                {

                };
            }
        }
    }
}
