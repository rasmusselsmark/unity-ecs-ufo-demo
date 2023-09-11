using Unity.Entities;
using Unity.Mathematics;

public struct MovementData : IComponentData
    {
        public float Speed;
        public float3 Destination;
    }