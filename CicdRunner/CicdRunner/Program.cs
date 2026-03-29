using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: dotnet run <configPath> <targetDir>");
                return;
            }

            var configPath = args[0];
            var targetDir = args[1];

            if (!File.Exists(configPath))
            {
                Console.WriteLine("Config not found");
                return;
            }

            Directory.CreateDirectory(targetDir);

            var config = new ConfigService().Load(configPath);

            var logName = $"CICD_{new DirectoryInfo(targetDir).Name}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.log";
            var logPath = Path.Combine(targetDir, logName);

            var logger = new Logger(logPath);

            logger.Info("Pipeline started");

            var executor = new PipelineExecutor(new CommandRunner(), logger);
            executor.Execute(config.Pipeline, targetDir);

            logger.Info("Pipeline finished");

            Console.WriteLine($"Done. Log: {logPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}  