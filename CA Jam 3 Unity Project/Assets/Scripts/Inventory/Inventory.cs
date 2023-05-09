using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    //can get a reference to items
    public UnityEvent Updated;

    //Items plus count
    public Dictionary<ItemData, int> Items { get; private set; } = new();

    public void AddItem(ItemData itemData) {
        if (Items.ContainsKey(itemData)) {
            Items[itemData]++;
        }
        else {
            Items.Add(itemData, 1);

            itemData.onPickup?.Invoke();
        }

        Updated.Invoke();

        foreach (var item in Items)
            Debug.Log(item + ",");
    }

    public bool RemoveItem(ItemData itemData) {
        if (Items.ContainsKey(itemData)) {
            Items[itemData]--;

            if (Items[itemData] == 0) {
                Items.Remove(itemData);
            }

            Updated.Invoke();
            //the item was removed from the inventory
            return true;
        }

        //item was not found and therefore nore removed
        return false;
    }

    public void RemoveAllItems()
    {
        Items.Clear();
        Updated.Invoke();
    }

    public bool CheckForItem(ItemData itemData) {
        return Items.ContainsKey(itemData);
    }
}
