using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models
{
    public class ButtonTurnBasedMode : StandartActiveSprite
    {
        public ButtonTurnBasedMode(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.ActiveType = ActiveType.OnlyClick;
            _activeObjectComponent.AddInteraction("Перейти в пошаговый режим",EnterTurnBasedMode);
        }
        public void EnterTurnBasedMode()
        {
            eventManager.Invoke("EnterTurnBasedMode");
        }
    }
}
