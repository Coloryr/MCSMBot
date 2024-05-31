using MCSMBot.Objs;
using Newtonsoft.Json;

namespace MCSMBot;

public static class MCSMHttp
{
    private const string Info = "/api/service/remote_services_system?apikey=";
    private const string Overview = "/api/overview?apikey=";
    private const string Services = "/api/service/remote_services_list?apikey=";
    private const string Instances = "/api/service/remote_service_instances?apikey={0}&daemonId={1}&page={2}&page_size={3}&status=&instance_name=";
    private const string Start = "/api/protected_instance/open?apikey={0}&uuid={1}&remote_uuid={2}";
    private const string Stop = "/api/protected_instance/stop?apikey={0}&uuid={1}&remote_uuid={2}";
    private const string Restart = "/api/protected_instance/restart?apikey={0}&uuid={1}&remote_uuid={2}";
    private const string Kill = "/api/protected_instance/kill?apikey={0}&uuid={1}&remote_uuid={2}";
    private const string Command = "/api/protected_instance/command?apikey={0}&uuid={1}&remote_uuid={2}&command={3}";

    private static HttpClient s_client;
    private static string s_key;
    public static void Init(string url, string key)
    {
        s_key = key;
        s_client = new()
        {
            BaseAddress = new Uri(url)
        };
    }

    public static async Task<InfoObj?> GetInfo()
    {
        var data = await s_client.GetStringAsync(Info + s_key);
        return JsonConvert.DeserializeObject<InfoObj>(data);
    }

    public static async Task<OverviewObj?> GetOverview()
    {
        var data = await s_client.GetStringAsync(Overview + s_key);
        return JsonConvert.DeserializeObject<OverviewObj>(data);
    }

    public static async Task<ServiceObj?> GetServices()
    {
        var data = await s_client.GetStringAsync(Services + s_key);
        return JsonConvert.DeserializeObject<ServiceObj>(data);
    }

    public static async Task<InstanceObj?> GetInstances(string uuid, int page = 1, int pagesize = 100)
    {
        var data = await s_client.GetStringAsync(string.Format(Instances, s_key, uuid, page, pagesize));
        return JsonConvert.DeserializeObject<InstanceObj>(data);
    }

    public static async Task<StatusObj?> InstanceStart(string daemonId, string instanceId)
    {
        var data = await s_client.GetStringAsync(string.Format(Start, s_key, instanceId, daemonId));
        return JsonConvert.DeserializeObject<StatusObj>(data);
    }

    public static async Task<StatusObj?> InstanceStop(string daemonId, string instanceId)
    {
        var data = await s_client.GetStringAsync(string.Format(Stop, s_key, instanceId, daemonId));
        return JsonConvert.DeserializeObject<StatusObj>(data);
    }

    public static async Task<StatusObj?> InstanceRestart(string daemonId, string instanceId)
    {
        var data = await s_client.GetStringAsync(string.Format(Restart, s_key, instanceId, daemonId));
        return JsonConvert.DeserializeObject<StatusObj>(data);
    }

    public static async Task<StatusObj?> InstanceKill(string daemonId, string instanceId)
    {
        var data = await s_client.GetStringAsync(string.Format(Kill, s_key, instanceId, daemonId));
        return JsonConvert.DeserializeObject<StatusObj>(data);
    }

    public static async Task<bool> InstanceCommand(string daemonId, string instanceId, string command)
    {
        var data = await s_client.GetAsync(string.Format(Command, s_key, instanceId, daemonId, command));
        return data.IsSuccessStatusCode;
    }
}
