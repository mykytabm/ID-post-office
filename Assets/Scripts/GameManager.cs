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
    public List<MineSettings> mineTimes;
    void Start()
    {
        _textSpawner = GetComponent<TextSpawner>();
        _uiManager = UIManager.Instance;
    }

    public void ManualGoldReceive(Vector2 position)
    {
        if (_cheat)
        {
            Gold += _cheatGold;
        }
        else
        {
            Gold += _goldPerClick;
        }

        SpawnText(position, "+" + _goldPerClick.ToString());
        _uiManager.UpdateGold();
    }

    public void GoldReceive(GoldReceiveSettings settings)
    {
        Gold += settings.gold;
        SpawnText(settings.pos, "+" + settings.gold.ToString());
        _uiManager.UpdateGold();
    }

    private void SpawnText(Vector2 pos, string text)
    {
        _textSpawner.SpawnText(new TextSettings(pos, text));
    }

    public MineSettings GetMineSettings(EBuildingType mineType)
    {
        return mineTimes.Where(x => x.Mine == mineType).SingleOrDefault();
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

[Serializable]
public struct MineSettings
{
    public MineSettings(EBuildingType mine, float time, ulong gold, bool spawnEffects)
    {
        Mine = mine;
        this.time = time;
        Gold = gold;
        SpawnEffects = spawnEffects;
    }

    [field: SerializeField]
    public EBuildingType Mine { get; set; }
    [field: SerializeField]
    public float time { get; set; }
    [field: SerializeField]
    public ulong Gold { get; set; }
    [field: SerializeField]
    public bool SpawnEffects { get; set; }
}