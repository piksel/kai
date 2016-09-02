using System;
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

        public static Moment Load(Project project, int index)
        {
            var moment = new Moment(project, index);

            var filepath = Path.Combine(project.MomentPath, index + ".kai-moment");
            using (var fs = File.OpenRead(filepath))
            {
                using (var br = new BinaryReader(fs))
                {
                    for(int i=0; i < project.FileList.Count; i++)
                    {
                        try
                        {
                            moment.Hashes.Add(br.ReadUInt32());
                        }
                        catch (Exception x)
                        {
                            Console.WriteLine("Error loading moment: " + x.Message);
                        }
                    }
                }
            }

            return moment;
        }

        public Moment(Project project, int index)
        {
            Index = index;
            Project = project;
        }
    }
}