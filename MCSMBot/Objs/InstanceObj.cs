using Newtonsoft.Json;

namespace MCSMBot.Objs;

public record InstanceObj : BaseObj
{
    public record DataObj
    {
        public record Data1Obj
        {
            public record ConfigObj
            {
                public record TerminalOptionObj
                {
                    [JsonProperty("haveColor")]
                    public bool HaveColor { get; set; }
                    [JsonProperty("pty")]
                    public bool Pty { get; set; }
                    [JsonProperty("ptyWindowCol")]
                    public int PtyWindowCol { get; set; }
                    [JsonProperty("ptyWindowRow")]
                    public int PtyWindowRow { get; set; }
                }
                public record EventTaskObj
                {
                    [JsonProperty("autoStart")]
                    public bool AutoStart { get; set; }
                    [JsonProperty("autoRestart")]
                    public bool AutoRestart { get; set; }
                    [JsonProperty("ignore")]
                    public bool Ignore { get; set; }
                }
                public record DockerObj
                {
                    [JsonProperty("containerName")]
                    public string ContainerName { get; set; }
                    [JsonProperty("image")]
                    public string Image { get; set; }
                    [JsonProperty("ports")]
                    public List<string> Ports { get; set; }
                    [JsonProperty("extraVolumes")]
                    public List<string> ExtraVolumes { get; set; }
                    [JsonProperty("memory")]
                    public int? Memory { get; set; }
                    [JsonProperty("networkMode")]
                    public string NetworkMode { get; set; }
                    [JsonProperty("networkAliases")]
                    public List<object> NetworkAliases { get; set; }
                    [JsonProperty("cpusetCpus")]
                    public string CpusetCpus { get; set; }
                    [JsonProperty("cpuUsage")]
                    public int? CpuUsage { get; set; }
                    [JsonProperty("maxSpace")]
                    public int? MaxSpace { get; set; }
                    [JsonProperty("io")]
                    public int? IO { get; set; }
                    [JsonProperty("network")]
                    public int? Network { get; set; }
                    [JsonProperty("workingDir")]
                    public string WorkingDir { get; set; }
                    [JsonProperty("env")]
                    public List<object> Env { get; set; }
                }
                public record PingConfigObj
                {
                    [JsonProperty("ip")]
                    public string IP { get; set; }
                    [JsonProperty("port")]
                    public ushort Port { get; set; }
                    [JsonProperty("type")]
                    public int Type { get; set; }
                }
                public record ExtraServiceConfigObj
                {
                    [JsonProperty("openFrpTunnelId")]
                    public string OpenFrpTunnelId { get; set; }
                    [JsonProperty("openFrpToken")]
                    public string OpenFrpToken { get; set; }
                }
                [JsonProperty("nickname")]
                public string Nickname { get; set; }
                [JsonProperty("startCommand")]
                public string StartCommand { get; set; }
                [JsonProperty("stopCommand")]
                public string StopCommand { get; set; }
                [JsonProperty("cwd")]
                public string Cwd { get; set; }
                [JsonProperty("ie")]
                public string InputEncoding { get; set; }
                [JsonProperty("oe")]
                public string OutputEncoding { get; set; }
                [JsonProperty("createDatetime")]
                public string CreateDatetime { get; set; }
                [JsonProperty("lastDatetime")]
                public long LastDatetime { get; set; }
                [JsonProperty("type")]
                public string Type { get; set; }
                [JsonProperty("tag")]
                public List<object> Tag { get; set; }
                [JsonProperty("endTime")]
                public string EndTime { get; set; }
                [JsonProperty("fileCode")]
                public string FileCode { get; set; }
                [JsonProperty("processType")]
                public string ProcessType { get; set; }
                [JsonProperty("updateCommand")]
                public string UpdateCommand { get; set; }
                [JsonProperty("crlf")]
                public int Crlf { get; set; }
                [JsonProperty("enableRcon")]
                public bool EnableRcon { get; set; }
                [JsonProperty("rconPassword")]
                public string RconPassword { get; set; }
                [JsonProperty("rconPort")]
                public ushort RconPort { get; set; }
                [JsonProperty("rconIp")]
                public string RconIp { get; set; }
                [JsonProperty("actionCommandList")]
                public List<object> ActionCommandList { get; set; }
                [JsonProperty("terminalOption")]
                public TerminalOptionObj TerminalOption { get; set; }
                [JsonProperty("eventTask")]
                public EventTaskObj EventTask { get; set; }
                [JsonProperty("docker")]
                public DockerObj Docker { get; set; }
                [JsonProperty("pingConfig")]
                public PingConfigObj pingConfig { get; set; }
                [JsonProperty("extraServiceConfig")]
                public ExtraServiceConfigObj ExtraServiceConfig { get; set; }
            }
            public record InfoObj
            {
                [JsonProperty("currentPlayers")]
                public int CurrentPlayers { get; set; }
                [JsonProperty("maxPlayers")]
                public int MaxPlayers { get; set; }
                [JsonProperty("version")]
                public string Version { get; set; }
                [JsonProperty("fileLock")]
                public int FileLock { get; set; }
                [JsonProperty("playersChart")]
                public List<object> PlayersChart { get; set; }
                [JsonProperty("openFrpStatus")]
                public bool OpenFrpStatus { get; set; }
            }
            [JsonProperty("instanceUuid")]
            public string InstanceUuid { get; set; }
            [JsonProperty("started")]
            public int Started { get; set; }
            [JsonProperty("status")]
            public int Status { get; set; }
            [JsonProperty("config")]
            public ConfigObj config { get; set; }
            [JsonProperty("info")]
            public InfoObj Info { get; set; }
        }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("PageSize")]
        public int PageSize { get; set; }
        [JsonProperty("MaxPage")]
        public int MaxPage { get; set; }
        [JsonProperty("data")]
        public List<Data1Obj> Data { get; set; }
    }
    [JsonProperty("data")]
    public DataObj Data { get; set; }
}
