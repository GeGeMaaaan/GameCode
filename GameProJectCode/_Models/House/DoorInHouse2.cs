using Gamee._Manager;
using Gamee.Interface;
using Gamee.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.House
{

    public class DoorInHouse2 : StandartActiveSprite
    {
        public DoorInHouse2(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Спусится вниз", ClimbDown);
            _activeObjectComponent.AddInteraction("Пройти в комнату", GoToRoom);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
        }
        public void ClimbDown()
        {
            eventManager.Invoke("ThirdFloorClimbDown");
        }
        public void GoToRoom()
        {
            eventManager.Invoke("InWorkRoom");
        }
        public void Investigate()
        {
            _subtitleManager.AddSubtitle("Проход в коридор. В нём видно лестницу наверх и дверь в комнату");
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
    }



}
