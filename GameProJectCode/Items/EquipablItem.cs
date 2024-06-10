using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{
    public class EquitableItem : Item
    {
        protected BodyPart bodyPart;
        protected Dictionary<string, int> parameterChange;
        protected Dictionary<MainStats, int> statsChange;
        private bool _isEquip;
        public EquitableItem(InventoryComponent inventoryOwner, InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {     
            statsChange = new Dictionary<MainStats, int>();
            parameterChange = new Dictionary<string, int>();
        }

        
        public void Equip()
        {
            _isEquip =true;
        }
        public void UnEquip()
        {
            _isEquip = false;
        }
        public bool IsEquip => _isEquip;

        public Dictionary<string, int> ParameterChanges => parameterChange;
        public Dictionary<MainStats, int> StatsChange => statsChange;
        public BodyPart BodyPart => bodyPart;
    }
    
}
