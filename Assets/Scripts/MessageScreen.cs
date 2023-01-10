using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class MessageScreen : Singleton<MessageScreen>
{
    [SerializeField] Transform _messageEntryContentRoot;
    [SerializeField] TextMeshProUGUI _messageEntryPrefab;

    public MessageEntrySO Entries;

    void Start()
    {
        AddEntry("Greeting");
        AddEntry("0");
        AddEntry("1");
    }

    void Update()
    {

    }
    public void AddEntry(string key)
    {
        var entryString = Entries.Entries.Where(x => x.Key.Equals(key)).Single().Entry;
        entryString += Environment.NewLine;
        var messageEntry = Instantiate(_messageEntryPrefab, _messageEntryContentRoot);
        messageEntry.text = entryString;
    }
}
