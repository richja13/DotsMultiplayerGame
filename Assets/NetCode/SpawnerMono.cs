using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerMono : MonoBehaviour
{
    public GameObject Player1;
}

public class SpawnerBaker : Baker<SpawnerMono>
{
    public override void Bake(SpawnerMono authoring)
    {
        Spawner spawner = default;
        spawner.Player1 = GetEntity(authoring.Player1);
        AddComponent(spawner);
    }
}
