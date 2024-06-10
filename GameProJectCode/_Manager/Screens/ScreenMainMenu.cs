using Gamee._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager.Screens
{
    public class ScreenMainMenu : Screen
    {
        private GameServiceContainer _container;
        private Background background;
        private Button button;
        public ScreenMainMenu(GameServiceContainer services) : base(services)
        {
            camera = new Camera(-2000, 2000, -2000, 2000, 0);
            CanDoSomething = false;
            button = new Button(new Vector2(500,500), "PlayButton", gameServices);
            AddSprite(new Background(new Vector2(-960, -550), "MainMenuBackGround", gameServices), 1);
            AddUI(button, 2);
        }
        public void StartGame()
        {
            _container.GetService<EventManager>().Invoke("StartGame");
        }
    }
}
