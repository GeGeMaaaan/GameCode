using Gamee._Models;
using Gamee.Components;
using Gamee.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager
{
    public static class ItemActionManager
    {
        
        public static void UpdateTheAction(Item item)
        {
            var NeedInteraction = new List<InteractionForItem>();
            if(item.ItemType == ItemType.Equitable)
            {
                EquitableItem equitableItem = (EquitableItem)item;
                if (!equitableItem.IsEquip)
                {
                    NeedInteraction.Add(new InteractionForItem("Экипировать", Equip));
                }
                else NeedInteraction.Add(new InteractionForItem("Снять", TakeOff));
            }
            else if (item.ItemType == ItemType.Weapon)
            {
                WeaponItem equitableItem = (WeaponItem)item;
                if (!equitableItem.IsEquip)
                {
                    NeedInteraction.Add(new InteractionForItem("Экипировать в левую руку", EquipLeftHand));
                    NeedInteraction.Add(new InteractionForItem("Экипировать в правую руку", EquipRightHand));
                }
                else NeedInteraction.Add(new InteractionForItem("Снять", TakeOff));
            }
            else if (item.ItemType ==ItemType.None)
            {

            }
            else if(item.ItemType == ItemType.Consumable&&item.InventoryOwner==item.HeroInventory)
            {
                NeedInteraction.Add(new InteractionForItem("Использовать", Consume));
            }
            if (item.InventoryOwner != item.HeroInventory)
            {
                NeedInteraction.Add(new InteractionForItem("Взять", PickUp));
            }
            void Consume(InventoryCell cell)
            {
               ConsumableItem consumableItem = (ConsumableItem)item;
                item.InventoryOwner.Stats.ModifyStatsByItem(consumableItem.ParameterChanges,consumableItem.StatsChanges);
                cell.RemoveItem();
            }
            void TakeOff(InventoryCell cell)
            {
                EquitableItem equitableItem = (EquitableItem)item;
                equitableItem.UnEquip();
                item.InventoryOwner.AddItem(item);
                cell.RemoveItem();
            }
            void Equip(InventoryCell cell)
             {
                EquitableItem equitableItem = (EquitableItem)item;
                if (item.HeroInventory.equipment.TryEquip(equitableItem))
                {
                    
                    cell.RemoveItem();
                }
             }
            void EquipLeftHand(InventoryCell cell)
            {
                WeaponItem weapon = (WeaponItem)item;
                if (item.HeroInventory.equipment.TryEquipLeft(weapon))
                {
                    
                    cell.RemoveItem();
                }
            }
            void EquipRightHand(InventoryCell cell)
            {
                WeaponItem weapon = (WeaponItem)item;
                if (item.HeroInventory.equipment.TryEquipRight(weapon))
                {
                   
                    cell.RemoveItem();
                }
            }
            void PickUp(InventoryCell cell)
             {
                item.HeroInventory.AddItem(item);
                cell.RemoveItem();
             }
            item._interactions = NeedInteraction;
        }
        
    }
}
