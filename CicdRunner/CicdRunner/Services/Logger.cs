using System;
using System.IO;

public class Logger
{
    private readonly string _path;
    private readonly object _lock = new();

    public Logger(string path)
    {
        _path = path;
    }

    public void Info(string message) => Write("INFO", message);
    public void Error(string message) => Write("ERROR", message);
    public void Success(string message) => Write("SUCCESS", message);

    private void Write(string level, string message)
    {
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

        lock (_lock)
        {
            File.AppendAllText(_path, line + Environment.NewLine);
        }
    }
}