using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemData", menuName = "Create Item", order = 1)]
public class ItemData : ScriptableObject
{
    public string ID { get { return id; } }
    public Sprite Icon { get { return icon; } }
    public string soundEffect { get { return fmodEvent; } }
    public Action onPickup;

    [SerializeField]
    private string id;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string fmodEvent;
}
