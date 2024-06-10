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

    public class ExitDoor2 : StandartActiveSprite
    {
        public ExitDoor2(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Открыть замок", UnlockTheDoor);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
        }
        public void UnlockTheDoor()
        {
            _activeObjectComponent.RemoveInteraction("Открыть замок", UnlockTheDoor);
            _activeObjectComponent.AddInteraction("Выйти", ExitFromHouse);
        }
        public void ExitFromHouse()
        {
            eventManager.Invoke("ExitFromHouse");
        }
        public void Investigate()
        {
            _subtitleManager.AddSubtitle("Основная дверь в дом");
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
