using Gamee._Models.House;
using Gamee._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamee._Models.RoomInHouse1;

namespace Gamee._Manager.Screens
{
    public class ScreenRoomInHouse1 : Screen
    {
        public ScreenRoomInHouse1(GameServiceContainer services) : base(services)
        {
            camera = new Camera(800, 800, -10000, 14500, 50);
            camera.Zoom = 1.5f;
            HeroPos = new Vector2(750, 700);
            AddSprite(new Background(new Vector2(0, 0), "New sprites/workCabinet/BackGroud", gameServices), 1);
            AddSprite(new ExitRoom1(new Vector2(750, 1040), "New sprites/workCabinet/Exit", gameServices), 5);
        }
    }

}