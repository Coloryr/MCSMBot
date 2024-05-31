using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MCSMBot.Objs;

public record ServiceObj : BaseObj
{
    public record DataObj
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("port")]
        public ushort Port { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("available")]
        public bool Available { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
    [JsonProperty("data")]
    public List<DataObj> Data { get; set; }
}
