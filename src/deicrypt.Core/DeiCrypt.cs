namespace deicrypt.Core;
using System.IO.Compression;

/// <summary>
/// Provides encoding and decoding functionality using DEI-related terms padded by safe spaces.
/// </summary>
public readonly ref struct DeiCrypt()
{
    private readonly ReadOnlySpan<byte> GetWordByIndex(int index) => index switch
    {
        0 => AntiRacism,
        1 => Racism,
        2 => Allyship,
        3 => Bias,
        4 => DEI,
        5 => Diversity,
        6 => Diverse,
        7 => ConfirmationBias,
        8 => Equity,
        9 => Equitableness,
        10 => Feminism,
        11 => Gender,
        12 => GenderIdentity,
        13 => Inclusion,
        14 => Inclusive,
        15 => AllInclusive,
        16 => Inclusivity,
        17 => Injustice,
        18 => Intersectionality,
        19 => Prejudice,
        20 => Privilege,
        21 => RacialIdentity,
        22 => Sexuality,
        23 => Stereotypes,
        24 => Pronouns,
        25 => Transgender,
        26 => Equality,
        _ => throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 26")
    };

    private readonly int GetIndexByWord(ReadOnlySpan<byte> word)
    {
        if (word.SequenceEqual(AntiRacism)) return 0;
        if (word.SequenceEqual(Racism)) return 1;
        if (word.SequenceEqual(Allyship)) return 2;
        if (word.SequenceEqual(Bias)) return 3;
        if (word.SequenceEqual(DEI)) return 4;
        if (word.SequenceEqual(Diversity)) return 5;
        if (word.SequenceEqual(Diverse)) return 6;
        if (word.SequenceEqual(ConfirmationBias)) return 7;
        if (word.SequenceEqual(Equity)) return 8;
        if (word.SequenceEqual(Equitableness)) return 9;
        if (word.SequenceEqual(Feminism)) return 10;
        if (word.SequenceEqual(Gender)) return 11;
        if (word.SequenceEqual(GenderIdentity)) return 12;
        if (word.SequenceEqual(Inclusion)) return 13;
        if (word.SequenceEqual(Inclusive)) return 14;
        if (word.SequenceEqual(AllInclusive)) return 15;
        if (word.SequenceEqual(Inclusivity)) return 16;
        if (word.SequenceEqual(Injustice)) return 17;
        if (word.SequenceEqual(Intersectionality)) return 18;
        if (word.SequenceEqual(Prejudice)) return 19;
        if (word.SequenceEqual(Privilege)) return 20;
        if (word.SequenceEqual(RacialIdentity)) return 21;
        if (word.SequenceEqual(Sexuality)) return 22;
        if (word.SequenceEqual(Stereotypes)) return 23;
        if (word.SequenceEqual(Pronouns)) return 24;
        if (word.SequenceEqual(Transgender)) return 25;
        if (word.SequenceEqual(Equality)) return 26;
        throw new ArgumentOutOfRangeException(nameof(word), "Word not found");
    }

    private readonly ReadOnlySpan<byte> AntiRacism 
        = "Anti-Racism         "u8;
    private readonly ReadOnlySpan<byte> Racism 
        = "Racism              "u8;
    private readonly ReadOnlySpan<byte> Allyship 
        = "Allyship            "u8;
    private readonly ReadOnlySpan<byte> Bias 
        = "Bias                "u8;
    private readonly ReadOnlySpan<byte> DEI 
        = "DEI                 "u8;
    private readonly ReadOnlySpan<byte> Diversity 
        = "Diversity           "u8;
    private readonly ReadOnlySpan<byte> Diverse 
        = "Diverse             "u8;
    private readonly ReadOnlySpan<byte> ConfirmationBias 
        = "Confirmation Bias   "u8;
    private readonly ReadOnlySpan<byte> Equity 
        = "Equity              "u8;
    private readonly ReadOnlySpan<byte> Equitableness 
        = "Equitableness       "u8;
    private readonly ReadOnlySpan<byte> Feminism 
        = "Feminism            "u8;
    private readonly ReadOnlySpan<byte> Gender 
        = "Gender              "u8; 
    private readonly ReadOnlySpan<byte> GenderIdentity 
        = "Gender Identity     "u8;
    private readonly ReadOnlySpan<byte> Inclusion 
        = "Inclusion           "u8;
    private readonly ReadOnlySpan<byte> Inclusive 
        = "Inclusive           "u8;
    private readonly ReadOnlySpan<byte> AllInclusive 
        = "All-Inclusive       "u8;
    private readonly ReadOnlySpan<byte> Inclusivity 
        = "Inclusivity         "u8;
    private readonly ReadOnlySpan<byte> Injustice 
        = "Injustice           "u8;
    private readonly ReadOnlySpan<byte> Intersectionality 
        = "Intersectionality   "u8;
    private readonly ReadOnlySpan<byte> Prejudice 
        = "Prejudice           "u8;
    private readonly ReadOnlySpan<byte> Privilege 
        = "Privilege           "u8;
    private readonly ReadOnlySpan<byte> RacialIdentity 
        = "Racial Identity     "u8;
    private readonly ReadOnlySpan<byte> Sexuality 
        = "Sexuality           "u8;
    private readonly ReadOnlySpan<byte> Stereotypes 
        = "Stereotypes         "u8;
    private readonly ReadOnlySpan<byte> Pronouns 
        = "Pronouns            "u8;
    private readonly ReadOnlySpan<byte> Transgender 
        = "Transgender         "u8;
    private readonly ReadOnlySpan<byte> Equality 
        = "Equality            "u8;

    /// <summary>
    /// Encodes a single byte into a pair of DEI terms.
    /// </summary>
    /// <param name="value">The byte value to encode.</param>
    /// <returns>A 40-byte span containing two 20-byte DEI terms padded by safe spaces.</returns>
    public readonly ReadOnlySpan<byte> EncodeByte(byte value)
    {
        var quotient = value / 27;
        var remainder = value % 27;
        
        var word = GetWordByIndex(quotient);
        var word2 = GetWordByIndex(remainder);
        
        var result = new byte[40];
        word.CopyTo(result);
        word2.CopyTo(result.AsSpan(20));
        
        return result;
    }

    /// <summary>
    /// Decodes a pair of DEI terms back into the original byte value.
    /// </summary>
    /// <param name="value">A 40-byte span containing two 20-byte DEI terms.</param>
    /// <returns>The decoded byte value.</returns>
    public readonly byte DecodeByte(ReadOnlySpan<byte> value)
    {
        var index = GetIndexByWord(value.Slice(0, 20));
        var index2 = GetIndexByWord(value.Slice(20, 20));
        return (byte)(index * 27 + index2);
    }

    /// <summary>
    /// Encodes a sequence of bytes into DEI terms.
    /// </summary>
    /// <param name="input">The bytes to encode.</param>
    /// <returns>A span containing the encoded DEI terms padded by safe spaces.</returns>
    public readonly ReadOnlySpan<byte> Encode(ReadOnlySpan<byte> input)
    {
        var result = new byte[input.Length * 40];
        for (int i = 0; i < input.Length; i++)
        {
            EncodeByte(input[i]).CopyTo(result.AsSpan(i * 40));
        }
        return result;
    }

    /// <summary>
    /// Decodes a sequence of DEI terms back into the original bytes.
    /// </summary>
    /// <param name="input">The DEI terms to decode.</param>
    /// <returns>A span containing the decoded bytes.</returns>
    /// <exception cref="ArgumentException">Thrown when input length is not a multiple of 40.</exception>
    public readonly ReadOnlySpan<byte> Decode(ReadOnlySpan<byte> input)
    {
        if (input.Length % 40 != 0)
            throw new ArgumentException("Input length must be a multiple of 40", nameof(input));
            
        var result = new byte[input.Length / 40];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = DecodeByte(input.Slice(i * 40, 40));
        }
        return result;
    }

    /// <summary>
    /// Encodes bytes from an input stream into DEI terms and writes them to an output stream.
    /// </summary>
    /// <param name="input">The stream containing bytes to encode.</param>
    /// <param name="output">The stream to write encoded DEI terms padded by safe spaces to.</param>
    /// <exception cref="ArgumentException">Thrown when input length is not a multiple of 40.</exception>
    public readonly void Encode(Stream input, Stream output)
    {
        Encode(input, output, false);
    }

    /// <summary>
    /// Decodes DEI terms from an input stream back into bytes and writes them to an output stream.
    /// </summary>
    /// <param name="input">The stream containing DEI terms to decode.</param>
    /// <param name="output">The stream to write decoded bytes to.</param>
    /// <exception cref="ArgumentException">Thrown when input length is not a multiple of 40.</exception>
    public readonly void Decode(Stream input, Stream output)
    {
        Decode(input, output, false);
    }

    public readonly void Encode(Stream input, Stream output, bool compress)
    {
        using var reader = new BinaryReader(input);
        using var compressStream = compress ? new BrotliStream(output, CompressionLevel.SmallestSize) : null;
        using var writeStream = new BufferedStream(compress ? compressStream! : output);
        
        while (input.Position < input.Length)
        {
            var value = reader.ReadByte();
            var encoded = EncodeByte(value);
            writeStream.Write(encoded);
        }
    }

    public readonly void Decode(Stream input, Stream output, bool compressed)
    {
        using var stream = compressed ? new BrotliStream(input, CompressionMode.Decompress) : input;
        using var reader = new BinaryReader(stream);
        using var writer = new BinaryWriter(output);

        if (!compressed && input.Length % 40 != 0)
            throw new ArgumentException("Input length must be a multiple of 40", nameof(input));
        
        var buffer = new byte[40];
        while (reader.Read(buffer, 0, 40)>0)
        {
            var decoded = DecodeByte(buffer);
            writer.Write(decoded);
        }
    }
}