using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{
    public class WeaponItem : EquitableItem
    {
        protected List<Skill> WeaponSkill;
        public WeaponItem(InventoryComponent inventoryOwner, InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            WeaponSkill= new List<Skill>();
            
        }
        public List<Skill> GetSkills()
        {
            return WeaponSkill;
        }
    }
}
