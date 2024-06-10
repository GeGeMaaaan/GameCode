using Gamee._Models;
using Gamee.Components;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager
{
    public class Menu
    {
        protected bool _isVisible;
        private bool ClickOnInteraction;
        protected bool isDisplay;
        protected bool IsClickOnInteraction(Vector2 clickPosition, Rectangle menuBound)
        {
            return menuBound.Contains(clickPosition);
        
        }
        public void DisplayInteractionMenu(Sprite _owner,List<Interaction> _interactions,Texture2D menuTexture)
        {

            Vector2 menuPosition = _owner.Position + new Vector2(_owner.Width, 0); 
            for (int i = 0; i < _interactions.Count; i++)
            {
                string interactionName = _interactions[i].Name;
                SpriteFont font = Globals.Content.Load<SpriteFont>("font/FileSmall");
                Globals.SpriteBatch.Draw(menuTexture, menuPosition, Color.White);
                Globals.SpriteBatch.DrawString(font, interactionName, menuPosition, Color.White);

                menuPosition.Y += 60; 
            }
            isDisplay = true;
        }
        public void DisplayInteractionMenu(InventoryCell _owner,Texture2D menuTexture)
        {
            Vector2 menuPosition = new Vector2(_owner.GetCellBounds().Location.X, _owner.GetCellBounds().Location.Y); 
            Item item = _owner.GetItem();
            for (int i = 0; i < item._interactions.Count; i++)
            {
                string interactionName = item._interactions[i].Name;
                SpriteFont font = Globals.Content.Load<SpriteFont>("font/FileSmall");
                Globals.SpriteBatch.Draw(menuTexture, menuPosition, Color.White);
                Globals.SpriteBatch.DrawString(font, interactionName, menuPosition, Color.White);

                menuPosition.Y += 60; 
            }
            isDisplay = true;

        }
        public void HandleMenuClick(Vector2 clickPosition, InventoryCell _owner)
        {
            var _interactions = _owner.GetItem()._interactions;
            ClickOnInteraction = false;
            Vector2 menuPosition = new Vector2(_owner.GetCellBounds().Location.X, _owner.GetCellBounds().Location.Y); // Начальная позиция меню

            for (int i = 0; i < _interactions.Count; i++)
            {
                Rectangle menuBound = new Rectangle((int)menuPosition.X, (int)menuPosition.Y, 300, 60);

                if (IsClickOnInteraction(clickPosition, menuBound))
                {
                    ClickOnInteraction = true;
                    _interactions[i].Action.Invoke(_owner);
                    _isVisible = false;
                    break;
                }

                menuPosition.Y += 60;
            }
            if (ClickOnInteraction == false)
            {
                _isVisible = false;
            }
        }
        public void HandleMenuClick(Sprite _owner, Vector2 clickPosition, List<Interaction> _interactions)
        {
            ClickOnInteraction = false;
            Vector2 menuPosition = _owner.Position + new Vector2(_owner.Width,0);

            for (int i = 0; i < _interactions.Count; i++)
            {
                Rectangle menuBound = new Rectangle((int)menuPosition.X, (int)menuPosition.Y, 300, 60);

                if (IsClickOnInteraction(clickPosition, menuBound))
                {
                    ClickOnInteraction = true;
                    _interactions[i].Action.Invoke();
                    _isVisible = false;
                    break;
                }

                menuPosition.Y += 60; 
            }
            if (ClickOnInteraction == false)
            {
                _isVisible = false;
            }
        }
        public bool IsVisible()
        {
            return _isVisible;
        }

        public void ToggleVisibility()
        {
            _isVisible = !_isVisible;
        }
    }
}
