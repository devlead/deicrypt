namespace deicrypt.Tests;

public class DeiCryptTests
{
    [Test]
    public async Task Encode()
    {
        // Given
        var input = "NSA"u8;
        var deiCrypt = new Core.DeiCrypt();

        // When
        ReadOnlySpan<byte> result = deiCrypt.Encode(input);

        // Then
        await Verify(System.Text.Encoding.UTF8.GetString(result));
    }

    [Test]
    public async Task Decode()
    {
        // Given
        var input = "Allyship            Pronouns            Bias                Allyship            Allyship            Gender              "u8;
        var deiCrypt = new Core.DeiCrypt();

        // When
        ReadOnlySpan<byte> result = deiCrypt.Decode(input);

        // Then
        await Verify(System.Text.Encoding.UTF8.GetString(result));
    }

    [Test]
    public void EncodeAndDecode()
    {
        // Given
        var input = "NSA"u8;
        var deiCrypt = new Core.DeiCrypt();

        // When
        ReadOnlySpan<byte> result = deiCrypt.Decode(deiCrypt.Encode(input));

        // Then
        Assert.That(input.SequenceEqual(result));
    }
}
