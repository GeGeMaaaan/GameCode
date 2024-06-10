using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.House
{
    public class Ladder : StandartActiveSprite
    {
        public Ladder(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            var CollisionRectangle_1 = new Rectangle((int)Position.X, (int)Position.Y + 200, Texture.Width, Texture.Height - 200);
            var CollisionRectangle_2 = new Rectangle((int)Position.X+Texture.Width-200, (int)Position.Y, 200, 200);
            List<Rectangle>  collisonRectangles = [CollisionRectangle_1, CollisionRectangle_2];
            RemoveComponent(_collisionComponent);
            _collisionComponent = new CollisionComponent(this, CollisionType.Solid, true, collisonRectangles);
            AddComponent(_collisionComponent);
            _activeObjectComponent.AddInteraction("Подняться", ClimbUp);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
            
        }
        private void ClimbUpFight()
        {
            _activeObjectComponentForFight.RemoveInteraction("Подняться (2 еденицы времени)", ClimbUpFight);
            _activeObjectComponentForFight.AddInteraction("Спуститься  (2 еденицы времени)", ClimbDownFight);
        }
        private void ClimbDownFight()
        {
            _activeObjectComponentForFight.RemoveInteraction("Спуститься  (2 еденицы времени)", ClimbDownFight);
            _activeObjectComponentForFight.AddInteraction("Подняться  (2 еденицы времени)", ClimbUpFight);
        }
        private void Investigate()
        {
            _subtitleManager.AddSubtitle("Лесница. Выглядит новой.");
        }
        private void ClimbUp()
        {
            eventManager.Invoke("HeroClimbUp");
            _activeObjectComponent.RemoveInteraction("Подняться", ClimbUp);
            _activeObjectComponent.AddInteraction("Спуститься", ClimbDown);
        }
        private void ClimbDown()
        {
            eventManager.Invoke("HeroClimbDown");
            _activeObjectComponent.RemoveInteraction("Спуститься", ClimbDown);
            _activeObjectComponent.AddInteraction("Подняться", ClimbUp);
        }
    }
}
