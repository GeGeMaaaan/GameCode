using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.RoomInHouse2
{
    public class Alex : StandartActiveSprite
    {
        private readonly SpeakComponent speakComponent;
        public Alex(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            RemoveComponent(_activeObjectComponent);
            _activeObjectComponent = new SpeakComponent(this, ActiveType.NearPlayer);
            (_activeObjectComponent as SpeakComponent).AddDialog("DialogWithAlex1");
            AddComponent(_activeObjectComponent);
        }
       
    }
}
