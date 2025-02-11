using Spectre.IO;
using Spectre.IO.Testing;
using static deicrypt.Tests.DeiCryptStreamTests;

namespace deicrypt.Tests;

public class DeiCryptStreamTests
{
    [Test]
    public async Task Encode()
    {
        // Given
        var fileSystem = new FakeFileSystem(FakeEnvironment.CreateMacOSEnvironment());
        var inputFile = fileSystem.CreateFile("/plain.txt", "NSA"u8.ToArray());
        var outputFile = fileSystem.CreateFile("/encoded.ally");

        var deiCrypt = new Core.DeiCrypt();

        // When
        using var input = inputFile.OpenRead();
        using var output = outputFile.OpenWrite();
        deiCrypt.Encode(input, output);

        // Then
        await Verify(outputFile.ReadAllText());
    }

    [Test]
    public async Task Decode()
    {
        // Given
        var fileSystem = new FakeFileSystem(FakeEnvironment.CreateMacOSEnvironment());
        var inputFile = fileSystem.CreateFile("/encoded.ally", "Allyship            Pronouns            Bias                Allyship            Allyship            Gender              "u8.ToArray());
        var outputFile = fileSystem.CreateFile("/plain.txt");
        var deiCrypt = new Core.DeiCrypt();

        // When
        using var input = inputFile.OpenRead();
        using var output = outputFile.OpenWrite();
        deiCrypt.Decode(input, output);

        // Then
        await Verify(outputFile.ReadAllText());
    }

    public class Compressed
    {
        [TestCase(true)]
        [TestCase(false)]
        public async Task Encode(bool compress)
        {
            // Given
            var fileSystem = new FakeFileSystem(FakeEnvironment.CreateMacOSEnvironment());
            var inputFile = fileSystem.CreateFile("/plain.txt", "NSA"u8.ToArray());
            var outputFile = fileSystem.CreateFile("/encoded.ally");
            var deiCrypt = new Core.DeiCrypt();

            // When
            using var input = inputFile.OpenRead();
            using var output = outputFile.OpenWrite();
            deiCrypt.Encode(input, output, compress);

            // Then

            await Verify(output.Position);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task EncodeAndDecode(bool compressed)
        {
            // Given
            var fileSystem = new FakeFileSystem(FakeEnvironment.CreateMacOSEnvironment());
            var inputFile = fileSystem.CreateFile("/plain.txt", "NSA"u8.ToArray());
            var interimFile = fileSystem.CreateFile("/encoded.ally");
            var outputFile = fileSystem.CreateFile("/decoded.txt");
            var deiCrypt = new Core.DeiCrypt();

            // When
            {
                using var input = inputFile.OpenRead();
                using var interim = interimFile.OpenWrite();
                deiCrypt.Encode(input, interim, compressed);
            }
            {
                using var interim = interimFile.OpenRead();
                using var output = outputFile.OpenWrite();
                deiCrypt.Decode(interim, output, compressed);
            }

            var inputText = inputFile.ReadAllText();
            var outputText = outputFile.ReadAllText();

            // Then
            await Verify(
                new {
                    inputText,
                    outputText,
                    equal = inputText == outputText,
                    compressed
                }
                );
        }
    }
}
