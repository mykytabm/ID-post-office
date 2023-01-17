using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ulong Gold { get; private set; }
    public ulong DarkEssence { get; private set; }

    [SerializeField] bool _cheat = false;
    [SerializeField] ulong _cheatGold = 10000;

    private ulong _goldPerClick = 1;

    private TextSpawner _textSpawner;

    private UIManager _uiManager;
    void Start()
    {
        _textSpawner = GetComponent<TextSpawner>();
        _uiManager = UIManager.Instance;
    }

    public void ManualGoldReceive(Vector2 position)
    {
        if (_cheat)
        {
            ReceiveGold(_cheatGold);
        }
        else
        {
            ReceiveGold(_goldPerClick);
        }

        SpawnText(position, "+" + _goldPerClick.ToString());
    }

    public void GoldReceive(GoldReceiveSettings settings)
    {
        ReceiveGold(settings.gold);
        SpawnText(settings.pos, "+" + settings.gold.ToString());
    }

    private void ReceiveGold(ulong gold)
    {
        Gold += gold;
        _uiManager.UpdateGold();
        BuildingManager.Instance.CheckUpgradeAvailability(Gold);
    }

    public void SpendGold(ulong gold)
    {
        Gold -= gold;
        _uiManager.UpdateGold();
        BuildingManager.Instance.CheckUpgradeAvailability(Gold);
    }

    private void SpawnText(Vector2 pos, string text)
    {
        _textSpawner.SpawnText(new TextSettings(pos, text));
    }
}

public record GoldReceiveSettings(EBuildingType mineType, Vector2 pos, ulong gold, bool spawnEffects);

public enum EBuildingType
{
    Satelite,
    TimeMachine,
    Plant,
    Gun,
    MiniPortal
}