using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject MessageScreenRoot;
    public TextMeshProUGUI goldText;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void ToggleMessageScreen()
    {
        if (MessageScreenRoot)
        {
            MessageScreenRoot.SetActive(!MessageScreenRoot.activeSelf);
        }
    }

    public void UpdateGold()
    {
        goldText.text = Utils.GoldToString(_gameManager.Gold);
    }
}
