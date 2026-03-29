using System.Collections.Generic;

public class PipelineConfig
{
    public List<Stage> Pipeline { get; set; } = new();
}

public class Stage
{
    public string Name { get; set; } = "";
    public string Command { get; set; } = "";
    public string Args { get; set; } = "";
    public bool StopOnFailure { get; set; }
}