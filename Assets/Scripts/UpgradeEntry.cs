using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEntry : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;
    public Button BuyButton;
    EUpgradeType upgradeType;

    private ulong _currentPrice;
    public void SetEntryData(UpgradeSO data)
    {
        image.sprite = data.icon;
        Title.text = data.Name;
        Cost.text = data.Cost.ToString();
        upgradeType = data.UpgradeType;

        _currentPrice = data.Cost;
    }

    public void BuyUpgrade()
    {
        UpgradeManager.Instance.BuyUpgrade(upgradeType, this);
    }

    public void SetBuyButtonActive(ulong gold)
    {
        if (upgradeType == EUpgradeType.SolarSails)
        {
            var satellite = BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.Satelite).FirstOrDefault();
            if (satellite == null)
            {
                BuyButton.interactable = false;
                return;
            }
        }
        else if (upgradeType == EUpgradeType.UpgradedSatellite)
        {
            var satellite = BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.Satelite).FirstOrDefault();
            if (satellite == null)
            {
                BuyButton.interactable = false;
                return;
            }
        }
        else if (upgradeType == EUpgradeType.DoubleTrouble)
        {
            var timeMachine = BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.TimeMachine).FirstOrDefault();
            if (timeMachine == null)
            {
                BuyButton.interactable = false;
                return;
            }
        }
        else if (upgradeType == EUpgradeType.ExpensiveStamps)
        {
            var stampMachine = BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.StampMachine).FirstOrDefault();
            if (stampMachine == null)
            {
                BuyButton.interactable = false;
                return;
            }
        }
        else if (upgradeType == EUpgradeType.LostLettersLocator)
        {
            var miniPortal = BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.MiniPortal).FirstOrDefault();
            if (miniPortal == null)
            {
                BuyButton.interactable = false;
                return;
            }
        }



        if (_currentPrice <= gold)
        {
            BuyButton.interactable = true;
        }
        else
        {
            if (BuyButton.interactable)
            {
                BuyButton.interactable = false;
            }
        }
    }
}
