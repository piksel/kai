using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public static class Actions
    {
        public static IKaiAction GetAction(string verb)
        {
            IKaiAction ka;
            switch (verb)
            {
                case "init": ka = new ActionInit(); break;
                case "mark": ka = new ActionMark(); break;
                case "warp": ka = new ActionWarp(); break;
                case "info": ka = new ActionInfo(); break;
                case "scan": ka = new ActionScan(); break;
                case "diff": ka = new ActionDiff(); break;
                default: ka = new ActionUnkown(); break;
            }

            return ka;
        }

    }

    public static class ExitCodes
    {
        public static readonly int Success = 0;
        public static readonly int UnknownError = 10;
    }

    public interface IKaiAction
    {
        void SetOptions(Options options, object subOptions);
        int Run();
    }

    internal class ActionUnkown : IKaiAction
    {
        public int Run()
        {
            Console.WriteLine("Unknown action.");
            return ExitCodes.UnknownError;
        }

        public void SetOptions(Options options, object subOptions)
        {
        }
    }

}
