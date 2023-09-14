using UnityEngine;
using Unity.Entities;

class UfoSpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public int Count;
    public float RangeX = 20f;
    public float RangeY = 10f;
    public float RangeZ = 20f;
    public float MaxSpeed = 3f;
}

class SpawnerBaker : Baker<UfoSpawnerAuthoring>
{
    public override void Bake(UfoSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        var prefab = GetEntity(authoring.Prefab, TransformUsageFlags.None);

        AddComponent(entity, new UfoSpawnerData
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
