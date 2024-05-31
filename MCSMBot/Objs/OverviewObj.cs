using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSMBot.Objs;

public record OverviewObj : BaseObj
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
        public record RecordObj
        {
            [JsonProperty("logined")]
            public int Logined { get; set; }
            [JsonProperty("illegalAccess")]
            public int IllegalAccess { get; set; }
            [JsonProperty("banips")]
            public int Banips { get; set; }
            [JsonProperty("loginFailed")]
            public int LoginFailed { get; set; }
        }
        public record SystemObj
        {
            public record UserObj
            {
                [JsonProperty("uid")]
                public int Uid { get; set; }
                [JsonProperty("gid")]
                public int Gid { get; set; }
                [JsonProperty("username")]
                public string Username { get; set; }
                [JsonProperty("homedir")]
                public string Homedir { get; set; }
                [JsonProperty("shell")]
                public string Shell { get; set; }
            }
            [JsonProperty("user")]
            public UserObj User { get; set; }
            [JsonProperty("time")]
            public long Time { get; set; }
            [JsonProperty("totalmem")]
            public long Totalmem { get; set; }
            [JsonProperty("freemem")]
            public long Freemem { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("version")]
            public string Version { get; set; }
            [JsonProperty("node")]
            public string Node { get; set; }
            [JsonProperty("hostname")]
            public string Hostname { get; set; }
            [JsonProperty("loadavg")]
            public List<float> Loadavg { get; set; }
            [JsonProperty("platform")]
            public string Platform { get; set; }
            [JsonProperty("release")]
            public string Release { get; set; }
            [JsonProperty("uptime")]
            public double Uptime { get; set; }
            [JsonProperty("cpu")]
            public double Cpu { get; set; }
        }
        public record ChartObj
        {
            public record SystemObj
            {
                [JsonProperty("cpu")]
                public float Cpu { get; set; }
                [JsonProperty("mem")]
                public float Mem { get; set; }
            }
            public record RequestObj
            {
                [JsonProperty("value")]
                public int Value { get; set; }
                [JsonProperty("totalInstance")]
                public int TotalInstance { get; set; }
                [JsonProperty("runningInstance")]
                public int RunningInstance { get; set; }
            }
            [JsonProperty("system")]
            public List<SystemObj> System { get; set; }
            [JsonProperty("request")]
            public List<RequestObj> Request { get; set; }
        }
        public record RemoteCountObj
        {
            [JsonProperty("available")]
            public int Available { get; set; }
            [JsonProperty("total")]
            public int Total { get; set; }
        }
        public record RemoteObj : InfoObj.DataObj
        {
            [JsonProperty("uuid")]
            public string UUID { get; set; }
            [JsonProperty("ip")]
            public string IP { get; set; }
            [JsonProperty("port")]
            public string Port { get; set; }
            [JsonProperty("prefix")]
            public string Prefix { get; set; }
            [JsonProperty("remarks")]
            public string Remarks { get; set; }
            [JsonProperty("available")]
            public bool Available { get; set; }
        }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("specifiedDaemonVersion")]
        public string SpecifiedDaemonVersion { get; set; }
        [JsonProperty("process")]
        public ProcessObj Process { get; set; }
        [JsonProperty("record")]
        public RecordObj Record { get; set; }
        [JsonProperty("system")]
        public SystemObj System { get; set; }
        [JsonProperty("chart")]
        public ChartObj Chart { get; set; }
        [JsonProperty("remoteCount")]
        public RemoteCountObj RemoteCount { get; set; }
        [JsonProperty("remote")]
        public List<RemoteObj> Remote { get; set; }
    }
    [JsonProperty("data")]
    public DataObj Data { get; set; }
}
