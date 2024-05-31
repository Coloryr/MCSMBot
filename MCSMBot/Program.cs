using OneBotSharp.Objs.Event;
using OneBotSharp.Objs.Message;
using OneBotSharp.Protocol;

namespace MCSMBot;

internal class Program
{
    private static ConfigObj s_config;
    private static readonly Dictionary<string, string> s_nodes = [];
    private static readonly Dictionary<string, Dictionary<string, string>> s_instances = [];
    static async Task Main()
    {
        var con = ConfigUtils.Config(new ConfigObj()
        {
            Url = "http://localhost:23333/",
            Key = "",
            BotUrl = "ws://localhost:8081/",
            Groups = [],
            Users = [],
            Head = "/mcsm",
            List = "list",
            Start = "start",
            Stop = "stop",
            Kill = "kill",
            Restart = "restart",
            Command = "command"
        }, "Config.json");

        con.Groups ??= [];
        con.Users ??= [];

        if (string.IsNullOrWhiteSpace(con.Key))
        {
            Console.WriteLine("Key为空，请修改配置文件后启动");
            Console.WriteLine("按下任意键关闭");
            Console.Read();
            return;
        }

        MCSMHttp.Init(con.Url, con.Key);

        s_config = con;
        var data = MCSMHttp.GetOverview().Result;
        if (data == null || data.Status != 200)
        {
            Console.WriteLine("获取MCSM面板数据失败，请检查Url和Key");
            return;
        }
        s_nodes.Clear();
        s_instances.Clear();
        foreach (var item in data.Data.Remote)
        {
            s_nodes.Add(item.Remarks.ToLower(), item.UUID);
            var data1 = await MCSMHttp.GetInstances(item.UUID);
            if (data1 == null || data1.Status != 200)
            {
                Console.WriteLine("获取MCSM实例数据失败，请检查Url和Key");
                return;
            }
            var dir1 = new Dictionary<string, string>();
            foreach (var item1 in data1.Data.Data)
            {
                dir1.Add(item1.config.Nickname, item1.InstanceUuid);
            }
            s_instances.Add(item.UUID, dir1);
        }

        RobotCore.Start(con.BotUrl, con.BotKey);

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "stop")
            {
                RobotCore.Stop();
                return;
            }
        }
    }

    public static async void Message(EventGroupMessage message)
    {
        if (!s_config.Groups.Contains(message.GroupId) 
            || !s_config.Users.Contains(message.UserId)
            || !message.RawMessage.StartsWith(s_config.Head))
        {
            return;
        }

        try
        {
            var args = message.RawMessage.Split(' ');
            if (args.Length == 1)
            {
                RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build("正在获取MCSM服务器信息数据")]);
                var data = await MCSMHttp.GetOverview();
                if (data == null || data.Status != 200)
                {
                    RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build("获取MCSM服务器信息数据错误")]);
                    return;
                }
                var list = new List<MsgBase>()
                {
                    MsgText.Build($"MCSM版本：{data.Data.Version} \n后端版本：{data.Data.SpecifiedDaemonVersion}\n"),
                    MsgText.Build($"Node版本：{data.Data.System.Node}-{data.Data.System.Release}\n"),
                    MsgText.Build($"系统：{data.Data.System.Type}-{data.Data.System.Release}\n"),
                    MsgText.Build($"Cpu:{data.Data.System.Cpu * 100:.##}% Mem:{(double)(data.Data.System.Totalmem - data.Data.System.Freemem) / 1024 / 1024 / 1024:.##}/{(double)data.Data.System.Totalmem / 1024 / 1024 / 1024:.##}G\n"),
                    MsgText.Build($"后端节点：{data.Data.RemoteCount.Available}/{data.Data.RemoteCount.Total}\n")
                };
                s_nodes.Clear();
                foreach (var item in data.Data.Remote)
                {
                    s_nodes.Add(item.Remarks, item.UUID);
                    list.Add(MsgText.Build($"\nMCSM节点：{item.Remarks} {(item.Available ? "在线" : "离线")}\n"));
                    list.Add(MsgText.Build($"版本：{data.Data.Version}\n"));
                    list.Add(MsgText.Build($"系统：{item.System.Type}-{item.System.Release}\n"));
                    list.Add(MsgText.Build($"Cpu:{item.System.CpuUsage * 100:.##}% Mem:{item.System.MemUsage * 100:.##}%\n"));
                    list.Add(MsgText.Build($"节点实例：{item.Instance.Running}/{item.Instance.Total}"));
                }
                RobotCore.SendGroupMessage(message.GroupId, list);
            }
            else if (args.Length > 1)
            {
                if (args[1] == s_config.List)
                {
                    if (args.Length != 3)
                    {
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"错误的参数，正确参数为{s_config.Head} {s_config.List} [节点名字]")]);
                        return;
                    }
                    var name = args[2].ToLower().Trim();
                    if (!s_nodes.TryGetValue(name, out string? value))
                    {
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"没有找到节点{name}，输入{s_config.Head}可以获取节点列表")]);
                        return;
                    }

                    var data = await MCSMHttp.GetInstances(value);
                    if (data == null || data.Status != 200)
                    {
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"获取MCSM节点{name}实例数据错误")]);
                        return;
                    }
                    var list = new List<MsgBase>()
                    {
                        MsgText.Build($"MCSM节点实例：{name}\n"),
                    };

                    var dir1 = new Dictionary<string, string>();

                    foreach (var item in data.Data.Data)
                    {
                        dir1.Add(item.config.Nickname, item.InstanceUuid);
                        list.Add(MsgText.Build($"\n实例：{item.config.Nickname} {GetStateCode(item.Status)}\n"));
                        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        var dateTime = epoch.AddMilliseconds(item.config.LastDatetime);
                        list.Add(MsgText.Build($"更新：{dateTime}"));
                    }

                    s_instances.Remove(value);
                    s_instances.Add(value, dir1);

                    RobotCore.SendGroupMessage(message.GroupId, list);
                }
                else if (args.Length == 4)
                {
                    var name = args[2].ToLower().Trim();
                    var ins = args[3].ToLower().Trim();
                    if (!s_nodes.TryGetValue(name, out var uuid1)
                        || !s_instances.TryGetValue(uuid1, out var value))
                    {
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"没有找到节点{name}，输入{s_config.Head}可以获取节点列表")]);
                        return;
                    }
                    if (!value.TryGetValue(ins, out var uuid))
                    {
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"没有找到实例{ins}，输入{s_config.Head} {s_config.List} {name}可以获取实例列表")]);
                        return;
                    }

                    if (args[1] == s_config.Start)
                    {
                        var data = await MCSMHttp.InstanceStart(uuid1, uuid);
                        if (data == null || data.Status != 200)
                        {
                            RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发失败")]);
                            return;
                        }
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发")]);
                        return;
                    }
                    else if (args[1] == s_config.Stop)
                    {
                        var data = await MCSMHttp.InstanceStop(uuid1, uuid);
                        if (data == null || data.Status != 200)
                        {
                            RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发失败")]);
                            return;
                        }
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发")]);
                        return;
                    }
                    else if (args[1] ==  s_config.Kill)
                    {
                        var data = await MCSMHttp.InstanceKill(uuid1, uuid);
                        if (data == null || data.Status != 200)
                        {
                            RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发失败")]);
                            return;
                        }
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发")]);
                        return;
                    }
                    else if (args[1] == s_config.Restart)
                    {
                        var data = await MCSMHttp.InstanceRestart(uuid1, uuid);
                        if (data == null || data.Status != 200)
                        {
                            RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发失败")]);
                            return;
                        }
                        RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"指令已下发")]);
                        return;
                    }

                    RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build($"错误的指令")]);
                }
                else
                { 
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            RobotCore.SendGroupMessage(message.GroupId, [MsgText.Build("指令执行出错，请查看控制台")]);
        }
    }

    private static string GetStateCode(int state)
    {
        return state switch
        {
            -1 => "忙碌中",
            0 => "关闭",
            1 => "停止中",
            2 => "启动中",
            3 => "运行中",
            _ => "未知状态"
        };
    }
}
