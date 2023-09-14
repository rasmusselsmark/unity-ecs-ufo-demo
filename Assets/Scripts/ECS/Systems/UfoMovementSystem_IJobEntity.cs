#if USE_I_JOB_ENTITY
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

/// <summary>
/// Iterate using IJobEntity for parallel jobs
/// See https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/iterating-data-ijobentity.html
/// </summary>
[BurstCompile]
public partial struct UfoMovementSystem_IJobEntity : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Create the job
        var job = new UfoMovementJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            random = new Random((uint)SystemAPI.Time.ElapsedTime + 1),
        };

        job.ScheduleParallel();
    }
}

[BurstCompile]
partial struct UfoMovementJob : IJobEntity
{
    public float deltaTime;
    public Random random;

    void Execute(ref LocalTransform transform, in MovementData movement)
    {
        transform = transform.Translate(transform.Forward() * movement.Speed * deltaTime);

        if (random.NextFloat() > 0.98f)
        {
            transform = transform.RotateY(random.NextInt(0, 360));
        }
    }
}
#endif
