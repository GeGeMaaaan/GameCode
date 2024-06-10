using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{

    public class ConsumableItem:Item
    {
        private Dictionary<string, int> parameterChanges;
        private Dictionary<MainStats, int> statsChanges;
        public ConsumableItem(InventoryComponent inventoryOwner, InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            parameterChanges = new Dictionary<string, int>();
            statsChanges = new Dictionary<MainStats, int>();
        }

        public Dictionary<string, int> ParameterChanges => parameterChanges;
        public Dictionary<MainStats, int> StatsChanges => statsChanges;
    }
}
