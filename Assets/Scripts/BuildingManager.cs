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

            var buildingData = new BuildingData(
                    buildingDataSO.data.BuildingName,
                    buildingDataSO.data.BuildingType,
                    buildingDataSO.data.InitialCost,
                    buildingDataSO.data.GrowthRate,
                    buildingDataSO.data.Level,
                    buildingDataSO.data.MailGenerated,
                    0);

            currentBuildings.Add(buildingDataSO.data.BuildingType, buildingData);

            buildingEntry.SetEntryData(buildingDataSO.icon, buildingData, buildingDataSO.Description);
        }
    }

    public void BuyBuilding(EBuildingType type)
    {
        var building = currentBuildings[type];

        building.Level++;
        building.TotalProduction = building.MailGenerated * (ulong)building.Level;
        UpdateBuilding(building);
    }

    public void UpdateBuilding(BuildingData data)
    {
        buildingUIEntries[data.BuildingType].UpdateData(data);
    }
}
