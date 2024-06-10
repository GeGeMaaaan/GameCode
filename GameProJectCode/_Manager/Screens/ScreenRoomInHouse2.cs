using Gamee._Models.House;
using Gamee._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamee._Models.RoomInHouse1;
using Gamee._Models.RoomInHouse2;

namespace Gamee._Manager.Screens
{
    public class ScreenRoomInHouse2 : Screen
    {
        public ScreenRoomInHouse2(GameServiceContainer services) : base(services)
        {
            camera = new Camera(800, 800, -10000, 14500, 0);
            camera.Zoom = 1.4f;
            HeroPos = new Vector2(750, 700);
            AddSprite(new Background(new Vector2(0, 0), "New sprites/beadRoom/Background", gameServices), 1);
            AddSprite(new ExitRoom2(new Vector2(750, 1000), "New sprites/beadRoom/Exit", gameServices), 5);
            AddSprite(new Background(new Vector2(0,0), "New sprites/beadRoom/Foreground", gameServices), 3);
            AddSprite(new Alex(new Vector2(400, 700), "New sprites/beadRoom/Алексей", gameServices), 2);
        }
    }

}