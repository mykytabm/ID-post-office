
using System;

[Serializable]
public class BuildingData
{
    public string BuildingName;
    public EBuildingType BuildingType;
    public ulong InitialCost;
    public float GrowthRate;
    public int Level;
    public ulong MailGenerated;
    public ulong TotalProduction;
    public float Time;
    public bool SpawnEffects;
    public float currentTime;

    public BuildingData(string BuildingName, EBuildingType BuildingType, ulong InitialCost, float GrowthRate, int Level, ulong mailGenerated, ulong TotalProduction, float Time,
    bool SpawnEffects)
    {
        this.BuildingName = BuildingName;
        this.BuildingType = BuildingType;
        this.InitialCost = InitialCost;
        this.GrowthRate = GrowthRate;
        this.MailGenerated = mailGenerated;
        this.Level = Level;
        this.TotalProduction = TotalProduction;
        this.Time = Time;
        this.SpawnEffects = SpawnEffects;
        currentTime = 0;
    }

    public BuildingData(BuildingData data)
    {
        this.BuildingName = data.BuildingName;
        this.BuildingType = data.BuildingType;
        this.InitialCost = data.InitialCost;
        this.GrowthRate = data.GrowthRate;
        this.MailGenerated = data.MailGenerated;
        this.Level = data.Level;
        this.TotalProduction = data.TotalProduction;
        this.Time = data.Time;
        this.SpawnEffects = data.SpawnEffects;
        currentTime = 0;
    }
}
