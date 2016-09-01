using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class Options
    {
        public Options()
        {
            InitVerb = new InitSubOptions();
            MarkVerb = new MarkSubOptions();
            WarpVerb = new WarpSubOptions();
            InfoVerb = new InfoSubOptions();
        }

        [Option('v', "verbose", HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [VerbOption("init", HelpText = "Initializes the current directory as a Kai project")]
        public InitSubOptions InitVerb { get; set; }

        [VerbOption("mark", HelpText = "Marks the current state of the project files as a new Moment")]
        public MarkSubOptions MarkVerb { get; set; }

        [VerbOption("warp", HelpText = "Warps the project to a specific Moment")]
        public WarpSubOptions WarpVerb { get; set; }

        [VerbOption("info", HelpText = "Shows information about the current Moment and project state")]
        public InfoSubOptions InfoVerb { get; set; }

        [VerbOption("scan", HelpText = "Scans for new files")]
        public ScanSubOptions ScanVerb { get; set; }

        [VerbOption("diff", HelpText = "Shows the differences between Moments")]
        public DiffSubOptions DiffVerb { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }
    }

    public class InitSubOptions
    {
        [Option(DefaultValue = ".")]
        public string Path { get; set; }
    }

    public class MarkSubOptions
    {

    }

    public class WarpSubOptions
    {

    }

    public class InfoSubOptions
    {

    }

    public class ScanSubOptions
    {

    }

    public class DiffSubOptions
    {
        [ValueList(typeof(List<string>), MaximumElements = 2)]
        public IList<string> Moments { get; set; }
    }
}
