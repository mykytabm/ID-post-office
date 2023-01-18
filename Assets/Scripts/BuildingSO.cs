using UnityEngine;

[CreateAssetMenu]
public class BuildingSO : ScriptableObject
{
    public Sprite icon;
    public string Description;
    public BuildingData data;
    public GameObject BuildingPrefab;
}