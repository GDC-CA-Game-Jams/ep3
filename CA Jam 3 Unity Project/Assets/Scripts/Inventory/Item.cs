using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Pickup))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;

    private void Start() {
        GetComponent<Pickup>().OnPickUp.AddListener(PickUpHandler);
    }

    private void PickUpHandler(GameObject pickUpSubject) {
        //check to see if object calling pickup has an inventory
        Inventory inventory = pickUpSubject.GetComponent<Inventory>();

        if (inventory != default) {
            inventory.AddItem(itemData);

            //This object has been picked up, destroy it now
            Destroy(gameObject);
        }
    }
}
