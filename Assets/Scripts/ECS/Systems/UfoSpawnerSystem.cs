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
        state.RequireForUpdate<UfoSpawnerData>();
    }

    public void OnStartRunning(ref SystemState state)
    {
        var spawner = SystemAPI.GetSingleton<UfoSpawnerData>();
        var random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks);

        Debug.Log($"Spawning {spawner.Count} ufos!");
        for (var i = 0; i < spawner.Count; i++)
        {
            SpawnUfo(ref state, spawner, ref random);
        }
    }

    public void OnStopRunning(ref SystemState state)
    {
    }

    private void SpawnUfo(ref SystemState state, UfoSpawnerData ufoSpawnerData, ref Unity.Mathematics.Random random)
    {
        var ufo = state.EntityManager.Instantiate(ufoSpawnerData.Prefab);
        var position = random.NextFloat3(
                new float3(-ufoSpawnerData.RangeX, 1, -ufoSpawnerData.RangeZ),
                new float3(ufoSpawnerData.RangeX, ufoSpawnerData.RangeY, ufoSpawnerData.RangeZ));

        var transform = LocalTransform.FromPositionRotation(
            position,
            Quaternion.Euler(0, random.NextFloat(0, 360), 0));

        state.EntityManager.SetName(ufo, "Ufo");
        SystemAPI.SetComponent(ufo, transform);
        SystemAPI.SetComponent(ufo, new MovementData
        {
            Speed = random.NextFloat(1f, ufoSpawnerData.MaxSpeed),
        });
    }
}
