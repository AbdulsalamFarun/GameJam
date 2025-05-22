using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;

    public InventoryItem(ItemData data)
    {
        itemData = data;
    }
}