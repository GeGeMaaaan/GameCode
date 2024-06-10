using Gamee._Manager;
using Gamee._Models;
using Gamee.Manager;
using Gamee;
using System.Collections.Generic;
using System.Diagnostics;
using Gamee.Components;
using System.Linq;
using Gamee._Manager.Screens;
using Gamee._Manager.Dialogs;
using Microsoft.Xna.Framework;
using System;

public class GameManager
{
    private CollisionManager _collisionManager;
    private ActionManager _actionManager;
    private GameObjectManager _gameObjectManager;
    private EventManager _eventManager;
    public ScreenManager _screenManager;
    public SubtitleManager _subtitleManager;
    public GameServiceContainer services;
    private CheckController _checkController;
    private DialogStorage _dialogStorage;
    private FightManager _fightManager;

    public GameManager(GameServiceContainer services)
    {
        this.services = services;
    }

    public void Init()
    {
        _fightManager = new FightManager();
        services.AddService(_fightManager);
        _dialogStorage = new DialogStorage();
        _dialogStorage.LoadDialogs();
        services.AddService(_dialogStorage);
        _subtitleManager = new SubtitleManager();
        services.AddService(_subtitleManager);
        _eventManager = new EventManager();
        services.AddService(_eventManager);
        _gameObjectManager = new GameObjectManager(services);
        services.AddService(_gameObjectManager.GetHero());
        _screenManager = new ScreenManager(services);
        _gameObjectManager.ChangeCurrentScreen(_screenManager.GetScreens()[0]);
        _collisionManager = new CollisionManager(_gameObjectManager.GetHero()); // Создание экземпляра CollisionManager
        _actionManager = new ActionManager(services);
        InventoryComponent inventoryComponent = _gameObjectManager.GetHero().GetComponent<InventoryComponent>();
        _checkController = new CheckController(_gameObjectManager.GetHero().GetComponent<StatsComponent>(), this);
        services.AddService(_checkController);
    }

    public void Update()
    {
        InputManager.Update(_gameObjectManager.GetHero().Position, _gameObjectManager.CurrentScreen.Camera);
        _actionManager.Update(_gameObjectManager.GetActiveObj(),_gameObjectManager.GetActiveUI(), services);
        _collisionManager.HandleCollisions(_gameObjectManager.GetCollidables());
        _gameObjectManager.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin(transformMatrix: _gameObjectManager.CurrentScreen.Camera.Transform);
        _gameObjectManager.Draw();
        Globals.SpriteBatch.End();
        Globals.SpriteBatch.Begin();
        _gameObjectManager.DrawWithoutTransform();
        Globals.SpriteBatch.End();
    }

    
}
