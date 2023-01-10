using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject MessageScreenRoot;

    public void ToggleMessageScreen()
    {
        if (MessageScreenRoot)
        {
            MessageScreenRoot.SetActive(!MessageScreenRoot.activeSelf);
        }
    }
}
