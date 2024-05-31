using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSMBot.Objs;

public record StatusObj : BaseObj
{
    public record DataObj
    {
        [JsonProperty("instanceUuid")]
        public string InstanceUuid { get; set; }
    }
    [JsonProperty("data")]
    public DataObj Data { get; set; }
}
