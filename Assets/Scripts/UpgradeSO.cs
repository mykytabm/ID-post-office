using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : ScriptableObject
{
    public Sprite icon;
    public EUpgradeType UpgradeType;
    public string Name;
    public string Description;
    public ulong Cost;
}

public enum EUpgradeType
{
    LetterGloves,
    ExpensiveStamps,
    SolarSails,
    DoubleTrouble,
    LostLettersLocator,
    SpaceGloves,
    UpgradedSatellite
}