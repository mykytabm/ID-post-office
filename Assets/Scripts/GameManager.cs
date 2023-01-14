using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Gold { get; private set; }
    public int DarkEssence { get; private set; }

    private int _goldPerClick = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ManualGoldReceive()
    {
        Gold += _goldPerClick;
    }
}
