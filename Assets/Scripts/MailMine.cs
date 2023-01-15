using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MailMine : MonoBehaviour
{
    public Transform visualRoot;
    public EBuildingType mineType;

    float _t;
    float _targetT;
    private ulong _gold;
    MineSettings _settings;

    void Awake()
    {
        UpdateSettings();
    }

    void FixedUpdate()
    {
        _t -= Time.fixedDeltaTime;
        if (_t <= 0)
        {
            GameManager.Instance.GoldReceive(new GoldReceiveSettings(mineType, transform.position, _gold, _settings.SpawnEffects));
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
        _settings = GameManager.Instance.GetMineSettings(mineType);

        _targetT = _settings.time;
        _t = _targetT;
        _gold = _settings.Gold;
    }
}
