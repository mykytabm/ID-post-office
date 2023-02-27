using UnityEngine;
using DG.Tweening;

public class ScalingBehavior : MonoBehaviour
{
    public float yScale;
    public float xScale;
    public float time;
    public Transform visualsRoot;

    private Sequence _sequence;
    void Start()
    {
        GetComponent<Building>().SpawnAction += () =>
        {
            Scale();
        };
    }



    private void Scale()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(visualsRoot.DOScale(new Vector3(xScale, yScale, 0), time / 2).SetEase(Ease.InOutQuad));
        _sequence.Append(visualsRoot.DOScale(new Vector3(1, 0.9f, 0), time / 2).SetEase(Ease.InOutQuad));
        _sequence.OnComplete(Scale);
    }
}
