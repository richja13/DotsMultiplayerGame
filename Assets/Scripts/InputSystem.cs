

using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[UpdateInGroup((typeof(GhostInputSystemGroup)))]
public partial struct InputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Spawner>();
        state.RequireForUpdate<PlayerData>();
        state.RequireForUpdate<NetworkId>();
    }

    public void OnUpdate(ref SystemState state)
    {
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        bool up = Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.S);

        foreach(var input in SystemAPI.Query<RefRW<PlayerData>>().WithAll<GhostOwnerIsLocal>())
        {
            input.ValueRW = default;
            input.ValueRW.Horizontal += (left && !right) ? -1 : (right && !left) ? 1 : 0;
            input.ValueRW.Vertical += (up && !down) ? 1 : (down && !up) ? -1 : 0;
        }
    }
}
