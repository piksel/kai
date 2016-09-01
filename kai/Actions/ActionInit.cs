using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class ActionInit : IKaiAction
    {
        private Options _o;
        private InitSubOptions _so;

        public int Run()
        {
            try
            {
                var path = Path.GetFullPath(_so.Path);

                Console.WriteLine($"Initialzing new project in path \"{path}\"...");
                Console.Write("Creating project... ");
                var project = Project.Create(path);
                Console.WriteLine("Done!");

                Console.Write("Scanning directory... ");
                var files = project.Scan().Result;
                Console.WriteLine(files + " file(s) found.");

                project.Save();


                return ExitCodes.Success;
            }
            catch (Exception x)
            {
                Console.WriteLine("Error initializing: " + x.Message);
                return ExitCodes.UnknownError;
            }
        }

        public void SetOptions(Options options, object subOptions)
        {
            this._o = options;
            this._so = subOptions as InitSubOptions;
        }
    }
}
