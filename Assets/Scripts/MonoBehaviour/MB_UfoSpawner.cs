using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    public GameObject UfoPrefab;
    public int Count;
    public float RangeX = 20f;
    public float RangeY = 10f;
    public float RangeZ = 20f;
    public float MaxSpeed = 3f;
    
    void Start()
    {
        for (var i = 0; i < Count; i++)
        {
            var ufo = Instantiate(UfoPrefab,
                new Vector3(Random.Range(-RangeX, RangeX), Random.Range(1, RangeY), Random.Range(-RangeZ, RangeZ)),
                Quaternion.Euler(0, Random.Range(0, 360), 0)
            ).GetComponent<MB_UfoMovement>();
            ufo.Speed = Random.Range(1f, MaxSpeed);
        }
    }
}
