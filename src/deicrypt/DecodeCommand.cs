using deicrypt.Core;
using Spectre.IO;

namespace deicrypt;

public class DecodeCommand(ILogger<DecodeCommand> logger, FileSystem fileSystem) : AsyncCommand<DecodeSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, DecodeSettings settings, CancellationToken cancellationToken)
    {
        logger.LogInformation("Decoding from {Input} to {Output}...", settings.Input, settings.Output);

        await using var input = fileSystem.GetFile(settings.Input).OpenRead();
        await using var output = fileSystem.GetFile(settings.Output).OpenWrite();

        var deiCrypt = new DeiCrypt();

        deiCrypt.Decode(input, output, settings.Decompress); 

        return 0;
    }
} 