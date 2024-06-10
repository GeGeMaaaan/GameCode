using Gamee._Models.Components;
using Gamee.Interface;
using Gamee._Models;
using Gamee;
    public class House : Sprite, ICollidable
    {
        private readonly RenderComponent _renderComponent;
        private readonly CollisionComponent _collisionComponent;
        //private readonly MouseHoverComponent _mouseHoverComponent;
        //private readonly ActiveObjectComponent _activeObjectComponent;
        public House(Vector2 pos, string textureName, GameServiceContainer services) : base(pos, textureName, services)
        {
            _renderComponent = new RenderComponent(this);
            _collisionComponent = new CollisionComponent(this,CollisionType.None,true);
            AddComponent(_renderComponent);
            AddComponent(_collisionComponent);
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
        public CollisionComponent CollisionComponent => _collisionComponent;
        

    }


