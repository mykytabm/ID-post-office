using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class MessageScreen : Singleton<MessageScreen>
{
    [SerializeField] TextMeshProUGUI text;

    public MessageEntrySO Entries;

    void Start()
    {
        AddEntry("Greeting");
    }

    void Update()
    {

    }
    public void AddEntry(string key)
    {
        var entryToAdd = Entries.Entries.Where(x => x.Key == key).FirstOrDefault();
        entryToAdd.Entry += Environment.NewLine;
        text.text += entryToAdd.Entry;
    }
}
