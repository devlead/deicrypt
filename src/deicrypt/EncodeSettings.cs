using System.ComponentModel;

namespace deicrypt;

public class EncodeSettings : CommandSettings
{
    [CommandArgument(0, "<inputFile>")]
    [Description("The unencoded source file.")]
    public required string Input { get; set; }

    [CommandArgument(1, "<outputFile>")]
    [Description("The encoded output file.")]
    public required string Output { get; set; }

    [CommandOption("--compress")]
    public bool Compress { get; set; }
}