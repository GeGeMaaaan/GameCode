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
//    public class Apple : Sprite,IActive
//    {
//        private RenderComponent _renderComponent;
//        private CollisionComponent _collisionComponent;
//        private ActiveObjectComponent _activeObjectComponent;
//        private Hero _hero;
//        private SubtitleManager _subtitleManager;
//        public Apple(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
//        {
//            _subtitleManager = services.GetService<SubtitleManager>();
//            _hero = services.GetService<Hero>();
//            _collisionComponent = new CollisionComponent(this, CollisionType.None, true);
//            _activeObjectComponent = new ActiveObjectComponent(this, ActiveType.NearPlayer);
//            _renderComponent = new RenderComponent(this);
//            _activeObjectComponent.AddInteraction("Подобрать", PickUp);
//            AddComponent(_collisionComponent);
//            AddComponent(_activeObjectComponent);
//            AddComponent(_renderComponent);
//        }
//        public void PickUp()
//        {
//            _hero.InventoryComponent.AddItem(new AppleItem(_hero.InventoryComponent, _hero.InventoryComponent));
//            DeleteSprite();
//        }
//        public void Investigate()
//        {
//            _subtitleManager.AddSubtitle("Яблоко. Кажется свежим");
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
