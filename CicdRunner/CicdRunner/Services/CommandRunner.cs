using System.Diagnostics;

public class CommandRunner
{
    public int Run(string command, string args, string workingDir, Logger logger)
    {
        var process = new Process();

        process.StartInfo.FileName = command;
        process.StartInfo.Arguments = args;
        process.StartInfo.WorkingDirectory = workingDir;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;

        try
        {
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output))
                logger.Info(output.Trim());

            if (!string.IsNullOrWhiteSpace(error))
                logger.Error(error.Trim());

            return process.ExitCode;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            return -1;
        }
    }
}