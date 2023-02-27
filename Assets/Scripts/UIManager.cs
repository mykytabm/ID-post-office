using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject UpgradesScreen;
    public GameObject GarageScreen;

    public TextMeshProUGUI goldText;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OpenUpgradeScreen()
    {
        if (UpgradesScreen)
        {
            GarageScreen.SetActive(false);
            UpgradesScreen.SetActive(true);
        }
    }

    public void OpenGarageScreen()
    {
        if (GarageScreen)
        {
            GarageScreen.SetActive(true);
            UpgradesScreen.SetActive(false);
        }
    }

    public void UpdateGold()
    {
        goldText.text = Utils.GoldToString(_gameManager.Gold);
    }
}
