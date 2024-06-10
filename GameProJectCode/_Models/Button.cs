using Gamee.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models
{
    public class Button : Sprite, IActive
    {
        private RenderComponent _renderComponent;
        private CollisionComponent _collisionComponent;
        private ActiveObjectComponent _activeObject;
        public Button(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _collisionComponent = new CollisionComponent(this,CollisionType.None,true);
            _activeObject = new ActiveObjectComponent(this, ActiveType.OnlyClick);
            _renderComponent = new RenderComponent(this);
            _activeObject.AddInteraction("Начать игру", StartGame);
            AddComponent(_renderComponent);
            AddComponent(_collisionComponent);
            AddComponent(_activeObject);
        }
        private void StartGame()
        {
            eventManager.Invoke("StartGame");
        }
        public override void Update()
        {
            foreach(var _component in _components)
            {
                _component.Update();
            }
        }
        public override void Draw()
        {
            foreach (var _component in _components)
            {
                _component.Draw();
            }
        }
        public void DrawActive()// Кнопка не меняется
        {
            foreach (var _component in _components)
            {
                _component.Draw();
            }
        }
        public ActiveObjectComponent ActiveObjectComponent => _activeObject;

        public CollisionComponent CollisionComponent => _collisionComponent;

        public ActiveObjectComponent ActiveObjectComponentForFight => _activeObject;
    }
}
