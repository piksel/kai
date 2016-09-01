using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{

    class Program
    {
        static bool _v;
        static string verb;
        static object subOptions;

        static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options, (v, so) => {
                verb = v; subOptions = so;
            }))
            {
                Console.WriteLine("Unknown arguments.");
#if !DEBUG
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
#endif
            }
            else
            {
                //Console.WriteLine(verb);
                var action = Actions.GetAction(verb);
                action.SetOptions(options, subOptions);
                var exitCode = action.Run();
                Environment.Exit(exitCode);
            }

#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
