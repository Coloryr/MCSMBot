using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MCSMBot.Objs;

public record BaseObj
{
    [JsonProperty("status")]
    public int Status { get; set; }
    [JsonProperty("time")]
    public long Time { get; set; }
}
