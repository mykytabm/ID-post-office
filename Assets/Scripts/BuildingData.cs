
using System;

[Serializable]
public struct BuildingData
{
    public string BuildingName;
    public EBuildingType BuildingType;
    public ulong InitialCost;
    public float GrowthRate;
    public int Level;
    public ulong MailGenerated;
    public ulong TotalProduction;

    public BuildingData(string BuildingName, EBuildingType BuildingType, ulong InitialCost, float GrowthRate, int Level, ulong mailGenerated, ulong TotalProduction)
    {
        this.BuildingName = BuildingName;
        this.BuildingType = BuildingType;
        this.InitialCost = InitialCost;
        this.GrowthRate = GrowthRate;
        this.MailGenerated = mailGenerated;
        this.Level = Level;
        this.TotalProduction = TotalProduction;
    }
}
