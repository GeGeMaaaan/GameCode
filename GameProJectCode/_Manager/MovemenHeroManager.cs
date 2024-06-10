using Gamee._Manager;
using Gamee._Models;
using Gamee.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class MovementHeroManager
{
    public Vector2 _targetPos = Vector2.Zero;
    private bool _canMoveLeft = false;
    private bool _canMoveRight = false;
    private const float ClickThreshold = 40;
    private Vector2 _movementDirection = Vector2.Zero;
    private bool canMove = true;
    private bool _mouseWasPressed = false;
    public bool needToDrawFlag = false;
    private EventManager eventManager;
    private GameServiceContainer gameServiceContainer;
    public bool _isTurnBasedMode { get; private set; } = false;
    private GameGrid _gameGrid; 

    public MovementHeroManager(GameServiceContainer serviceContainer)
    {
        gameServiceContainer = serviceContainer;
        eventManager = serviceContainer.GetService<EventManager>();
        eventManager.Subscribe("StartDialog", DisableMovement);
        eventManager.Subscribe("DialogEnd", EnableMovement);
        eventManager.Subscribe("StartFight", DisableMovement);
        eventManager.Subscribe("EndFight", EnableMovement);
    }

    public void EnterTurnBasedMode()
    {
        _isTurnBasedMode = true;
        _gameGrid = gameServiceContainer.GetService<GameObjectManager>().CurrentScreen.Grid;
        Vector2 heroPos = gameServiceContainer.GetService<Hero>().Position;
        if (_gameGrid == null)
        {
            throw new Exception("Не заданно поле для экрана");
        }
        
    }
    public void ExitTurnBasedMode()
    {
        _isTurnBasedMode = false;
    }

    public void Update(MouseState mouseState, KeyboardState keyboardState, Vector2 heroPos)
    {
        if (canMove)
        {
            _movementDirection = Vector2.Zero;
            if (_isTurnBasedMode)
            {
                UpdateTurnBasedMovement(mouseState, heroPos);
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.D))
                {
                    UpdateKeyboardInput(keyboardState);
                }
                else
                {
                    UpdateMovementDirection(mouseState, new Vector2(heroPos.X + 48, heroPos.Y));
                }
            }
        }
        else
            _movementDirection = Vector2.Zero;
    }

    private void UpdateTurnBasedMovement(MouseState mouseState, Vector2 heroPos)
    {
        if (mouseState.RightButton == ButtonState.Pressed && !_mouseWasPressed)
        {
            _mouseWasPressed = true;
            _canMoveLeft = true;
            _canMoveRight = true;
            // Получаем позицию мыши
            Vector2 mousePosition = InputManager.MousePositionWorld;

            // Получаем клетку по позиции мыши
            var targetCell = _gameGrid?.GetCellByPosition(mousePosition);

            if (targetCell != null)
            {
                _targetPos = new Vector2(targetCell.Position.X-30,heroPos.Y);
            }
        }
        else if (mouseState.RightButton == ButtonState.Released)
        {
            _mouseWasPressed = false;
        }
        if (heroPos == _targetPos)
        {
            _targetPos.X = heroPos.X;
            _targetPos.Y = 0;
            _canMoveLeft = false;
            _canMoveRight = false;
        }
        else
        {
            if (_targetPos != Vector2.Zero)
            {
                if (_targetPos.X < heroPos.X && _canMoveLeft)
                {
                    _movementDirection.X--;
                }
                else if (_targetPos.X > heroPos.X && _canMoveRight)
                {
                    _movementDirection.X++;
                }
            }
        }
    }

    private void UpdateMovementDirection(MouseState mouseState, Vector2 heroPos)
    {
        if (mouseState.RightButton == ButtonState.Pressed && !_mouseWasPressed)
        {
            _targetPos = InputManager.MousePositionWorld;
            _canMoveLeft = true;
            _canMoveRight = true;
            _mouseWasPressed = true;
        }
        else if (mouseState.RightButton == ButtonState.Released)
        {
            _mouseWasPressed = false;
        }

        if (Math.Abs(_targetPos.X - heroPos.X + 32) < ClickThreshold)
        {
            _targetPos.X = heroPos.X;
            _targetPos.Y = 0;
            _canMoveLeft = false;
            _canMoveRight = false;
            needToDrawFlag = false;
        }
        else
        {
            if (_targetPos != Vector2.Zero)
            {
                if (_targetPos.X < heroPos.X && _canMoveLeft)
                {
                    _movementDirection.X--;
                    needToDrawFlag = true;
                }
                else if (_targetPos.X > heroPos.X && _canMoveRight)
                {
                    _movementDirection.X++;
                    needToDrawFlag = true;
                }
                else needToDrawFlag = false;
            }
        }
    }

    private void UpdateKeyboardInput(KeyboardState keyboardState)
    {
        needToDrawFlag = false;
        _canMoveLeft = false;
        _canMoveRight = false;
        if (keyboardState.IsKeyDown(Keys.A)) _movementDirection.X--;
        else if (keyboardState.IsKeyDown(Keys.D)) _movementDirection.X++;
    }

    public void CollideLeft()
    {
        _canMoveLeft = false;
    }

    public void CollideRight()
    {
        _canMoveRight = false;
    }

    private void DisableMovement()
    {
        canMove = false;
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    public bool Moving => _movementDirection != Vector2.Zero;
    public Vector2 Direction => _movementDirection;
    public bool NeedToMove => _canMoveLeft && _canMoveRight;
    public Vector2 ClickPosition => _targetPos;
}

