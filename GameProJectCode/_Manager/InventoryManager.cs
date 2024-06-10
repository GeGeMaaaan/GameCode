using Gamee.Components;
using Gamee.Manager;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gamee._Manager
{
    public class InventoryManager: Menu
    {

        protected InventoryComponent _inventoryComponent;
        private Texture2D menuTexture;
        private bool _isDraggingItem = false;
        private InventoryCell _owner;
        private Vector2 prevMousePos;
        private InventoryCell _draggedItemCell = null;
        private Rectangle _inventoryBounds;
        protected Vector2 _position;
        private EquipmentComponent _equipment;
        private int itemInRow;
        private int CeilWidth = 50;
        private int margin = 5;
        private bool hasEqupment = false;
        public InventoryManager(InventoryComponent inventoryComponent, Texture2D texture)
        {

            itemInRow = 7;
            _inventoryComponent = inventoryComponent;
            if (inventoryComponent.equipment != null)
            {
                _equipment = inventoryComponent.equipment;
            }
            menuTexture = texture;
            hasEqupment = _inventoryComponent.equipment != null;
        }
        public void ChangeItemInRow(int newItemInRow)
        {
            itemInRow = newItemInRow;
        }
        public virtual void Update()
        {

        }
        public virtual void Draw()
        {

        }
        public void BasicUpdate()
        {

            if (!_isVisible) isDisplay = false;
            
            if (_inventoryComponent.IsInventoryVisible())
            {
                if (_isDraggingItem && InputManager.MouseState.LeftButton == ButtonState.Released)
                {
                    if (Vector2.Distance(InputManager.MousePositionForUI, prevMousePos) > 10)
                    {
                        foreach (var cell in _inventoryComponent.GetCells())
                        {
                            if (cell != _draggedItemCell && cell.GetCellBounds().Contains(InputManager.MousePositionForUI))
                            {
                                cell.SetItem(_draggedItemCell.GetItem());
                                _draggedItemCell.RemoveItem();
                                _owner = cell;
                                // Перемещаем предмет в новую ячейку
                                break;
                            }
                        }
                    }
                    _isDraggingItem = false;
                    _draggedItemCell = null;
                }
                if (isDisplay && InputManager.wasClickForInteraction)
                {
                    HandleMenuClick(InputManager.MousePositionForUI, _owner);
                }
                else if (InputManager.wasClickForInteraction)
                {
                    if (!_isVisible)
                    {
                        if (hasEqupment)
                            HandleEquipmentClick();
                        HandleInventoryClick2();
                    }
                    else _isVisible= false;

                }
                else if (InputManager.WasClick)
                {
                    HandleInventoryClick();

                }
                else
                {
                    CheckHover();
                }
                
            }
        }

        public void BasicDraw()
        {
            if (_inventoryComponent.IsInventoryVisible())
            {
                DrawInventory();
                if (_isVisible)
                {
                    DisplayInteractionMenu(_owner, menuTexture);
                }
                DrawHover();
            }
            else _inventoryBounds = new Rectangle((int)_position.X, (int)_position.Y, 0, 0);
        }
        private void DrawInventory()
        {
            if(hasEqupment)
            { 
                DrawEquipmentSlots();
                DrawStats(_equipment.StatsComponent);
            }
            Vector2 ceilPos = _position;
            int i = 1;
            foreach (InventoryCell cell in _inventoryComponent.GetCells())
            {
                Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("InventoryCeil"), ceilPos, Color.White * 0.8f);
                cell.SetCellBounds(new Rectangle((int)ceilPos.X, (int)ceilPos.Y, CeilWidth, CeilWidth));
                if (cell.HasItem())
                {
                    Item item = cell.GetItem();
                    Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>($"ItemIcons/{item.Icon}"), new Vector2(ceilPos.X , ceilPos.Y), Color.White);

                }
                if (i % itemInRow == 0)
                {
                    ceilPos = new Vector2(_position.X, ceilPos.Y + CeilWidth);
                }
                else ceilPos = new Vector2(ceilPos.X + CeilWidth, ceilPos.Y);
                i++;
            }


        }
        private void DrawEquipmentSlots()
        {
            var equipmentPos = new Vector2(_position.X - 500, _position.Y);
            Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("InventoryBase"), new Vector2(_position.X-430,_position.Y-20), Color.White * 0.8f);
            List<Vector2> equipmentPosList;
            equipmentPosList =
            [
                new Vector2(equipmentPos.X + margin + 3 * CeilWidth, equipmentPos.Y + 2*margin),//Head
                new Vector2(equipmentPos.X + margin + 3 * CeilWidth, equipmentPos.Y + 3*margin+CeilWidth),//Body
                new Vector2(equipmentPos.X + margin + 2 * CeilWidth-margin, equipmentPos.Y + 3 * margin + CeilWidth),//arm
                new Vector2(equipmentPos.X + margin + 3 * CeilWidth, equipmentPos.Y + 4* margin + 2*CeilWidth),//Leg
                new Vector2(equipmentPos.X + margin + 2 * CeilWidth-margin, equipmentPos.Y + 4 * margin + 2 * CeilWidth),//Dec1
                new Vector2(equipmentPos.X + margin + 4 * CeilWidth+margin, equipmentPos.Y + 4 * margin + 2 * CeilWidth)//Dec2
            ];
            for(int i = 0; i < equipmentPosList.Count; i++) 
            {
                DrawEquipmentSlot(_inventoryComponent.equipment.EquipmentCells[i], equipmentPosList[i]);
            }
        }

        private void DrawEquipmentSlot(EquipmentCell cell, Vector2 equipmnentPos)
        {  
                Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("InventoryCeil"), equipmnentPos, Color.White * 0.8f);
                cell.SetCellBounds(new Rectangle((int)equipmnentPos.X, (int)equipmnentPos.Y, 50, 50));
                if (cell.HasItem())
                {
                    Item item = cell.GetItem();
                    Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>($"ItemIcons/{item.Icon}"), new Vector2(equipmnentPos.X, equipmnentPos.Y), Color.White);

                }
        }
        private void DrawStats(StatsComponent stats)
        {
            var equipmnentPos = new Vector2(_position.X - 450, _position.Y);
            var FirstStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+30, equipmnentPos.Y + 6 * 20 + 50);// берется от позиции leg
            var SecondStringPos = new Vector2(equipmnentPos.X + margin + 1* CeilWidth, equipmnentPos.Y + 8 * 20 + 50);// берется от позиции leg
            var ThiedStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth, equipmnentPos.Y + 9 * 20 + 50);// берется от позиции leg
            SpriteFont font = Globals.Content.Load<SpriteFont>("font/FileVerySmall");
            SpriteFont fontForHelth = Globals.Content.Load<SpriteFont>("font/FileSmall");
            string health ="Здоровье: "+ stats.CurrentHealth + "\\" + stats.GetCurrentParameter()["MaxHealth"];
            string Damage = "Урон: "+ stats.GetCurrentParameter()["Damage"];
            string Defense = "Защита: "+ stats.GetCurrentParameter()["Defense"]; 
            Globals.SpriteBatch.DrawString(fontForHelth, health, FirstStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, Damage, SecondStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, Defense, ThiedStringPos, Color.White);


            FirstStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+100, equipmnentPos.Y + 8 * 20 + 50);// берется от позиции leg
            SecondStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+100, equipmnentPos.Y + 9 * 20 + 50);// берется от позиции leg
            ThiedStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+100, equipmnentPos.Y + 10 * 20 + 50);// берется от позиции leg
            var FourStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+100, equipmnentPos.Y + 11 * 20 + 50);
            var FiveStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+100, equipmnentPos.Y + 12 * 20 + 50);
            var SixStringPos = new Vector2(equipmnentPos.X + margin + 1 * CeilWidth+100, equipmnentPos.Y + 13 * 20 + 50);

            string strength = "Сила: " + stats.GetCurrentStats()[MainStats.strength];
            string dexterity = "Ловкость: " + stats.GetCurrentStats()[MainStats.dexterity];
            string intelligence = "Интелект: " + stats.GetCurrentStats()[MainStats.intelligence];
            string diplomacy = "Дипломатия: " + stats.GetCurrentStats()[MainStats.diplomacy];
            string medicine = "Медицина: " + stats.GetCurrentStats()[MainStats.medicine];
            string gun_mastery = "Мастерство оружеия: " + stats.GetCurrentStats()[MainStats.gun_mastery];
            Globals.SpriteBatch.DrawString(font, strength, FirstStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, dexterity, SecondStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, intelligence, ThiedStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, diplomacy, FourStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, medicine, FiveStringPos, Color.White);
            Globals.SpriteBatch.DrawString(font, gun_mastery, SixStringPos, Color.White);
        }
        private void HandleEquipmentClick()
        {
            foreach (var cell in _equipment.EquipmentCells)
            {

                if (cell.GetCellBounds().Contains(InputManager.MousePositionForUI))
                {

                    if (cell.HasItem())
                    {
                        _owner = cell;
                        _isVisible = true;
                        break;
                    }
                }
            }
        }
        private void HandleInventoryClick()
        {
            foreach (var cell in _inventoryComponent.GetCells())
            {
                if (cell.GetCellBounds().Contains(InputManager.MousePositionForUI))
                {
                    if (cell.HasItem())
                    {
                        // Начинаем перетаскивание предмета
                        _isDraggingItem = true;
                        prevMousePos = InputManager.MousePositionForUI;
                        _draggedItemCell = cell;
                        //_dragOffset = InputManager.MousePositionWorld - cell.Position;
                        break;
                    }
                }
            }

        }
        private void HandleInventoryClick2()
        {
           
            foreach (var cell in _inventoryComponent.GetCells())
            {

                if (cell.GetCellBounds().Contains(InputManager.MousePositionForUI))
                {

                    if (cell.HasItem())
                    {
                        _owner = cell;
                        _isVisible = true;
                        break;
                    }
                }
            }

        }
        private void CheckHover()
        {
            foreach (var cell in _inventoryComponent.GetCells())
            {
                cell.Update(0.5f);
            }
            if (_equipment != null)
            {
                foreach(var cell in _equipment.EquipmentCells)
                {
                    cell.Update(0.5f);
                }
            }
        }
        private void DrawHover()
        {
            foreach (var cell in _inventoryComponent.GetCells())
            {
                if (cell.NeedToShowText())
                {
                    DrawHoverWindow(cell);
                }
            }
            if (_equipment != null)
            {
                foreach (var cell in _equipment.EquipmentCells)
                {
                    if (cell.NeedToShowText())
                    {
                        DrawHoverWindow(cell);
                    }
                }
            }
        }

        private void DrawHoverWindow(InventoryCell cell)
        {
            if (cell.HasItem())
            {
                string text = cell.GetItem().Description;
                int width = 200;
                SpriteFont font = Globals.Content.Load<SpriteFont>("font/FileSmall");
                string[] lines = GetWrappedTextLines(font, text, width);
                Vector2 drawPosition = new Vector2(cell.GetCellBounds().X,cell.GetCellBounds().Y-(lines.Count()*font.LineSpacing));
                foreach (string line in lines)
                {
                    Globals.SpriteBatch.DrawString(font, line, drawPosition, Color.White);
                    drawPosition.Y += font.LineSpacing;
                }
            }
        }
        private string[] GetWrappedTextLines(SpriteFont font, string text, int width)
        {
            // Разделение текста на слова
            string[] words = text.Split(' ');
            string wrappedText = "";
            string line = "";

            foreach (string word in words)
            {
                // Проверка, помещается ли следующее слово в строку
                if (font.MeasureString(line + word).X > width)
                {
                    // Добавляем текущую строку в массив строк и начинаем новую строку
                    wrappedText += line.Trim() + "\n";
                    line = "";
                }
                // Добавляем слово к текущей строке
                line += word + " ";
            }

            // Добавляем последнюю строку
            wrappedText += line.Trim();

            // Разделение текста на строки по символу новой строки
            return wrappedText.Split('\n');
        }
        public Rectangle InventoryBounds => _inventoryBounds;
    }
}
