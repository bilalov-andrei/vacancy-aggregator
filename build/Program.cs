using System;
using SimpleExec;
using System.IO;
using System.IO.Compression;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace VacancyAggregator.Build
{
    internal class Program
    {
        private const string WebUI = "src/VacancyAggregator.WebUI/VacancyAggregator.WebUI.csproj";
        private const string Service = "./src/VacancyAggregator.Service/VacancyAggregator.Service.csproj";
        private const string Console = "./src/VacancyAggregator.Console/VacancyAggregator.Console.csproj";
        private const string HeadHunterSource = "./src/VacancyAggregator.VacancySources.HeadHunter/VacancyAggregator.VacancySources.HeadHunter.csproj";

        private static readonly string[] Projects =
        {
            WebUI,
            Service,
            Console,
            HeadHunterSource
        };

        private const string ArtifactsOutputDirectory = "artifacts/output";
        private const string ArtifactsDirectory = "artifacts";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine("Set build number. For example, build.exe 1");
                return;
            }

            var buildNumber = args[0];

            Target("default", DependsOn("pack"));

            Target("clean:lastArtefacts", () =>
            {
                if (Directory.Exists(ArtifactsDirectory)) Directory.Delete(ArtifactsDirectory, true);
            });

            Target("clean:projectOutputs", forEach: Projects, action: project =>
            {
                Run("dotnet", $"clean {project}");
            });

            Target("publish",
                DependsOn("clean:projectOutputs", "clean:lastArtefacts"),
                forEach: Projects,
                action: project =>
                {
                    Run("dotnet", $"publish {project} --configuration Release --output {Path.Join(ArtifactsOutputDirectory, Path.GetFileNameWithoutExtension(project))}");
                });

            Target("pack",
                DependsOn("publish"),
                forEach: Projects,
                action: project =>
                {
                    var projectName = Path.GetFileNameWithoutExtension(project);
                    ZipFile.CreateFromDirectory(Path.Join(ArtifactsOutputDirectory, projectName), Path.Join(ArtifactsDirectory, $"{projectName}_{buildNumber}.zip"),
                        CompressionLevel.Fastest, false);

                });

            RunTargetsAndExitAsync(new string[0]).Wait();
        }
    }
}