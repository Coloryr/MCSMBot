using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSMBot;

public record ConfigObj
{
    public string Url { get; set; }
    public string Key { get; set; }
    public string BotUrl { get; set; }
    public string? BotKey { get; set; }
    public List<long> Groups { get; set; }
    public List<long> Users { get; set; }

    public string Head { get; set; }
    public string List { get; set; }
    public string Start { get; set; }
    public string Stop { get; set; }
    public string Kill { get; set; }
    public string Restart { get; set; }
    public string Command { get; set; }
}
