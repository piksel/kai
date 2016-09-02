using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class ActionDiff : IKaiAction
    {
        private Options _o;
        private DiffSubOptions _so;

        public int Run()
        {
            try {
                if (_so.Moments.Count < 2) throw new ArgumentException();

                var project = Project.Load(Environment.CurrentDirectory);

                Console.WriteLine($"Differences between Moment #{_so.Moments[0]} and Moment #{_so.Moments[1]}:");

                var momentA = project.GetMoment(int.Parse(_so.Moments[0]));
                var momentB = project.GetMoment(int.Parse(_so.Moments[1]));

                int changes = 0;

                for(int i=0; i< momentA.Hashes.Count; i++)
                {
                    if (momentA.Hashes[i] != momentB.Hashes[i])
                    {
                        Console.WriteLine($"{momentA.Hashes[i].ToString("x8")} -> {momentA.Hashes[i].ToString("x8")} {project.FileList[i]}");
                        changes++;
                    }
                }

                Console.WriteLine($"\n{changes} change(s) in total.");

                return ExitCodes.Success;
            }
            catch (Exception x)
            {
                Console.WriteLine("Error showing differences: " + x.Message);
                return ExitCodes.UnknownError;
            }
        }

        public void SetOptions(Options options, object subOptions)
        {
            _o = options;
            _so = subOptions as DiffSubOptions;
        }
    }
}
