using CommandLine;

namespace UrlShortener.DTOs
{
    internal class CliOptions
    {
        [Option('h', "help", Required = false, HelpText = "See help text")]
        public bool Help { get; set; }
    }
}
