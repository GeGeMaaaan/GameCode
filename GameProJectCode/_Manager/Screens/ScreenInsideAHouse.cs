using Gamee._Models;
using Gamee._Models.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager.Screens
{
    public class ScreenInsideAHouse : Screen
    {
        private bool firstTime = true;
        private bool banditIsHere= false;
        private int floorNumber = 1;
        private GameServiceContainer container;
        private EventManager eventManager;
        private float VerticalOffset = 0;
        public ScreenInsideAHouse(GameServiceContainer services) : base(services)
        {
            container = services;
            eventManager = services.GetService<EventManager>();
            eventManager.Subscribe("BanditIsHere", BanditIsHere);
            eventManager.Subscribe("ThirdFloorClimbUP", EnterThirdFloor);
            eventManager.Subscribe("ThirdFloorClimbDown", EnterSecondFloor);
            eventManager.Subscribe("HeroClimbUp", EnterSecondFloor);
            eventManager.Subscribe("HeroClimbDown", EnterFirstFloor);
            camera = new Camera(1000, 1100, -10000, 1450, VerticalOffset);
            camera.Zoom = 1;
            HeroPos = new Vector2(750, 1550);
            AddSprite(new Background(new Vector2(0, 0), "New sprites/screenInsideAHouse/Background", gameServices), 1);
            AddSprite(new Background(new Vector2(-300, 0), "New sprites/screenInsideAHouse/underBackground", gameServices), 0);
            AddSprite(new Background(new Vector2(0, 0), "New sprites/screenInsideAHouse/ForeGround", gameServices), 100);
            AddSprite(new Dresser(new Vector2(1260, 1660), "New sprites/screenInsideAHouse/Dresser", gameServices), 3);
            AddSprite(new Icon(new Vector2(1488, 1615), "New sprites/screenInsideAHouse/Icon", gameServices), 4);
            AddSprite(new Ladder(new Vector2(200, 1170), "New sprites/screenInsideAHouse/Ladder", gameServices), 2);
            AddSprite(new DoorInHouse1 (new Vector2(290,1050), "New sprites/screenInsideAHouse/DoorInHouse1",gameServices),2);
            AddSprite(new DoorInHouse2(new Vector2(1230, 640), "New sprites/screenInsideAHouse/DoorInHouse1", gameServices), 2);
            AddSprite(new ExitDoor2(new Vector2(1080, 1615), "New sprites/screenInsideAHouse/ExitDoor1", gameServices), 3);
            AddSprite(new ExitDoor1(new Vector2(1685, 1650), "New sprites/screenInsideAHouse/ExitDoor2", gameServices), 3);
            AddSprite(new Chest(new Vector2(1700, 1230), "New sprites/screenInsideAHouse/Chest", gameServices), 3);
            AddSprite(new DoorInBedRoom(new Vector2(362, 630), "New sprites/screenInsideAHouse/DoorInBeadRoom", gameServices), 4);
            CreateGameGrid();

        }
        public override void Update()
        {
            if(banditIsHere&& floorNumber==2)
            {
                VerticalOffset = 400;
                camera.SetVerticalOffset(VerticalOffset); 
                camera.Zoom = 0.9f;
                banditIsHere = false;
                container.GetService<GameObjectManager>().StartDialog("DialogWithBandit");
                AddSprite(new Marauder(new Vector2(1400, 1520), "New sprites/Enemy/marauder", gameServices), 10);
                AddSprite(new Marauder(new Vector2(1000, 1520), "New sprites/Enemy/marauder2", gameServices), 10);
            }
            base.Update();
            if(firstTime==true)
            {
                eventManager.Invoke("EnterAHouseFirstTime");
                firstTime = false;
            }
        }
        public void CreateGameGrid()
        {
            Point start = new Point(150, 1760);
            var LadderUpSkill = new MovementSkill("Подняться по леснице", "Герой поднимается по лестице занимает 2 единцы времени", 0, 2,(5,4),MovementType.absolute);
            var LadderUpDownSkill = new MovementSkill("Спуситься по леснице", "Герой спускается по лестице занимает 2 единцы времени", 0, 2, (3, 0),MovementType.absolute);
            var uniqueCellCoordinate1 = new List<(int, int)>
            {
                (4, 4),
                (5, 4)
            };
            var uniqueCellCoordinate2 = new List<(int, int)>
            {
                (1, 0),
                (2, 0),
                (3, 0)
            };
            int cellSize = 140; 
            Grid = new GameGrid(start.ToVector2(), 12, 5, cellSize);
            Grid.DontDraw(new List<int> { 1, 2, 3 });
            Grid.SetUniqueCellSkill(uniqueCellCoordinate2, new List<Skill> { LadderUpSkill });
            Grid.SetUniqueCellSkill(uniqueCellCoordinate1, new List<Skill> { LadderUpDownSkill });
        }
        private void BanditIsHere()
        {
            banditIsHere = true;
        }
        private void EnterFirstFloor()
        {
            floorNumber = 1;
        }
        private void EnterSecondFloor()
        {
            floorNumber = 2;
        }
        private void EnterThirdFloor()
        {
            floorNumber = 3;
        }




    }
}
