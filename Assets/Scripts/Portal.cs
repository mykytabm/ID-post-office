using UnityEngine;
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
        var pos = Random.insideUnitCircle.normalized * 3.5f;
        var mail = Instantiate(_mailPrefab, pos, Quaternion.identity);

        var mailSequence = DOTween.Sequence();
        mailSequence.Append(mail.transform.DOMove(transform.position, Random.Range(0.6f, 0.8f)).SetEase(Ease.InQuad));
        mailSequence.Append(mail.transform.DOScale(Vector3.zero, 0.4f));
        mailSequence.OnComplete(() =>
        {
            Destroy(mail);
            GameManager.Instance.ManualGoldReceive();
            BounceEffect();
        });
    }

    private void BounceEffect()
    {
        if (_portalEffect)
        {
            _portalEffect.DOKill();
            var clickSequence = DOTween.Sequence();
            clickSequence.Append(_portalEffect.DOScale(Vector3.one * 1.15f, 0.1f).SetEase(Ease.InQuad));
            clickSequence.Append(_portalEffect.DOScale(Vector3.one, 0.2f));
        }
    }
}
