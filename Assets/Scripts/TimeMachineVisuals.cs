using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimeMachineVisuals : MonoBehaviour
{
    private float _t;
    public Transform visualRoot;

    private Transform _buildingsRoot;
    private Building _building;
    private bool _isTeleporting;

    private void Awake()
    {
        _buildingsRoot = GameObject.FindGameObjectWithTag("BuildingsRoot").transform;
        _building = GetComponent<Building>();

        RandomizeTime();
    }

    private void FixedUpdate()
    {
        if (!_isTeleporting)
        {
            _t -= Time.fixedDeltaTime;
            if (_t <= 0)
            {
                Teleport();
            }
        }
    }

    private void RandomizeTime()
    {
        _t = Random.Range(5f, 30f);
    }

    private void Teleport()
    {
        if (visualRoot)
        {
            _isTeleporting = true;
            _building.enabled = false;
            visualRoot.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
            {
                transform.position = (Vector2)_buildingsRoot.position + new Vector2(Random.Range(-3f, 3f), Random.Range(-4f, 4f));

                visualRoot.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    _building.enabled = true;
                    RandomizeTime();
                    _isTeleporting = false;
                });
            });
        }
    }
}
