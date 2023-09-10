using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct UfoSpawnerSystem : ISystem, ISystemStartStop
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Spawner>();
    }

    public void OnStartRunning(ref SystemState state)
    {
        foreach (var spawner in SystemAPI.Query<RefRO<Spawner>>())
        {
            var random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks);

            Debug.Log($"Spawning {spawner.ValueRO.Count} ufos!");
            for (var i = 0; i < spawner.ValueRO.Count; i++)
            {
                SpawnUfo(ref state, spawner, ref random);
            }
        }
    }

    public void OnStopRunning(ref SystemState state)
    {
    }

    private void SpawnUfo(ref SystemState state, RefRO<Spawner> spawner, ref Unity.Mathematics.Random random)
    {
        var ufo = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
        var position = random.NextFloat3(
                new float3(-spawner.ValueRO.RangeX, 1, -spawner.ValueRO.RangeZ),
                new float3(spawner.ValueRO.RangeX, spawner.ValueRO.RangeY, spawner.ValueRO.RangeZ));

        var transform = LocalTransform.FromPositionRotation(
            position,
            Quaternion.Euler(0, random.NextFloat(0, 360), 0));

        state.EntityManager.SetName(ufo, "Ufo");
        SystemAPI.SetComponent(ufo, transform);
        SystemAPI.SetComponent(ufo, new MovementData
        {
            Speed = random.NextFloat(1f, spawner.ValueRO.MaxSpeed),
        });
    }
}