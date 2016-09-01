using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public static class PathEx
    {
        public static string GetRelativePath(string relativeTo, string fullPath)
        {
            if (relativeTo[relativeTo.Length - 1] != Path.DirectorySeparatorChar) relativeTo += Path.DirectorySeparatorChar;
            return Uri.UnescapeDataString(
                new Uri(relativeTo).MakeRelativeUri(new Uri(fullPath))
                .ToString()
                .Replace('/', Path.DirectorySeparatorChar)
            );
        }
    }
}
