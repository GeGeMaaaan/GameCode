using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Items
{
    public class Revolver : WeaponItem
    {
        public Revolver(InventoryComponent inventoryOwner, InventoryComponent heroInventory) : base(inventoryOwner, heroInventory)
        {
            WeaponSkill.Add(new AttackSkill("Выстрел в упор", "Наносит 2-5 урона. Дальность 1 клетка.Требует 2 единицы времени", 2, 2, 2, 5, 1));
            WeaponSkill.Add(new AttackSkill("Прицельный высрел", "Наносит 8-10 урона. Дальность 4 клетки. Требует 3 единицы времени", 2, 3, 8, 10, 4));
            WeaponSkill.Add(new ComboSkill("Выстрел с отходом", "Наносит 1-2 урона и перемещает на одну клетку влево. Дальность 1 клетки. Требует 2 единицы времени", 3, 2,
                new AttackSkill("","",0,0,1,2,1),new MovementSkill("","",0,0,(-1,0),MovementType.relative)));
            itemType = ItemType.Weapon;
            Name = "Револьвер";
            ID = 5;
            Icon = "Revolver";
            Description = "Револьвер который я приобрел после службы.";
        }
    }
}
