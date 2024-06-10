using Gamee._Manager;
using Gamee.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models
{
    public class StandartActiveSprite : Sprite,IActive
    {
        protected RenderComponent _renderComponent;
        protected CollisionComponent _collisionComponent;
        protected ActiveObjectComponent _activeObjectComponent;
        protected SubtitleManager _subtitleManager;
        protected ActiveObjectComponent _activeObjectComponentForFight;
        public StandartActiveSprite(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponentForFight = new ActiveObjectComponent(this, ActiveType.NearPlayer);
            _subtitleManager = services.GetService<SubtitleManager>();
            _collisionComponent = new CollisionComponent(this, CollisionType.None, true);
            _activeObjectComponent = new ActiveObjectComponent(this, ActiveType.NearPlayer);
            _renderComponent = new RenderComponent(this);

            AddComponent(_collisionComponent);
            AddComponent(_activeObjectComponent);
            AddComponent(_renderComponent);
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
        public void DrawActive()
        {
            foreach (var component in _components)
            {
                if (component is RenderComponent)
                {
                    (component as RenderComponent).DrawActive();
                }
                else
                {
                    component.Draw();
                }
            }
        }
        public ActiveObjectComponent ActiveObjectComponentForFight => _activeObjectComponentForFight;
        public ActiveObjectComponent ActiveObjectComponent => _activeObjectComponent;

        public CollisionComponent CollisionComponent =>_collisionComponent;
    }
}
