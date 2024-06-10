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

    public class ExitDoor1 : StandartActiveSprite
    {
        public ExitDoor1(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Выйти", ExitFromHouse);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
        }
        public void ExitFromHouse()
        {
            eventManager.Invoke("ExitFromHouse");
        }
        public void GoToRoom()
        {
            eventManager.Invoke("InWorkRoom");
        }
        public void Investigate()
        {
            _subtitleManager.AddSubtitle("Дверь которую я открыл");
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
    }



}
