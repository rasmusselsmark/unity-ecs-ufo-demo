using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    public Entity Prefab;
    public int Count;
    public float RangeX;
    public float RangeY;
    public float RangeZ;
    public float MaxSpeed;
}
