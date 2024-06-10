using Gamee._Manager;
using Gamee._Models;
using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{
    public class BoxItem : Item
    {
        
        public BoxItem(InventoryComponent inventoryOwner,InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            itemType = ItemType.Peaceable;
            Name = "Коробка";
            ID= 0;
            Icon = "Flag";
            Description = "Просто коробка для теста.Можно поставить";

            
        }
    }
}
