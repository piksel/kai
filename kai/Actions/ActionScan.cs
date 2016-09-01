using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class ActionScan : IKaiAction
    {
        public int Run()
        {
            try
            {
                var project = Project.Load(Environment.CurrentDirectory);
                Console.Write("Scanning for new files... ");
                var newFiles = project.Scan().Result;
                Console.WriteLine($"{newFiles} new file(s) found.");
                project.Save();

                return ExitCodes.Success;
            }
            catch (Exception x)
            {
                Console.WriteLine("Error scanning: " + x.Message);
                return ExitCodes.UnknownError;
            }

        }

        public void SetOptions(Options options, object subOptions)
        {
            
        }
    }
}
