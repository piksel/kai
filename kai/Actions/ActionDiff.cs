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

                Console.WriteLine($"Differences between Moment #{_so.Moments[0]} and Moment #{_so.Moments[1]}:");

                throw new NotImplementedException();

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
