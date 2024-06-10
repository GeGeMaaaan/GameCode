using Gamee._Models;
using Gamee._Models.House;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gamee._Manager.Screens
{
    public class ScreenStartLocation : Screen
    {
        private bool firstTime = true;
        private Hero hero;
        public ScreenStartLocation(GameServiceContainer services) : base(services)
        {
            hero = services.GetService<Hero>();
            camera = new Camera(1600, 8400, -10000, 700, -400);
            camera.Zoom = 0.6f;
            HeroPos = new Vector2(8000, 850);
            AddSprite(new Background(new Vector2(0, -600), "New sprites/screeStartLocation/Background", gameServices), 1);
            AddSprite(new Horse(new Vector2(8000, 600), "New sprites/screeStartLocation/Horse", gameServices), 2);
            CreateHouse();
            CreateGameGrid();
        }

        private void CreateHouse()
        {
            Vector2 housePos = new Vector2(70, -580);
            Vector2 MainDoorPos = new Vector2(housePos.X + 2220, housePos.Y + 1335);
            Vector2 SecondDoorPos = new Vector2(housePos.X + 1080, housePos.Y + 1360);
            AddSprite(new House(housePos, "New sprites/screeStartLocation/HouseV2", gameServices), 5);
            AddSprite(new MainDoor(MainDoorPos, "New sprites/screeStartLocation/Door1", gameServices), 6);
            AddSprite(new SecondDoor(SecondDoorPos, "New sprites/screeStartLocation/Door2", gameServices), 6);
           
        }

        public override void Update()
        {
            base.Update();
            PlayerNearHouse();
        }

        private void PlayerNearHouse()
        {
            if (hero.Position.X < 3500 && firstTime == true)
            {
                eventManager.Invoke("PlayerNearHouse");
                firstTime = false;
            }
        }

        public void CreateGameGrid()
        {
            Point start = new Point(0, 1100); // Начальная точка сетки
            
            int cellSize = 150; // Размер клетки

            base.CreateBaseGrid(start, 60,1, cellSize);
        }
    }
}
