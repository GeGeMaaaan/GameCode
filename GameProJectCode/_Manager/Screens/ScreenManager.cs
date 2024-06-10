using Gamee._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager.Screens
{
    
    public class ScreenManager
    {
        private List<Screen> _screens = new List<Screen>();
        private GameServiceContainer _container;
        public ScreenManager(GameServiceContainer services)
        {
            _container = services;
            _screens.Add(new ScreenMainMenu(services));
            _screens.Add(new ScreenStartLocation(services));
            _screens.Add(new ScreenInsideAHouse(services));
            _screens.Add(new ScreenRoomInHouse1(services));
            _screens.Add(new ScreenRoomInHouse2(services));
            services.GetService<EventManager>().Subscribe("StartGame",StartGame);
            services.GetService<EventManager>().Subscribe("OpenDoor", OpenDoor);
            services.GetService<EventManager>().Subscribe("InWorkRoom", EnterWorkRoom);
            services.GetService<EventManager>().Subscribe("ExitFromRoom1", ExitWorkRoom);
            services.GetService<EventManager>().Subscribe("ExitFromHouse", ExitFromHouse);
            services.GetService<EventManager>().Subscribe("OpenDoorInBedRoom", EnterBedRoom);
            services.GetService<EventManager>().Subscribe("ExitFromRoom2", ExitBedRoom);
            services.GetService<EventManager>().Subscribe("GameWon", ExitBedRoom);
            services.GetService<EventManager>().Subscribe("GameOver", ExitBedRoom);
        }
        public List<Screen> GetScreens()
        {
            return _screens;
        }
        public void StartGame()
        {
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[1]);
            _container.GetService<GameObjectManager>().StartDialog("FirstDialog");
        }
        public void OpenDoor()
        {
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[2]); 
        }
        public void EnterWorkRoom()
        {
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[3]);
        }
        public void ExitWorkRoom()
        {
            _screens[2].HeroPos = new Vector2(290, 1000);
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[2]);
        }
        public void ExitFromHouse()
        {
            _screens[1].HeroPos = new Vector2(1000, 850);
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[1]);
        }
        public void EnterBedRoom()
        {
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[4]);
        }
        public void ExitBedRoom()
        {
            _screens[2].HeroPos = new Vector2(362, 580);
            _container.GetService<GameObjectManager>().ChangeCurrentScreen(_screens[2]);
        }
        
    }
}
