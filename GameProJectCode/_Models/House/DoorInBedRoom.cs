using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.House
{
    internal class DoorInBedRoom : StandartActiveSprite
    {
        public DoorInBedRoom(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Осмотерть", Investigate);
            _activeObjectComponent.AddInteraction("Открыть", Open);
        }
        public void Investigate()
        {
            _subtitleManager.AddSubtitle("Дверь в спалню. За ней слышен храп. По-видимому мой друг там");
        }
        public void Open()
        {
            eventManager.Invoke("OpenDoorInBedRoom");
        }
    }
}
