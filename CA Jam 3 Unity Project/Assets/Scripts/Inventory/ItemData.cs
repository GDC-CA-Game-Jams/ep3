using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Create Item", order = 1)]
public class ItemData : ScriptableObject
{
    public string ID { get { return id; } }
    public Sprite Icon { get { return icon; } }

    [SerializeField]
    private string id;

    [SerializeField]
    private Sprite icon;
}
