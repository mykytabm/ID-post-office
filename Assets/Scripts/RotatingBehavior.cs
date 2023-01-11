using UnityEngine;

public class RotatingBehavior : MonoBehaviour
{
    public float rotationFactor;
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationFactor);
    }
}
