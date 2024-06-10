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
    public class ArmorItem:EquitableItem
    {

        public ArmorItem(InventoryComponent inventoryOwner,InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            itemType = ItemType.Equitable;
            bodyPart = BodyPart.Body;
            Name = "Броня";
            Description = "Броня для теста.\n+10 защиты";
            ID = 1;
            Icon = "Flag";
            parameterChange["Defense"] = 10;
        }
    }
}
