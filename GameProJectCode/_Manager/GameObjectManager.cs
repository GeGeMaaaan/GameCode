using Gamee._Models.House;
using Gamee._Models;
using Gamee.Interface;
using System.Collections.Generic;
using System.Linq;
using Gamee._Manager.Dialogs;
using System.Collections;
using Gamee._Manager.Screens;
using System.ComponentModel.Design;

namespace Gamee._Manager
{
    public class GameObjectManager
    {
        private Hero _hero;
        private List<InteractionMenuManager> _interactionMenuManagers;
        private List<InventoryManager> _inventoryManagers;
        private FightManager _fightManager;
        private SubtitleManager SubtitleManager;
        private DialogManager DialogManager;
        private bool IsDialog = false;
        private bool IsFight = false;
        private EventManager EventManager;
        private Screen currentScreen;
        private GameServiceContainer services;
        private Camera camera;
        private bool isTurnBasedMode = false;
        public GameObjectManager(GameServiceContainer serviceContainer)
        {

            _fightManager = serviceContainer.GetService<FightManager>();
            services = serviceContainer;
            serviceContainer.AddService(this);
            EventManager = services.GetService<EventManager>();
            SubtitleManager = services.GetService<SubtitleManager>();
            _inventoryManagers = new List<InventoryManager>();
            _interactionMenuManagers = new List<InteractionMenuManager>();
            CreateHero();
            EventManager.Subscribe("DialogEnd", DialogEnd);
            EventManager.Subscribe("EnterTurnBasedMode", EnterTurnBasedMode);
            EventManager.Subscribe("StartFight1", StartFight1);
            EventManager.Subscribe("EndFight", EndFight);
        }
        public void Update()
        {

            if (currentScreen != null)
            {
                currentScreen.Update();
                currentScreen.UpdateUI();
            }
            if (currentScreen.DoSomething)
            {
                camera.HandleInput(_hero.Position, _hero.Width);

                
                
                    foreach (var interactionMenu in _interactionMenuManagers)
                    {
                        interactionMenu.Update();
                    }

                    foreach (var inventoryManager in _inventoryManagers)
                    {
                        inventoryManager.Update();
                    }

                
                SubtitleManager.Update();

            }
            if (IsDialog)
            {
                DialogManager.Update();
            }
            if (IsFight)
            {
                _fightManager.Update(Globals.TotalSeconds);
            }
            camera.Follow();
        }

        public void Draw()
        {
            if (currentScreen != null)
            {
                currentScreen.Draw();
            }
            if (currentScreen.DoSomething)
            {
                foreach (var interactionMenu in _interactionMenuManagers)
                {
                    interactionMenu.Draw();
                }
            }
            if (IsFight)
            {
                _fightManager.DrawGrid();
            }
        }
        public void DrawWithoutTransform()
        {
            if (currentScreen != null)
            {
                currentScreen.DrawUI();
            }
            if (currentScreen.DoSomething)
            {
                foreach (var inventoryManager in _inventoryManagers)
                {
                    inventoryManager.Draw();
                }
            }

            if (IsDialog)
            {

                DialogManager.Draw();

            }
            if(IsFight)
            {
                _fightManager.DrawUI();
            }
            SubtitleManager.Draw();
        }
        private void CreateHero()
        {
            _hero = new Hero(new Vector2(300, Globals.floarLevel - 100), "New sprites/screeStartLocation/PlayerBase", services);

        }
        public void AddSprite(Sprite sprite)
        {
            if (currentScreen != null)
            {
                currentScreen.AddSprite(sprite);
            }
        }
        public void RemoveSprite(Sprite sprite)
        {
            if (currentScreen != null)
            {
                currentScreen.RemoveSprite(sprite);
            }
        }
        public void AddInventoryManager(InventoryManager inventoryManager)
        {
            _inventoryManagers.Add(inventoryManager);
        }

        public void AddInteractionMenu(InteractionMenuManager interactionMenu)
        {
            _interactionMenuManagers.Add(interactionMenu);
        }
        public void ChangeCurrentScreen(Screen screen)
        {
            camera = screen.Camera;
            currentScreen = screen;
            _hero.Position = screen.HeroPos;
            screen.RemoveSprite(_hero);
            if (currentScreen.DoSomething)
            {
                screen.AddSprite(_hero, 50);
            }
            _interactionMenuManagers = currentScreen.InteractionMenuManagers;
            _inventoryManagers = currentScreen.InventoryManagers;
        }
        public Hero GetHero()
        {
            return _hero;
        }
        public List<Sprite> GetActiveObj()
        {
            return currentScreen.GetActiveObj();
        }
        public List<Sprite> GetActiveUI()
        {
            return currentScreen.GetActiveUI();
        }
        public List<Sprite> GetGameObjects()
        {
            return currentScreen.GetObj();
        }
        

        public List<Sprite> GetCollidables()
        {
            return currentScreen.GetColligableObj();
        }

        public void StartDialog(string id)
        {
            DialogManager = new DialogManager(services.GetService<DialogStorage>().GetDialog(id), services);
            IsDialog = true;
            EventManager.Invoke("StartDialog");
        }
        public void DialogEnd()
        {
            IsDialog = false;
        }
        public void EnterTurnBasedMode()
        {
            isTurnBasedMode = true;
        }
        public void StartFight1()
        {
            _fightManager.Initialize(_hero, currentScreen.Grid,services);
            EventManager.Invoke("StartFight");
            int pos = 8;
            foreach (var obj in currentScreen.GetObj())
            {
               
                if (obj is ICharacter&&!(obj is Hero))
                {
                    _fightManager.AddCombatant(obj as ICharacter, pos, 0);
                    pos =pos- 2;
                }
            }

             IsFight = true; // Устанавливает флаг боя
        }
        public void EndFight()
        {
            _fightManager = new FightManager();
            IsFight = false;
        }
        public Screen CurrentScreen => currentScreen;
    }
}
