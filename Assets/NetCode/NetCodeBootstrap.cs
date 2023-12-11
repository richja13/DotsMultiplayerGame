using Unity.NetCode;
using UnityEngine.Scripting;

[Preserve]
public class NetCodeBootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 7777;
        return base.Initialize(defaultWorldName);
    }
}
