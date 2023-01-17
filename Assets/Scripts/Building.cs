using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform visualRoot;

    private BuildingData _data;
    private float _t;
    private float _targetT;
    private ulong _generationRate;

    void Awake()
    {
        UpdateSettings();
    }

    void FixedUpdate()
    {
        _t -= Time.fixedDeltaTime;
        if (_t <= 0)
        {
            GameManager.Instance.GoldReceive(new GoldReceiveSettings(_data.BuildingType, transform.position, _generationRate, _data.SpawnEffects));
            BounceEffect();
            _t = _targetT;
        }
    }

    private void BounceEffect()
    {
        if (visualRoot)
        {
            visualRoot.DOKill();
            var clickSequence = DOTween.Sequence();
            clickSequence.Append(visualRoot.DOScale(Vector3.one * 0.85f, 0.1f).SetEase(Ease.InQuad));
            clickSequence.Append(visualRoot.DOScale(Vector3.one, 0.2f));
        }
    }

    public void UpdateSettings()
    {
        _data = BuildingManager.Instance.GetBuildingData(_data.BuildingType);

        _targetT = _data.Time;
        _t = _targetT;
        _generationRate = _data.MailGenerated;
    }
}
