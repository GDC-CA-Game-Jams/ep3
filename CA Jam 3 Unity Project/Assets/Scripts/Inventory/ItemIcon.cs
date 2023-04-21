using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Image))]
public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMPro.TextMeshProUGUI countUI;

    private ItemData itemData;

    private InventoryUI inventoryUI;

    private Transform originalParent;

    public void Init(InventoryUI ui, ItemData data, int count) {
        inventoryUI = ui;
        itemData = data;

        if (image == default) {
            image = GetComponent<Image>();
        }

        image.sprite = itemData.Icon;

        countUI.text = count.ToString();
    }

    public void SetCount(int count) {
        countUI.text = count.ToString();
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originalParent = transform.parent;
        transform.parent = transform.parent.parent;
    }
    public void OnEndDrag(PointerEventData eventData) {
        //transform.parent = originalParent;

        //for now we will simply remove this item from the inventory
        inventoryUI.ItemRemovedByUser(itemData);
    }



}
