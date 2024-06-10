using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{
    public class AppleItem : ConsumableItem
    {
        public AppleItem(InventoryComponent inventoryOwner, InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            itemType = ItemType.Consumable;
            Name = "Яблоко";
            ID = 0;
            Icon = "Flag";
            Description = "Яблоко.Должно быть вкусное.\n Востонавливает 1 здоровье";
            

        }
    }
}
