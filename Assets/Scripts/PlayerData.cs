using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

[GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
public struct PlayerData : IInputComponentData
{
    public int Horizontal;
    public int Vertical;
    [GhostField]
    public float speed;

}
