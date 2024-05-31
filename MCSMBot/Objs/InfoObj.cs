using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MCSMBot.Objs;

public record InfoObj : BaseObj
{
    public record DataObj
    {
        public record ProcessObj
        {
            [JsonProperty("cpu")]
            public long Cpu { get; set; }
            [JsonProperty("memory")]
            public long Memory { get; set; }
            [JsonProperty("cwd")]
            public string Cwd { get; set; }
        }
        public record InstanceObj
        {
            [JsonProperty("running")]
            public int Running { get; set; }
            [JsonProperty("total")]
            public int Total { get; set; }
        }
        public record SystemObj
        {
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("hostname")]
            public string Hostname { get; set; }
            [JsonProperty("platform")]
            public string Platform { get; set; }
            [JsonProperty("release")]
            public string Release { get; set; }
            [JsonProperty("uptime")]
            public long Uptime { get; set; }
            [JsonProperty("cwd")]
            public string Cwd { get; set; }
            [JsonProperty("loadavg")]
            public List<double> Loadavg { get; set; }
            [JsonProperty("freemem")]
            public long Freemem { get; set; }
            [JsonProperty("cpuUsage")]
            public double CpuUsage { get; set; }
            [JsonProperty("memUsage")]
            public double MemUsage { get; set; }
            [JsonProperty("totalmem")]
            public long Totalmem { get; set; }
            [JsonProperty("processCpu")]
            public long ProcessCpu { get; set; }
            [JsonProperty("processMem")]
            public long ProcessMem { get; set; }
        }
        public record CpuMemChartObj
        {
            [JsonProperty("cpu")]
            public int Cpu { get; set; }
            [JsonProperty("mem")]
            public int Memory { get; set; }
        }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("process")]
        public ProcessObj Process { get; set; }
        [JsonProperty("instance")]
        public InstanceObj Instance { get; set; }
        [JsonProperty("system")]
        public SystemObj System{ get; set; }
        [JsonProperty("cpuMemChart")]
        public List<CpuMemChartObj> CpuMemChart { get; set; }
    }
    [JsonProperty("data")]
    public List<DataObj> Data { get; set; }
}
