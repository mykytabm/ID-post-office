using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform visualRoot;
    private BuildingData _data;

    private Vector2 _mouseOffset;

    public bool IsHeld { get; private set; }

    public Action SpawnAction;

    private void Awake()
    {
        visualRoot.localScale = Vector3.zero;
    }

    private void Start()
    {
        ScaleUpEffect();
    }

    private void OnDisable()
    {
        IsHeld = false;
    }
    private void OnMouseDown()
    {
        IsHeld = true;
        _mouseOffset = (Vector2)transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        if (IsHeld)
        {
            transform.position = GetMousePos() + _mouseOffset;
        }
    }

    private void OnMouseUp()
    {
        IsHeld = false;
    }

    private Vector2 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void ScaleUpEffect()
    {
        if (visualRoot)
        {
            var clickSequence = DOTween.Sequence();
            clickSequence.Append(visualRoot.DOScale(Vector3.one * 1.2f, 0.4f).SetEase(Ease.InQuad));
            clickSequence.Append(visualRoot.DOScale(Vector3.one, 0.2f));
            clickSequence.OnComplete(() =>
            {
                SpawnAction?.Invoke();
            });
        }
    }
}
