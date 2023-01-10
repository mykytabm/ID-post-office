using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MessageEntry
{
    public MessageEntry(string key, string entry)
    {
        Key = key;
        Entry = entry;
    }

    [field: SerializeField]
    public string Key { get; set; }
    [field: SerializeField]
    public string Entry { get; set; }
}

[CreateAssetMenu]
public class MessageEntrySO : ScriptableObject
{
    public List<MessageEntry> Entries;
}