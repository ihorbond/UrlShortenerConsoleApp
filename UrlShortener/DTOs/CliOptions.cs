using CommandLine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DTOs
{
    internal class CliOptions
    {
        [Option('h', "help", Required = false, HelpText = "See help text")]
        public bool Help { get; set; }
    }
}
