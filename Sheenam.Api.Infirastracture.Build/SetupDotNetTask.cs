using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;

internal class SetupDotNetTask : GithubTask
{
    public string Name { get; set; }
    public string DotNetVersion { get; set; }
}