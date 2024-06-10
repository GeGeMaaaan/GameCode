using Gamee._Manager;
using Gamee.Interface;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Gamee._Models.House
{
    public class MainDoor : StandartActiveSprite, ICollidable
    {

        private Texture2D _activeTexture;
        private bool IsOpen = false;
        private Hero _hero;
        public MainDoor(Vector2 position, string textureName, GameServiceContainer services) : base(position, textureName, services)
        {

            _hero = services.GetService<Hero>();
            _activeObjectComponent.AddInteraction("Открыть", OpenDoor);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);


        }
        private void OpenDoor()
        {
            if (!IsOpen)
            {
                _subtitleManager.AddSubtitle("Заперто");
            }

        }
        private void Investigate() 
        {
            _subtitleManager.AddSubtitle("Я хорошо помню эту дверь из дества. Хм-м. На двери виднеются вмятены и небольшие проломы, как буддто кто-то хотел выбить дверь");
        }
        
        public CollisionComponent CollisionComponent => _collisionComponent;

        public ActiveObjectComponent ActiveObjectComponent => _activeObjectComponent;
    }
}
