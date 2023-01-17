using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    public List<BuildingSO> buildingsData;
    public GameObject BuildingEntryPrefab;
    public Transform buildingEntriesParent;

    public Dictionary<EBuildingType, BuildingData> currentBuildings = new();
    public Dictionary<EBuildingType, BuildingEntry> buildingUIEntries = new();

    public List<Building> buildings = new();

    void Start()
    {
        CreateBuildingEntries();
    }

    private void CreateBuildingEntries()
    {
        foreach (var buildingDataSO in buildingsData)
        {
            var buildingEntryInst = Instantiate(BuildingEntryPrefab, buildingEntriesParent);

            var buildingEntry = buildingEntryInst.GetComponent<BuildingEntry>();

            buildingUIEntries.Add(buildingDataSO.data.BuildingType, buildingEntry);

            var buildingData = new BuildingData(buildingDataSO.data);

            currentBuildings.Add(buildingDataSO.data.BuildingType, buildingData);

            buildingEntry.SetEntryData(buildingDataSO.icon, buildingData, buildingDataSO.Description);
        }

        CheckUpgradeAvailability(GameManager.Instance.Gold);
    }

    public void BuyBuilding(EBuildingType type)
    {
        var buildingData = currentBuildings[type];
        var buildingPrice = (ulong)(buildingData.InitialCost * Math.Pow(buildingData.GrowthRate, (buildingData.Level + 1)));

        if (GameManager.Instance.Gold < buildingPrice)
        {
            return;
        }

        GameManager.Instance.SpendGold(buildingPrice);

        var building = currentBuildings[type];

        building.Level++;
        building.TotalProduction = building.MailGenerated * (ulong)building.Level;

        currentBuildings[type] = building;

        UpdateBuilding(null, building);

        // TODO: Spawn building
    }

    public void BuildBuilding(EBuildingType type)
    {
        switch (type)
        {
            case EBuildingType.TimeMachine:

                break;
        }
    }
    public BuildingData GetBuildingData(EBuildingType type)
    {
        return currentBuildings[type];
    }

    public void UpdateBuilding(Building building, BuildingData data)
    {
        buildingUIEntries[data.BuildingType].UpdateData(data);


    }

    public void CheckUpgradeAvailability(ulong currentGold)
    {
        foreach (var uiEntry in buildingUIEntries)
        {
            uiEntry.Value.SetBuyButtonActive(currentGold);
        }
    }
}
