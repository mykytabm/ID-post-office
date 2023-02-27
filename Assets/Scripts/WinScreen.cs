using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI clicksText;

    private ulong _clicks;
    void Start()
    {

    }

    void Update()
    {

    }

    public void IncrementClicks()
    {
        _clicks++;
        clicksText.text = Utils.GoldToString(_clicks);
    }
}
