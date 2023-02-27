using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public List<UpgradeSO> upgradeSOs;

    private Dictionary<EUpgradeType, bool> _boughtUpgrades;

    public Transform EntriesParent;
    public GameObject UpgradeEntry;

    public List<UpgradeEntry> upgradeEntries;
    private void Awake()
    {
        _boughtUpgrades = new Dictionary<EUpgradeType, bool>();

        foreach (var upgrade in upgradeSOs)
        {
            _boughtUpgrades.Add(upgrade.UpgradeType, false);
        }

        CreateUpgradeEntries();
    }

    public void BuyUpgrade(EUpgradeType upgradeType, UpgradeEntry entry)
    {
        _boughtUpgrades[upgradeType] = true;

        if (upgradeType == EUpgradeType.LetterGloves)
        {
            GameManager.Instance.SetGoldPerClick(2);
        }
        else if (upgradeType == EUpgradeType.SpaceGloves)
        {
            GameManager.Instance.SetGoldPerClick(5);
        }
        else if (upgradeType == EUpgradeType.ExpensiveStamps)
        {
            GameManager.Instance.SetGoldPerClick(10);
        }
        else if (upgradeType == EUpgradeType.SolarSails)
        {
            BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.Satelite).FirstOrDefault().MailGenerated += 100;
        }
        else if (upgradeType == EUpgradeType.UpgradedSatellite)
        {
            BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.Satelite).FirstOrDefault().MailGenerated *= 2;
        }
        else if (upgradeType == EUpgradeType.ExpensiveStamps)
        {
            BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.StampMachine).FirstOrDefault().MailGenerated *= 2;
        }
        else if (upgradeType == EUpgradeType.LostLettersLocator)
        {
            BuildingManager.Instance.spawnedBuildingsData.Where(x => x.BuildingType == EBuildingType.MiniPortal).FirstOrDefault().MailGenerated *= 2;
        }

        if (upgradeEntries.Contains(entry))
        {
            upgradeEntries.Remove(entry);
        }

        Destroy(entry.gameObject);
    }

    private void CreateUpgradeEntries()
    {
        foreach (var upgradeSO in upgradeSOs)
        {
            var upgradeInst = Instantiate(UpgradeEntry, EntriesParent);

            var upgradeEntry = upgradeInst.GetComponent<UpgradeEntry>();

            upgradeEntries.Add(upgradeEntry);

            upgradeEntry.SetEntryData(upgradeSO);
        }

        CheckUpgradeAvailability(GameManager.Instance.Gold);
    }

    public void CheckUpgradeAvailability(ulong currentGold)
    {
        foreach (var uiEntry in upgradeEntries)
        {
            uiEntry.SetBuyButtonActive(currentGold);
        }
    }
}
