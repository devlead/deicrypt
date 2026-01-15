/*****************************
 * Helpers
 *****************************/

public partial class Program
{
    static void Main_SetupExtensions()
    {        
        if (GitHubActions.IsRunningOnGitHubActions)
        {
            TaskSetup(context => GitHubActions.Commands.StartGroup(context.Task.Name));
            TaskTeardown(context => GitHubActions.Commands.EndGroup());
        }
    }
}


public static partial class CakeTaskBuilderExtensions
{
    private static ExtensionHelper extensionHelper = new (Task, () => RunTarget(Argument("target", "Default")));

    public static CakeTaskBuilder Then(this CakeTaskBuilder cakeTaskBuilder, string name)
        => extensionHelper
            .TaskCreate(name)
            .IsDependentOn(cakeTaskBuilder);


    public static CakeReport Run(this CakeTaskBuilder cakeTaskBuilder)
        => extensionHelper.Run();

    public static CakeTaskBuilder Default(this CakeTaskBuilder cakeTaskBuilder)
    {
        extensionHelper
            .TaskCreate("Default")
            .IsDependentOn(cakeTaskBuilder);
        return cakeTaskBuilder;
    }

}