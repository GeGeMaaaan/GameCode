using Gamee._Models;
using Gamee.Interface;
using Gamee.Manager;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Gamee._Manager.Screens
{
    public class Screen
    {
        protected Dictionary<Sprite, int> _gameObjects;
        protected GameServiceContainer gameServices;
        protected Camera camera;
        protected List<Sprite> activeSprites;
        protected Dictionary<Sprite, int> UI_Sprites;
        protected List<InventoryManager> inventoryManagers;
        protected List<InteractionMenuManager> interactionManagers;
        protected bool CanDoSomething = true;
        public Vector2 HeroPos;
        protected EventManager eventManager;
        public GameGrid Grid { get; protected set; }

        public Screen(GameServiceContainer services)
        {
            eventManager = services.GetService<EventManager>();
            activeSprites = new List<Sprite>();
            inventoryManagers = new List<InventoryManager>();
            interactionManagers = new List<InteractionMenuManager>();
            gameServices = services;
            _gameObjects = new Dictionary<Sprite, int>();
            UI_Sprites = new Dictionary<Sprite, int>();
        }

        public void CreateBaseGrid(Point start, int width,int height, int cellSize)
        {
            Grid = new GameGrid(start.ToVector2(),width, height, cellSize);
        }

        public void AddSprite(Sprite sprite, int order)
        {
            _gameObjects.Add(sprite, order);
        }

        public void AddUI(Sprite sprite, int order)
        {
            UI_Sprites.Add(sprite, order);
        }

        public void AddSprite(Sprite sprite)
        {
            int order = _gameObjects.Values.Any() ? _gameObjects.Values.Max() + 1 : 1;
            _gameObjects.Add(sprite, order);
        }

        public void RemoveSprite(Sprite sprite)
        {
            _gameObjects.Remove(sprite);
        }

        public virtual void Update()
        {
            inventoryManagers.Clear();
            interactionManagers.Clear();
            foreach (var gameObject in _gameObjects.Keys)
            {
                gameObject.Update();
                if (gameObject is IActive)
                {
                    interactionManagers.Add((gameObject as IActive).ActiveObjectComponent.menuManager);
                    interactionManagers.Add((gameObject as IActive).ActiveObjectComponentForFight.menuManager);
                }
                if (gameObject is IInventory)
                {
                    inventoryManagers.Add((gameObject as IInventory).InventoryManager);
                }
            }
        }

        public virtual void UpdateUI()
        {
            foreach (var UI in UI_Sprites.Keys)
            {
                UI.Update();
                if (UI is IActive)
                {
                    interactionManagers.Add((UI as IActive).ActiveObjectComponent.menuManager);
                }
            }
        }

        public void Draw()
        {
            var sortedDictionary = _gameObjects.OrderBy(x => x.Value).ToDictionary();
            foreach (var gameObject in sortedDictionary.Keys)
            {
                if (gameObject is IActive && InputManager.KeyboardState.IsKeyDown(Keys.H))
                {
                    (gameObject as IActive).DrawActive();
                }
                else
                {
                    gameObject.Draw();
                }
            }
        }

        public void DrawUI()
        {
            var sortedDictionary = UI_Sprites.OrderBy(x => x.Value).ToDictionary();
            foreach (var UI in sortedDictionary.Keys)
            {
                UI.Draw();
            }
        }

        public List<Sprite> GetColligableObj()
        {
            var list = new List<Sprite>();
            foreach (var gameObject in _gameObjects.Keys)
            {
                if (gameObject is ICollidable)
                {
                    list.Add(gameObject);
                }
            }
            return list;
        }

        public List<Sprite> GetActiveObj()
        {
            var list = new List<Sprite>();
            foreach (var gameObject in _gameObjects.Keys)
            {
                if (gameObject is IActive)
                {
                    list.Add(gameObject);
                }
            }
            return list;
        }

        public List<Sprite> GetActiveUI()
        {
            var list = new List<Sprite>();
            foreach (var UI in UI_Sprites.Keys)
            {
                if (UI is IActive)
                {
                    list.Add(UI);
                }
            }
            return list;
        }

        public List<Sprite> GetObj()
        {
            var list = new List<Sprite>();
            foreach (var gameObject in _gameObjects.Keys)
            {
                list.Add(gameObject);
            }
            return list;
        }

        public List<Sprite> GetUI()
        {
            var list = new List<Sprite>();
            foreach (var UI in UI_Sprites.Keys)
            {
                list.Add(UI);
            }
            return list;
        }

        public bool DoSomething => CanDoSomething;
        public Camera Camera => camera;
        public List<InventoryManager> InventoryManagers => inventoryManagers;
        public List<InteractionMenuManager> InteractionMenuManagers => interactionManagers;
    }
}
