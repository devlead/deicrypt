using deicrypt;

public partial class Program
{
    static partial void AddServices(IServiceCollection services)
    {
        services.AddSingleton(Spectre.IO.FileSystem.Shared);
    }

    static partial void ConfigureApp(AppServiceConfig appServiceConfig)
    {
        appServiceConfig.SetApplicationName("deicrypt");
        
        appServiceConfig.AddCommand<EncodeCommand>("encode")
                    .WithDescription("Encode file")
                    .WithExample(["encode", "inputfile", "outputfile"])
                    .WithExample(["encode", "inputfile", "outputfile", "--compress"]);

        appServiceConfig.AddCommand<DecodeCommand>("decode")
                    .WithDescription("Decode file")
                    .WithExample(["decode", "inputfile", "outputfile"])
                    .WithExample(["decode", "inputfile", "outputfile", "--decompress"]);
    }
}
