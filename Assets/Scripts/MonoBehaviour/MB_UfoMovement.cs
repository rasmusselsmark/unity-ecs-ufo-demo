using UnityEngine;

public class MB_UfoMovement : MonoBehaviour
{
    public float Speed;
    
    void Update()
    {
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));

        if (Random.value > 0.98f)
        {
            transform.Rotate(0, Random.Range(0, 360), 0);
        }
    }
}
