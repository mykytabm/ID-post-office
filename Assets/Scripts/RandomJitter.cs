using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomJitter : MonoBehaviour
{
    public float speed;
    public float timer = 0.1f;

    private float _t = 0;
    private int _dir = 0;

    void FixedUpdate()
    {
        _t -= Time.fixedDeltaTime;
        if (_t <= 0)
        {
            _dir = Random.Range(0, 2);
            _t = timer;
        }

        transform.position = new Vector2(transform.position.x + speed * _dir * Time.fixedDeltaTime, transform.position.y);
    }
}
