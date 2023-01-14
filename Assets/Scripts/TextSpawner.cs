using DG.Tweening;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _textPrefab;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SpawnText(TextSettings settings)
    {
        var targetPos = settings.randomOffset ? settings.pos + Random.insideUnitCircle : settings.pos;
        var textInst = Instantiate(_textPrefab, targetPos, Quaternion.identity);
        var text = textInst.GetComponent<TMPro.TextMeshPro>();
        text.text = settings.text;
        textInst.transform.DOMoveY(targetPos.y + 3, 1.55f).SetEase(Ease.Linear);
        text.DOFade(0, 1f).SetEase(Ease.Linear).SetDelay(0.6f).OnComplete(() =>
        {
            Destroy(textInst);
        });
    }

}

public record TextSettings(Vector2 pos, string text, bool randomOffset = true);