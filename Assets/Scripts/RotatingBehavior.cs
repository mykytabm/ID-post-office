using UnityEngine;

public class RotatingBehavior : MonoBehaviour
{
    public float rotationFactor;
    public bool randomizeStartRotation;
    public bool randomizeRotationDirection;

    private bool _rightDir;

    void Awake()
    {
        if (randomizeStartRotation)
        {
            var rot = Random.Range(-180, 180);
            transform.Rotate(Vector3.forward * rot);
        }
        if (randomizeRotationDirection)
        {
            _rightDir = Random.Range(0, 2) == 0;
        }
    }
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate((_rightDir ? 1 : -1) * Vector3.forward * rotationFactor);
    }
}
