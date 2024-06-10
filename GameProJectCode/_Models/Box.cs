//using Gamee._Manager;
//using Gamee.Interface;
//using Gamee.Items;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Gamee._Models
//{
//    public class Box : Sprite,ICollidable,IActive
//    {
//        private RenderComponent _renderComponent;
//        private CollisionComponent _collisionComponent;
//        private ActiveObjectComponent _activesObjectComponent;
//        private Hero _hero;
//        public Box(Vector2 position, string textureName, GameServiceContainer services) : base(position, textureName, services)
//        {
//            _hero = services.GetService<Hero>();
//            _activesObjectComponent = new ActiveObjectComponent(this,ActiveType.NearPlayer);
//            _collisionComponent = new CollisionComponent(this,CollisionType.None, true);
//            _collisionComponent.ChangeCollsion();
//            _renderComponent = new RenderComponent(this);
//            _activesObjectComponent.AddInteraction("Подобрать", PickUp);
//            AddComponent(_collisionComponent);
//            AddComponent(_renderComponent);
//            AddComponent(_activesObjectComponent);
            
//        }
//        public void PickUp()
//        {
//            _hero.InventoryComponent.AddItem(new BoxItem(_hero.InventoryComponent, _hero.InventoryComponent));
//            DeleteSprite();
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
//            };
//        }
//        public CollisionComponent CollisionComponent => _collisionComponent;
//        public ActiveObjectComponent ActiveObjectComponent => _activesObjectComponent;
//    }

//}