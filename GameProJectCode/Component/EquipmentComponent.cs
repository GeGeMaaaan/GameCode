using Gamee._Manager;
using Gamee._Models;
using Gamee.Components;
using Gamee.Interface;
using Gamee.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EquipmentCell : InventoryCell
{

    private EquitableItem equitable;
    Dictionary<string, int> zeroStatsChange = new Dictionary<string, int>
        {
            {"Health", 0},
            {"Damage", 0},
            {"Defense", 0}
        };

    public EquipmentCell()
    {
       
    }
    public Dictionary<string,int> GetParameterChange()
    {
        
            return (item as EquitableItem).ParameterChanges;
        
    }
    public Dictionary<MainStats, int> GetStatsChange()
    {
        
            return (item as EquitableItem).StatsChange;
        
    }


}
public class EquipmentComponent : Component
{
    private EquipmentCell headCell;
    private EquipmentCell bodyCell;
    private EquipmentCell legCell;
    private EquipmentCell armCell;
    private List<EquipmentCell> equipmentCells;
    private StatsComponent statsComponent;
    private InventoryComponent _inventoryOwner;
    private EquipmentCell rightHand;
    private EquipmentCell leftHand;
    public EquipmentComponent(Sprite owner,StatsComponent stats,InventoryComponent inventoryOwner) : base(owner)
    {
        _inventoryOwner = inventoryOwner;
        statsComponent = stats;
        headCell = new EquipmentCell();
        bodyCell = new EquipmentCell();
        legCell = new EquipmentCell();
        armCell = new EquipmentCell();
        rightHand = new EquipmentCell();
        leftHand = new EquipmentCell();
        equipmentCells =
        [
            headCell,
            bodyCell,
            legCell,
            armCell,
            rightHand,
            leftHand,
        ];
    }
    public bool TryEquip(EquitableItem item)
    {
        if (item.BodyPart ==BodyPart.Head)
        {
            if (!headCell.HasItem())
            {
                headCell.SetItem(item);
                item.Equip();
            }
        }
        else if (item.BodyPart == BodyPart.Body)
        {
            if (!bodyCell.HasItem())
            {
                bodyCell.SetItem(item);
                item.Equip();
            }
        }
        else if (item.BodyPart == BodyPart.Boots)
        {
            if (!legCell.HasItem())
            {
                legCell.SetItem(item);
                item.Equip();
            }
        }
        else if (item.BodyPart == BodyPart.Arm)
        {
            if (!armCell.HasItem())
            {
                armCell.SetItem(item);
                item.Equip();
            }
        }
        else return false;
       
        return true;
    }
    public bool TryEquipLeft(WeaponItem item)
    {
       if (!leftHand.HasItem())
       {
           leftHand.SetItem(item);
            item.Equip();
        }
        else return false;

        return true;
    }
    public bool TryEquipRight(WeaponItem item)
    {
        if (!rightHand.HasItem())
        {
            rightHand.SetItem(item);
            item.Equip();
        }
        else return false;

        return true;
    }
    public override void Draw()
    {
          
    }

    public override void Update()
    {
        foreach (var equipmentCell in equipmentCells)
        {
            if (equipmentCell.HasItem())
            {
                equipmentCell.GetItem().InventoryOwner = _inventoryOwner;
                ItemActionManager.UpdateTheAction(equipmentCell.GetItem());
                statsComponent.ModifyStatsByEquipment(equipmentCell.GetParameterChange(),equipmentCell.GetStatsChange());
            }
            
        }
    }
    public EquipmentCell RightHand => rightHand;
    public EquipmentCell LeftHand => leftHand;
    public List<EquipmentCell> EquipmentCells => equipmentCells;
    public StatsComponent StatsComponent => statsComponent;

}

