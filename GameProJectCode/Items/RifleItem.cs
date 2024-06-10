using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{
    public class RifleItem : WeaponItem
    {
        public RifleItem(InventoryComponent inventoryOwner, InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            
        }
    }
}
