using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraAuthoring : MonoBehaviour
{
    public float speed = 2;
    public EntityManager manager;
    public float3 offset = float3.zero;
    private LocalTransform player1Transform;
    private LocalTransform player2Transform;
    private NativeArray<Entity> entities;
    private float StartYPos;

    private void Start()
    {
        StartYPos = transform.position.y;
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        FindEntity();
    }

    public void FindEntity()
    {
        EntityQuery query1 = manager.CreateEntityQuery(ComponentType.ReadOnly<PlayerData>());
        entities = query1.ToEntityArray(Allocator.TempJob);
    }

    private void LateUpdate()
    {
        if (entities.Length > 1)
        {
            player1Transform = manager.GetComponentData<LocalTransform>(entities[0]);
            player2Transform = manager.GetComponentData<LocalTransform>(entities[1]);
        }
        else if (entities.Length == 1)
        {
            player1Transform = manager.GetComponentData<LocalTransform>(entities[0]);
            player2Transform = manager.GetComponentData<LocalTransform>(entities[0]);
        }
        else FindEntity();

        if (entities.Length < 1) return;

        Vector3 gameObjectPosition = new Vector3((player1Transform.Position.x/2 + player2Transform.Position.x/2) + 
            offset.x, CameraYPos(player1Transform.Position.x, player2Transform.Position.x, player1Transform.Position.z,
            player2Transform.Position.z, StartYPos) + offset.y, (player1Transform.Position.z/2
            + player2Transform.Position.z/2) + offset.z);

        transform.position = Vector3.Lerp(transform.position, gameObjectPosition, speed * Time.deltaTime);

    }

    float CameraYPos(float player1x, float player2x,float player1z,float player2z,float startPosY)
    {
        var xDistance = Mathf.Abs(player1x - player2x); 
        var zDistance = Mathf.Abs(player1z - player2z);
        var camY = (zDistance+xDistance > 22) ? (zDistance + xDistance) / 5 + startPosY : startPosY;
        return camY;
    }
}
