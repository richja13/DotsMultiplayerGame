using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour 
{

    public float speed = 20;
    public Vector3 position;

    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            AddComponent<PlayerData>();
        }
    }
}

