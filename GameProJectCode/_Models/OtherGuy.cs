//using Gamee._Manager;
//using Gamee._Models.Components;
//using Gamee.Components;
//using Gamee.Interface;
//using Gamee.Manager;
//using System.Collections.Generic;

//namespace Gamee._Models
//{
//    public class OtherGuy : Sprite, ICollidable, IActive
//    {
//        private readonly AnimationComponent _animationComponent;
//        private readonly SpeakComponent speakComponent;
//        private readonly CollisionComponent _collisionComponent;
//        private RenderComponent _renderComponent;
//        public Animation _animation;

//        public OtherGuy(Vector2 position, string textureName, GameServiceContainer services) : base(position, textureName, services)
//        {
//            _renderComponent = new RenderComponent(this);
//            _animation = new Animation(Globals.Content.Load<Texture2D>("PlayerIdle"), 10, 1, 0.3f, 1);
//            _collisionComponent = new CollisionComponent(this, CollisionType.None, false);
//            speakComponent = new SpeakComponent(this, services.GetService<Hero>(), ActiveType.NearPlayer);
//            speakComponent.AddDialog("TestDialog.txt");
//            AddComponent(_collisionComponent);
//            AddComponent(speakComponent);
//        }

//        public override void Update()
//        {
//            _animation.Update();
//            foreach (var component in _components)
//            {
//                component.Update();
//            }
//        }

//        public override void Draw()
//        {
//            _animation.Draw(Position);
//            foreach (var component in _components)
//            {
//                component.Draw();
//            }
//        }
//        public CollisionComponent CollisionComponent => _collisionComponent;

//        public ActiveObjectComponent ActiveObjectComponent => speakComponent;
//    }
//}