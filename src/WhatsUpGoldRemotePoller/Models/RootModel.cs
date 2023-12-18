namespace WhatsUpGoldRemotePoller.Models;

public class RootModel
{
    public Data Data { get; set; } = new();
}

public class Data
{
    public bool Health { get; set; } = true;
    public string Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
}
