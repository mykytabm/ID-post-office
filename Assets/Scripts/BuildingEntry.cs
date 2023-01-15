using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildingEntry : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Cost;
    public GameObject BuyButtonRoot;

    private List<GameObject> _modeOne = new List<GameObject>();
    private List<GameObject> _modeTwo = new List<GameObject>();

    private ulong _currentPrice;
    private EBuildingType buildingType;

    private bool _isMouseDown;

    private void Awake()
    {
        _modeOne.Add(Title.gameObject);
        _modeOne.Add(Level.gameObject);

        if (Description)
        {
            _modeTwo.Add(Description.gameObject);
        }
        _modeTwo.Add(BuyButtonRoot);

    }
    public void SetEntryData(Sprite icon, BuildingData buildingData, string description = "")
    {
        buildingType = buildingData.BuildingType;
        image.sprite = icon;
        Title.text = buildingData.BuildingName;
        if (description != string.Empty)
        {
            Description.text = description;
        }
        Level.text = buildingData.Level.ToString();

        if (buildingData.Level > 1)
        {
            _currentPrice = (ulong)(buildingData.InitialCost * Math.Pow(buildingData.GrowthRate, (buildingData.Level + 1)));
        }
        else
        {
            _currentPrice = buildingData.InitialCost;
        }

        Cost.text = _currentPrice.ToString();
    }

    public void UpdateData(BuildingData data)
    {
        SetEntryData(image.sprite, data);
    }

    public void BuyLevel()
    {
        if (GameManager.Instance.Gold < _currentPrice)
        {
            return;
        }
        BuildingManager.Instance.BuyBuilding(buildingType);
    }

    private void SetActiveGOsInList(List<GameObject> list, bool newEnabled)
    {
        foreach (var go in list)
        {
            go.SetActive(newEnabled);
        }
    }
}
