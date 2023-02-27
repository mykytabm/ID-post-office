using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{
    public Transform target;
    public float ellipseFactor;
    public float speed = 2f;
    public float radius = 1.3f;

    private float _angle;
    private SpriteRenderer _sprRenderer;
    private float _direction;

    private void Awake()
    {
        speed = Random.Range(0.10f, 0.5f);
        radius = Random.Range(2.9f, 3.5f);
        _direction = Random.Range(0, 2);
        _sprRenderer = GetComponentInChildren<SpriteRenderer>();
        Utils.Wait(0.6f, () =>
        {
            GetComponent<TrailRenderer>().emitting = true;
        });
    }

    private void FixedUpdate()
    {
        _angle += (_direction == 0 ? 1 : -1) * speed * Time.fixedDeltaTime;
        var posX = Mathf.Sin(_angle) * radius * ellipseFactor;
        var posY = Mathf.Cos(_angle) * radius;
        var t = new Vector2(posX, posY);

        transform.position = (Vector2)target.position + t;

        transform.right = -t.normalized;
    }
}
