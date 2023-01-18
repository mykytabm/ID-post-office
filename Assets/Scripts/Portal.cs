using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _portalEffect;
    [SerializeField] private GameObject _mailPrefab;

    private bool _isClicked = false;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (_isClicked)
        {

        }
    }

    private void OnMouseDown()
    {
        SendMail();
    }

    private void SendMail()
    {
        var startPos = Random.insideUnitCircle.normalized * 3f;
        var targetPos = (Vector2)transform.position + Random.insideUnitCircle;
        var mail = Instantiate(_mailPrefab, startPos + (Vector2)transform.position, Quaternion.identity);

        mail.transform.DOMove(targetPos, Random.Range(0.6f, 0.8f))
        .SetEase(Ease.InQuad)
        .OnComplete(() =>
        {
            mail.transform.DOScale(Vector3.zero, 0.4f).OnComplete(() =>
            {
                Destroy(mail);
            });

            StartCoroutine(Utils.Wait(0.1f, () =>
            {
                GameManager.Instance.ManualGoldReceive(mail.transform.position);
                BounceEffect();
            }));
        });
    }

    private void BounceEffect()
    {
        if (_portalEffect)
        {
            _portalEffect.DOKill();
            var clickSequence = DOTween.Sequence();
            clickSequence.Append(_portalEffect.DOScale(Vector3.one * 0.95f, 0.1f).SetEase(Ease.InQuad));
            clickSequence.Append(_portalEffect.DOScale(Vector3.one, 0.2f));
        }
    }
}
