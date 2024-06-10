//using Gamee._Manager;
//using Gamee.Components;
//using Gamee.Interface;
//using Gamee.Items;
//using System;
//using System.Collections.Generic;
using Gamee._Manager;
using Gamee.Components;
using Gamee.Interface;
using Gamee.Items;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.House
{
    public class Dresser : StandartActiveSprite, IInventory
    {
        private InventoryComponent _inventoryComponent;
        private Hero hero;
        private ObjInventoryManager inventoryManager;
        public Dresser(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            hero = services.GetService<Hero>();
            _inventoryComponent = new InventoryComponent(this, 3);
            inventoryManager = new ObjInventoryManager(_inventoryComponent, Globals.Content.Load<Texture2D>("InteractionMenuBase"));
            _activeObjectComponent.AddInteraction("Открыть", Open);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
            AddComponent(_collisionComponent);
            AddComponent(_activeObjectComponent);
            AddComponent(_renderComponent);
            AddComponent(_inventoryComponent);
        }
        private void FillTheChest()
        {
            _inventoryComponent.AddItem(new ArmorItem(_inventoryComponent, hero.InventoryComponent));
        }
        public void Investigate()
        {
            _subtitleManager.AddSubtitle("Старый камод. Навивает воспоминаний из юности");
        }
        public void Open()
        {
            _inventoryComponent.ToggleInventoryVisibility();
            _activeObjectComponent.RemoveInteraction("Открыть", Open);
            _activeObjectComponent.AddInteraction("Закрыть", Close);
        }
        public void Close()
        {
            _inventoryComponent.ToggleInventoryVisibility();
            _activeObjectComponent.RemoveInteraction("Закрыть", Close);
            _activeObjectComponent.AddInteraction("Открыть", Open);
        }
        public override void Update()
        {

            foreach (var component in _components)
            {
                component.Update();
            }
        }

        public override void Draw()
        {
            foreach (var component in _components)
            {
                component.Draw();
            }
        }
        public ActiveObjectComponent ActiveObjectComponent => _activeObjectComponent;

        public InventoryManager InventoryManager => inventoryManager;
    }
}
