#if !USE_I_JOB_ENTITY
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

/// <summary>
/// Iterate using SystemAPI
/// See https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/systems-systemapi-query.html
/// </summary>
public partial struct UfoMovementSystem_SystemAPI : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, movement) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MovementData>>())
        {
            transform.ValueRW = transform.ValueRW.Translate(
                transform.ValueRW.Forward() * movement.ValueRO.Speed * SystemAPI.Time.DeltaTime);

            var n = noise.snoise(transform.ValueRO.Position);
            if (n > 0.5f)
            {
                // use snoise instead of random, to allow threading
                var rotation = noise.snoise(new float4(transform.ValueRO.Position * 100f, 0.5f));
                transform.ValueRW = transform.ValueRW.RotateY(rotation);
            }
        }
    }
}
#endif
