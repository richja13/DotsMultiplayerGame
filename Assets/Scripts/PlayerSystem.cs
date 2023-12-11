using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Collections;
using UnityEngine.Jobs;


[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
public partial class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (playerData, transform, velocity) in SystemAPI.Query<RefRW<PlayerData>, RefRW<LocalTransform>, RefRW<PhysicsVelocity>>())
        {
            transform.ValueRW.Rotation = new quaternion(0, 0, 0, 1);
            Vector3 target = new Vector3(playerData.ValueRO.Horizontal, -0.7f, playerData.ValueRO.Vertical) * deltaTime * 500;
            velocity.ValueRW.Linear = target;
            //transform.ValueRW = transform.ValueRO.Translate(new Vector3(Input.GetAxis("Player1H"), 0, Input.GetAxis("Player1V")) * cubeData.ValueRO.speed * deltaTime);
        }
    }
}
