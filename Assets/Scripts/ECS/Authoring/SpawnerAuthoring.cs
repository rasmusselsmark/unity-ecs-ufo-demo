using UnityEngine;
using Unity.Entities;

class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public int Count;
    public float RangeX = 20f;
    public float RangeY = 10f;
    public float RangeZ = 20f;
    public float MaxSpeed = 3f;
}

class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        var prefab = GetEntity(authoring.Prefab, TransformUsageFlags.None);

        AddComponent(entity, new Spawner
        {
            Prefab = prefab,
            Count = authoring.Count,
            RangeX = authoring.RangeX,
            RangeY = authoring.RangeY,
            RangeZ = authoring.RangeZ,
            MaxSpeed = authoring.MaxSpeed,
        });
    }
}
