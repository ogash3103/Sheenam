using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;


    var aDotNetClient = new ADotNetClient();

    var githubPipeline = new GithubPipeline
    {
        Name = "Build & Test NL Events",

        OnEvents = new ADotNet.Models.Pipelines.GithubPipelines.DotNets.Events
        {
            Push = new PushEvent
            {
                Branches = new string[] { "main" }
            },

            PullRequest = new PullRequestEvent
            {
                Branches = new string[] { "main" }
            }
        },

        Jobs = new Dictionary<string, Job>
        {
            {
              "build",
              new Job
              {
                  RunsOn = BuildMachines.Windows2022,

                  Steps = new List<GithubTask>
                  {
                      new CheckoutTaskV2
                      {
                          Name = "Check out"
                      },

                      new SetupDotNetTaskV1
                      {
                          Name = "Setup .Net",

                          TargetDotNetVersion = new TargetDotNetVersion
                          {
                              DotNetVersion = "8.0.15",
                              IncludePrerelease = true
                          }
                      },

                      new RestoreTask
                      {
                          Name = "Restore"
                      },

                      new DotNetBuildTask
                      {
                          Name = "Build"
                      },

                      new TestTask
                      {
                          Name = "Test"
                      }
                  }
              }
            }
        }
    };

var client = new ADotNetClient();

client.SerializeAndWriteToFile
    (
    adoPipeline: githubPipeline,
    path: "../../../../.github/workflows/dotnet.yml");

    /*string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
    string? directoryPath = Path.GetDirectoryName(buildScriptPath);

    if (String.IsNullOrEmpty(directoryPath) is false && Directory.Exists(directoryPath) is false)
    {
        Directory.CreateDirectory(directoryPath);
    }

    aDotNetClient.SerializeAndWriteToFile(githubPipeline, path: buildScriptPath);*/
