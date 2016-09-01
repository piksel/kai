using System.Collections.Generic;
using System.IO;

namespace Piksel.Kai
{
    public class Moment
    {
        public int Index { get; }
        public List<uint> Hashes { get; set; } = new List<uint>();

        public Project Project { get; }

        public void Save()
        {
            Directory.CreateDirectory(Project.MomentPath);
            var filepath = Path.Combine(Project.MomentPath, Index + ".kai-moment");
            using(var fs = File.OpenWrite(filepath))
            {
                using (var bw = new BinaryWriter(fs)) {

                    foreach (var hash in Hashes)
                    {
                        bw.Write(hash);
                    }
                
                }
            }
        }

        public Moment(Project project, int index)
        {
            Index = index;
            Project = project;
        }
    }
}