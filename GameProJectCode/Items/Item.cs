using System.Collections.Generic;
using Gamee.Components;
public delegate void InteractionItem(InventoryCell cell);
public class Item
{
    public string Icon { get; protected set; }
    public string Name { get; protected set; }
    public int ID { get; protected set; }
    public string Description { get; protected set; }
    public InventoryComponent InventoryOwner { get;  set; }
    public List<InteractionForItem> _interactions = new List<InteractionForItem>();//Не должобыть публичным
    private InventoryComponent _heroInventory;
    protected ItemType itemType;
    public Item (InventoryComponent inventoryOwner,InventoryComponent heroInventory)
    {
        itemType = ItemType.None;
        InventoryOwner = inventoryOwner;
        _heroInventory = heroInventory;
    }
    public InventoryComponent HeroInventory => _heroInventory;
    public ItemType ItemType => itemType;
}

