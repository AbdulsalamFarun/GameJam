using UnityEngine;
public enum ItemType
{
    Burger,
    meat,
    Cheese,
    carrot,
    pizza,

 


}
public class ItemData : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int value;
    public float time;
}
