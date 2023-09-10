#if USE_JOBS
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

[RequireMatchingQueriesForUpdate]
public partial class UfoMovementSystem_IJobEntity : SystemBase
{
    private EntityQuery query;

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
    
    protected override void OnCreate()
    {
        // Query for entities with MovementData component
        query = GetEntityQuery
        (
            typeof(LocalTransform),
            typeof(MovementData)
        );
    }

    protected override void OnUpdate()
    {
        // Create the job
        var job = new UfoMovementJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            random = new Random((uint)SystemAPI.Time.ElapsedTime + 1),
        };

        // Schedule the job using Dependency property
        Dependency = job.ScheduleParallel(query, Dependency);
    }
}
#endif
