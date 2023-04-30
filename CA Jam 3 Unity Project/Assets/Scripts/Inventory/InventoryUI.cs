using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Inventory Inventory = null;

    [SerializeField]
    private GameObject ContentBox = null;

    [SerializeField]
    private GameObject ItemIconPrefab = null;

    private Dictionary<ItemData, ItemIcon> ItemIcons = new(); 

    // Start is called before the first frame update
    void Start()
    {
        //register with the inventory
        Inventory.Updated.AddListener(HandleInventoryUpdate);

        if(Inventory == null)
        {
            Debug.LogError("Inventory UI needs to be linked to Player Inventory!");
            Debug.Break();
        }
    }

    //TODO: This will need to be refactored, it is gross
    void HandleInventoryUpdate() {
        var inventory = Inventory.Items;

        //Add new Icons, or skip of already there
      foreach(var item in inventory) {
            ItemData itemData = item.Key;
            int itemCount = item.Value;
            if (ItemIcons.ContainsKey(itemData)) {
                //update the count
                ItemIcons[itemData].SetCount(itemCount);

                //ensure the the itemicon is reset
                ItemIcons[itemData].gameObject.transform.parent = ContentBox.transform;
            }
            else {
                //spawn a new icon
                var go = Instantiate<GameObject>(ItemIconPrefab, ContentBox.transform);
                ItemIcon itemIcon = go.GetComponent<ItemIcon>();
                itemIcon.Init(this, itemData, itemCount);

                ItemIcons.Add(item.Key, itemIcon);
            }
        }

        //keep track of items that were removed this time
        //need to do this as we cannot remove while iterating
        List<ItemData> itemsToRemove = new();

        //Remove Icons that do not need to be there
        foreach (var itemIcon in ItemIcons) {
            if (!inventory.ContainsKey(itemIcon.Key)) {
                itemsToRemove.Add(itemIcon.Key);
                Destroy(itemIcon.Value.gameObject);
            }
        }

        for(int i = 0; i < itemsToRemove.Count; i++) {
            ItemIcons.Remove(itemsToRemove[i]);
        }
    }

    public void ItemRemovedByUser(ItemData itemData) {
        Inventory.RemoveItem(itemData);
    }
}
