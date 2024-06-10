using Gamee._Manager;
using Gamee._Models;
using Gamee.Items;
using Gamee.Manager;
using System.Collections.Generic;

namespace Gamee.Components
{
    public class InventoryCell
    {
        private float hoverTime;
        protected Item item;
        private Rectangle cellBounds;
        private bool showText = false;
        public bool HasItem()
        {
            return item != null;
        }
        public void SetItem(Item item)
        { 
            this.item = item;
            
        }
        public bool NeedToShowText()
        {
            return showText;
        }
        public Item GetItem()
        {
            if (this.item != null)
            {
                return item;
            }
            else { return null; }
        }
        public void SetCellBounds(Rectangle rectangle)
        {
            cellBounds = rectangle;
        }
        public Rectangle GetCellBounds()
        {
            return cellBounds;
        }
        public void RemoveItem()
        {
            item = null;
        }
        public void Update(float HoverDelaySeconds)
        {
            if (cellBounds != default(Rectangle))
            {
                Rectangle mouseRectangle = new Rectangle((int)InputManager.MousePositionForUI.X, (int)InputManager.MousePositionForUI.Y, 1, 1);

                if (mouseRectangle.Intersects(GetCellBounds()))
                {
                    hoverTime += Globals.TotalSeconds;

                    if (hoverTime >= HoverDelaySeconds)
                    {
                        showText = true;
                    }
                }
                else
                {
                    hoverTime = 0f;
                    showText = false;
                }
            }
        }
    }
    
    public class InventoryComponent : Component
    {
        public EquipmentComponent equipment;
        private List<InventoryCell> _cell;
        private bool _inventoryVisible;
        private int _MaxSlots;
        private StatsComponent _stats;
        public InventoryComponent(Sprite owner, int maxSlots,StatsComponent statsComponent = null) : base(owner)
        {
            if (statsComponent!=null)
            {
                _stats = statsComponent;
                equipment = new EquipmentComponent(owner, statsComponent,this);
            }   
            _cell = new List<InventoryCell>();
            _inventoryVisible = false; 
            _MaxSlots = maxSlots;
            for (int i = 0; i < maxSlots; i++)
            {
                _cell.Add(new InventoryCell());
            }
        }
        public StatsComponent Stats => _stats;
        public void AddItem(Item item)
        {
            for (int i = 0; i < _MaxSlots; i++)
            {
                if (!_cell[i].HasItem())
                {
                    
                    _cell[i].SetItem(item);
                    break;
                } 
            }
        }
        public int MaxSlots()
        {
            return _MaxSlots;
        }
        // Получить все предметы в инвентаре
        public List<InventoryCell> GetCells()
        {
            return _cell;
        }
        public Sprite ReturnOwner()
        {
            return _owner;
        }
        public void ToggleInventoryVisibility()
        {
            _inventoryVisible = !_inventoryVisible;
        }

        public bool IsInventoryVisible()
        {
            return _inventoryVisible;
        }
        public override void Update()
        {
            foreach(var cell in _cell)
            {

                if (cell.HasItem())
                {
                    cell.GetItem().InventoryOwner=this;
                    ItemActionManager.UpdateTheAction(cell.GetItem());
                }
            }
            if(equipment!=null)
            {

                equipment.Update();
            }
        }

        public override void Draw()
        {
            if (equipment != null)
            {
                equipment.Draw();
            }
        }
    }
}