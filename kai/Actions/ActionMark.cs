using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class ActionMark : IKaiAction
    {
        public int Run()
        {
            try {
                var project = Project.Load(Environment.CurrentDirectory);
                var start = DateTime.Now;

                Console.Write("Hashing current file states... ");
                var moment = project.CreateMoment().Result;
                Console.WriteLine($"Done!\nHashed {moment.Hashes.Count} file(s) in {(DateTime.Now - start).TotalSeconds:F2} second(s).");

                Console.WriteLine($"Created new Moment #{moment.Index}.");

                moment.Save();
                project.Save();

                return ExitCodes.Success;
            }
            catch (Exception x)
            {
                Console.WriteLine("Error marking: " + x.Message);
                return ExitCodes.UnknownError;
            }
        }

        public void SetOptions(Options options, object subOptions)
        {

        }
    }
}
