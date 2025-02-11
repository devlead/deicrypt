using System.ComponentModel;

namespace deicrypt;

public class DecodeSettings : CommandSettings
{
    [CommandArgument(0, "<inputFile>")]
    [Description("The encoded source file.")]
    public required string Input { get; set; }

    [CommandArgument(1, "<outputFile>")]
    [Description("The decoded output file.")]
    public required string Output { get; set; }

    [CommandOption("--decompress")]
    public bool Decompress { get; set; }
} 