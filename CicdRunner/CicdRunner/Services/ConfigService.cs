using System.IO;
using System.Text.Json;

public class ConfigService
{
    public PipelineConfig Load(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

        var json = File.ReadAllText(path);

        var config = JsonSerializer.Deserialize<PipelineConfig>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (config == null || config.Pipeline == null || config.Pipeline.Count == 0)
            throw new InvalidDataException();

        return config;
    }
}