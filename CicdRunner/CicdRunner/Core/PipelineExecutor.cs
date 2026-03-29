using System.Collections.Generic;
using System.IO;

public class PipelineExecutor
{
    private readonly CommandRunner _runner;
    private readonly Logger _logger;

    public PipelineExecutor(CommandRunner runner, Logger logger)
    {
        _runner = runner;
        _logger = logger;
    }

    public void Execute(List<Stage> stages, string targetDir)
    {
        var repoDir = Path.Combine(targetDir, "repo");

        foreach (var stage in stages)
        {
            _logger.Info($"Starting stage: {stage.Name}");

            var workingDir = stage.Command == "git" && stage.Args.Contains("clone")
                ? targetDir
                : repoDir;

            if (!Directory.Exists(repoDir) && workingDir == repoDir)
                Directory.CreateDirectory(repoDir);

            var exitCode = _runner.Run(stage.Command, stage.Args, workingDir, _logger);

            if (exitCode == 0)
            {
                _logger.Success($"Stage finished with ExitCode {exitCode}");
            }
            else
            {
                _logger.Error($"Stage failed with ExitCode {exitCode}");

                if (stage.StopOnFailure)
                {
                    _logger.Error("Stopping pipeline.");
                    break;
                }
            }
        }
    }
} 