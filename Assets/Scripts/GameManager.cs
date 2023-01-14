using System.Collections;
using System.Collections.Generic;
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
            Gold += _cheatGold;
        }
        else
        {
            Gold += _goldPerClick;
        }

        SpawnText(position, "+" + _goldPerClick.ToString());
        _uiManager.UpdateGold();
    }

    private void SpawnText(Vector2 pos, string text)
    {
        _textSpawner.SpawnText(new TextSettings(pos, text));
    }
}
