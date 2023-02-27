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

        SpawnText(new TextSettings(position, "+" + _goldPerClick.ToString()));
    }

    public void GoldReceive(GoldReceiveSettings settings)
    {
        ReceiveGold(settings.gold);
        if (settings.spawnEffects)
        {
            SpawnText(new TextSettings(settings.pos, "+" + settings.gold.ToString(), settings.randomOffset, settings.offset));
        }
    }

    public void SetGoldPerClick(ulong newGoldPerClick)
    {
        _goldPerClick = newGoldPerClick;
    }

    private void ReceiveGold(ulong gold)
    {
        Gold += gold;
        _uiManager.UpdateGold();
        BuildingManager.Instance.CheckUpgradeAvailability(Gold);
        UpgradeManager.Instance.CheckUpgradeAvailability(Gold);
    }

    public void SpendGold(ulong gold)
    {
        Gold -= gold;
        _uiManager.UpdateGold();
        BuildingManager.Instance.CheckUpgradeAvailability(Gold);
        UpgradeManager.Instance.CheckUpgradeAvailability(Gold);

    }

    public void SpawnText(TextSettings settings)
    {
        _textSpawner.SpawnText(settings);
    }
}

public record GoldReceiveSettings(EBuildingType mineType, Vector2 pos, ulong gold, bool spawnEffects, bool randomOffset = false, float offset = 0);

public enum EBuildingType
{
    StampMachine,
    Satelite,
    TimeMachine,
    Plant,
    Gun,
    MiniPortal,
    WinGame
}