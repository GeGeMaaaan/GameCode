using Gamee._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.House
{
    public class Icon : StandartActiveSprite
    {
        public Icon(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
        }
        private void Investigate()
        {
            _subtitleManager.AddSubtitle("Старые стелянные иконы. Я любил их разгядывать в дестве. Алексей рассказывал, что они передаются из поколения в поколеня уже больше сотни лет");
        }
    }
}
