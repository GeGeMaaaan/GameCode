using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.RoomInHouse1
{
    public class ExitRoom2 : StandartActiveSprite
    {
        public ExitRoom2(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Выйти", ExitFroomRoom);
        }
        private void ExitFroomRoom()
        {
            eventManager.Invoke("ExitFromRoom2");
        }
    }
}
