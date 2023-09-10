#if !USE_JOBS
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

public partial struct UfoMovementSystem_SystemAPI : ISystem
{
    private Random _random;

    public void OnCreate(ref SystemState state)
    {
        _random = new Random((uint)SystemAPI.Time.ElapsedTime + 1);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, movement) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MovementData>>())
        {
            transform.ValueRW = transform.ValueRW.Translate(
                transform.ValueRW.Forward() * movement.ValueRO.Speed * SystemAPI.Time.DeltaTime);

            if (_random.NextFloat() > 0.98f)
            {
                transform.ValueRW = transform.ValueRW.RotateY(_random.NextInt(0, 360));
            }
        }
    }
}
#endif