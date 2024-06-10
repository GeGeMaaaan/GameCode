using Gamee._Manager;
using Gamee.Manager;

namespace Gamee._Models.Components
{
    public class MovementComponent : Component
    {
        private readonly float _speed = 300f;
        private bool _canMoveLeft = true;
        private bool _canMoveRight = true;
        private MovementHeroManager _movementHeroManager;
        public MovementComponent(Sprite owner,MovementHeroManager movementHeroManager) : base(owner)
        {
            _movementHeroManager = movementHeroManager;
            _owner = owner;
        }

        public bool CanMoveLeft
        {
            get { return _canMoveLeft; }
            set { _canMoveLeft = value; }
        }

        public bool CanMoveRight
        {
            get { return _canMoveRight; }
            set { _canMoveRight = value; }
        }

        public override void Update()
        {
            if (_movementHeroManager.Moving)
            {
                if (_movementHeroManager._isTurnBasedMode)
                {
                    _owner.Position = _movementHeroManager._targetPos;
                }
                else
                {
                    if (_movementHeroManager.Direction.X < 0 && _canMoveLeft)
                    {
                        _owner.Position += Vector2.Normalize(_movementHeroManager.Direction) * _speed * Globals.TotalSeconds;
                    }
                    else if (_movementHeroManager.Direction.X > 0 && _canMoveRight)
                    {
                        _owner.Position += Vector2.Normalize(_movementHeroManager.Direction) * _speed * Globals.TotalSeconds;
                    }
                }
            }
        }

        public override void Draw()
        {

        }
    }
}