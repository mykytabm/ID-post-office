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

    private void Awake()
    {
        _sprRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _angle += speed * Time.fixedDeltaTime;
        var posX = Mathf.Sin(_angle) * radius * ellipseFactor;
        var posY = Mathf.Cos(_angle) * radius;
        var t = new Vector2(posX, posY);

        transform.position = (Vector2)target.position + t;

        transform.right = -t.normalized;
    }
}
