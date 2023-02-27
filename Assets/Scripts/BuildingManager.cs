using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Transform factoriesRoot;
    public Transform portalRoot;

    private int _factoriesCount;

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

        var building = BuildBuilding(runtimeBuildingData);
        if (building != null)
        {
            spawnedBuildingsVisuals.Add(building);
        }
    }

    private Building BuildBuilding(BuildingData data)
    {
        var prefab = buildingsData.Where(x => x.data.BuildingType == data.BuildingType).FirstOrDefault().BuildingPrefab;
        Building building = null;
        GameObject buildingInst;
        switch (data.BuildingType)
        {
            case EBuildingType.WinGame:
                SceneManager.LoadScene("WinScene");
                break;
            case EBuildingType.TimeMachine:
                if (data.Level < 30 && data.Level % 2 == 0)
                {
                    Vector2 pos = buildingsRoot.transform.position;
                    var randomOffset = new Vector2(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-4, 4));
                    buildingInst = Instantiate(prefab);
                    buildingInst.transform.position = pos + randomOffset;
                    building = buildingInst.GetComponent<Building>();
                }
                break;
            case EBuildingType.Satelite:
                if (data.Level < 30 && (data.Level == 1 || data.Level % 2 == 0))
                {
                    buildingInst = Instantiate(prefab);
                    building = buildingInst.GetComponent<Building>();
                    buildingInst.GetComponent<MoveInCircle>().target = portalRoot;
                }
                break;
            case EBuildingType.Plant:
                if (data.Level < 30)
                {
                    buildingInst = Instantiate(prefab);
                    Vector2 position = (Vector2)factoriesRoot.transform.position + new Vector2(_factoriesCount * 0.6f, 0);
                    building = buildingInst.GetComponent<Building>();
                    buildingInst.transform.position = position;
                }
                _factoriesCount++;
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
