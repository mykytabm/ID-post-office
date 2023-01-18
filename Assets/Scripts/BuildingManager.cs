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

    public List<Building> spawnedBuildingsVisuals = new();

    public List<BuildingData> spawnedBuildingsData = new();
    public Transform buildingsRoot;

    private void Start()
    {
        CreateBuildingEntries();
    }

    private void FixedUpdate()
    {
        foreach (var building in spawnedBuildingsData)
        {
            building.currentTime -= Time.fixedDeltaTime;
            if (building.currentTime <= 0)
            {
                GameManager.Instance.GoldReceive(new GoldReceiveSettings(building.BuildingType, Vector2.zero, building.TotalProduction, false));
                building.currentTime = building.Time;
            }
        }
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

        var runtimeBuildingData = currentBuildings[type];

        runtimeBuildingData.Level++;
        runtimeBuildingData.TotalProduction = runtimeBuildingData.MailGenerated * (ulong)runtimeBuildingData.Level;

        currentBuildings[type] = runtimeBuildingData;

        if (spawnedBuildingsData.Contains(runtimeBuildingData))
        {
            var t = spawnedBuildingsData.Where(x => x.BuildingType == type).FirstOrDefault();
            spawnedBuildingsData.Remove(t);
        }
        spawnedBuildingsData.Add(runtimeBuildingData);

        UpdateBuilding(null, runtimeBuildingData);

        var building = BuildBuilding(type);

        spawnedBuildingsVisuals.Add(building);
    }

    private Building BuildBuilding(EBuildingType type)
    {
        var prefab = buildingsData.Where(x => x.data.BuildingType == type).FirstOrDefault().BuildingPrefab;
        Building building = null;
        switch (type)
        {
            case EBuildingType.TimeMachine:
                Vector2 position = buildingsRoot.transform.position;
                var randomOffset = new Vector2(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-4, 4));
                var buildingInst = Instantiate(prefab);
                buildingInst.transform.position = position + randomOffset;
                building = buildingInst.GetComponent<Building>();
                break;
        }

        return building;
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
