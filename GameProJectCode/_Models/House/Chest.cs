using Gamee._Manager;
using Gamee.Components;
using Gamee.Interface;
using Gamee.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models
{
    internal class Chest : StandartActiveSprite, IInventory
    {

        private InventoryComponent _inventoryComponent;
        private Hero hero;
        private ObjInventoryManager inventoryManager;



        public Chest(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            hero = services.GetService<Hero>();
            _inventoryComponent = new InventoryComponent(this, 10);
            inventoryManager = new ObjInventoryManager(_inventoryComponent, Globals.Content.Load<Texture2D>("InteractionMenuBase"));
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
            _activeObjectComponent.AddInteraction("Открыть сундук", Open);
            Fill();
            AddComponent(_inventoryComponent);

        }

        private void Fill()
        {
            _inventoryComponent.AddItem(new ArmorItem(_inventoryComponent, hero.InventoryComponent));
        }
        public void Open()
        {
            _inventoryComponent.ToggleInventoryVisibility();
            _activeObjectComponent.RemoveInteraction("Открыть сундук", Open);
            _activeObjectComponent.AddInteraction("Закрыть сундук", Close);
        }
        public void Close()
        {
            _inventoryComponent.ToggleInventoryVisibility();
            _activeObjectComponent.RemoveInteraction("Закрыть сундук", Close);
            _activeObjectComponent.AddInteraction("Открыть сундук", Open);
        }
        private void Investigate()
        {
            _subtitleManager.AddSubtitle("Обыкновенный сундук. Думаю Алексей не будет против если я у него что нибудь позаимствую");
        }
        public InventoryManager InventoryManager => inventoryManager;
    }
}
