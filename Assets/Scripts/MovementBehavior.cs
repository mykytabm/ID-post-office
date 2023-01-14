using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    public float moveFactor;

    private void FixedUpdate()
    {
        transform.position += Vector3.right * moveFactor * Time.fixedDeltaTime;
    }
}
