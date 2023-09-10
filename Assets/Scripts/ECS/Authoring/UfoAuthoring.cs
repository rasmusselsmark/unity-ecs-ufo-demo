using Unity.Entities;
using UnityEngine;

public class UfoAuthoring : MonoBehaviour
{
}

class UfoBaker : Baker<UfoAuthoring>
{
    public override void Bake(UfoAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<MovementData>(entity);
    }
}
