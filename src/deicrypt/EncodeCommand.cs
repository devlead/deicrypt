using deicrypt.Core;
using Spectre.IO;

namespace deicrypt;

public class EncodeCommand(ILogger<EncodeCommand> logger, FileSystem fileSystem) : AsyncCommand<EncodeSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, EncodeSettings settings)
    {
        logger.LogInformation("Encoding from {Input} to {Output}...", settings.Input, settings.Output);

        await using var input = fileSystem.GetFile(settings.Input).OpenRead();
        await using var output = fileSystem.GetFile(settings.Output).OpenWrite();

        var deiCrypt = new DeiCrypt();

        deiCrypt.Encode(input, output, settings.Compress);

        return 0;
    }
}