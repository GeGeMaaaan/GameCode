//using Gamee._Manager;
//using Gamee.Interface;
//using Gamee.Items;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Gamee._Models.House
//{
//    public class Rifle: Sprite, IActive
//    {
//        private RenderComponent _renderComponent;
//        private CollisionComponent _collisionComponent;
//        private ActiveObjectComponent _activeObjectComponent;
//        private SubtitleManager _subtitleManager;
//        private Hero _hero;

        

//        public Rifle(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
//        {
//            _hero = gameServiceContainer.GetService<Hero>();
//            _collisionComponent = new CollisionComponent(this, CollisionType.None, true);
//            _activeObjectComponent = new ActiveObjectComponent(this, ActiveType.NearPlayer);
//            _renderComponent = new RenderComponent(this);
//            _subtitleManager = services.GetService<SubtitleManager>();
//            _activeObjectComponent.AddInteraction("Взять", PickUp);
//            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
//            AddComponent(_collisionComponent);
//            AddComponent(_activeObjectComponent);
//            AddComponent(_renderComponent);
//        }
//        public void PickUp()
//        {
//            _hero.InventoryComponent.AddItem(new RifleItem(_hero.InventoryComponent, _hero.InventoryComponent));
//            DeleteSprite();
//        }
//        public void Investigate()
//        {
//            _subtitleManager.AddSubtitle("Винтовка. На охотичую не похожа.");
//        }
//        public override void Update()
//        {

//            foreach (var component in _components)
//            {
//                component.Update();
//            }
//        }

//        public override void Draw()
//        {
//            foreach (var component in _components)
//            {
//                component.Draw();
//            }
//        }
//        public ActiveObjectComponent ActiveObjectComponent => _activeObjectComponent;
//    }
//}
