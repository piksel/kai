using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    class Hasher
    {
        internal static uint HashFile(string file)
        {
            using (var fs = File.OpenRead(file)) {
                using (var ms = new MemoryStream()) {
                    fs.CopyTo(ms);
                    return xxHashSharp.xxHash.CalculateHash(ms.ToArray());
                }
            }
        }
    }
}
