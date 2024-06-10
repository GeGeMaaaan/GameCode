using Gamee._Manager;
using Gamee._Models.Components;
using Gamee.Components;
using Gamee.Interface;
using Gamee.Manager;
using System.Collections.Generic;

namespace Gamee._Models
{
    public class Hero : Sprite, ICollidable, IInventory,ICharacter
    {
        private readonly MovementComponent _movementComponent;
        private readonly AnimationComponent _animationComponent;
        private readonly CollisionComponent _collisionComponent;
        private readonly InventoryComponent _inventoryComponent;
        private readonly SkillComponent _skillComponent;
        public MovementHeroManager movementHeroManager;
        private StatsComponent _statsComponent;
        private HeroInventoryManager inventoryManager;
        public (int,int) gridCoordinate;
        public Dictionary<object, Animation> _animations = new Dictionary<object, Animation>();
        private bool CanMove = true;
        public SubtitleManager subtitleManager { get; private set; }
        private HeroEvent events;
        public Hero(Vector2 position, string textureName, GameServiceContainer services) : base(position, textureName, services)
        {
            subtitleManager = services.GetService<SubtitleManager>();
            events = new HeroEvent(this);
            movementHeroManager = new MovementHeroManager(services);
            _animations.Add(Vector2.UnitX * -1, new Animation( Globals.Content.Load<Texture2D>("PlayerAnimationRun"), 1, 2, 0.1f, 1));
            _animations.Add(Vector2.UnitX, new Animation( Globals.Content.Load<Texture2D>("PlayerAnimationRun"), 1, 2, 0.1f, 2));
            _movementComponent = new MovementComponent(this, movementHeroManager);
            _statsComponent = new StatsComponent(this,services);
            _animationComponent = new AnimationComponent(this, _animations, movementHeroManager);
            _collisionComponent = new CollisionComponent(this, CollisionType.None, false);
            _inventoryComponent = new InventoryComponent(this, 49, _statsComponent);
            inventoryManager = new HeroInventoryManager(_inventoryComponent, Globals.Content.Load<Texture2D>("InteractionMenuBase"));
            _skillComponent = new SkillComponent(this, services);
            _skillComponent.AddSkill(new MovementSkill("Подкат влево", "Перемещает на 3 клеки влево, требует 2 единицы времени", 3, 2, (-3, 0), MovementType.relative));
            _skillComponent.AddSkill(new MovementSkill("Подкат вправо", "Перемещает на 3 клеки вправо, требует 2 единицы времени", 3, 2, (3, 0), MovementType.relative));
            AddComponent(_inventoryComponent);
            AddComponent(_movementComponent);
            AddComponent(_animationComponent);
            AddComponent(_collisionComponent);
            AddComponent(_skillComponent);
            AddComponent(_statsComponent);
            foreach (var item in events.events)
            {
                eventManager.Subscribe(item.Key,item.Value);
            }

        }
        public override void Update()
        {
            if (CanMove)
                movementHeroManager.Update(InputManager.MouseState, InputManager.KeyboardState, Position);
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        public override void Draw()
        {
            foreach (var component in _components)
            {
                component.Draw();
            }
        }

        public void ChangeFightPos(GameGrid grid, int x, int y)
        {
            var cellPos = grid.GetCellPos(x, y);
            Position = new Vector2(cellPos.X,cellPos.Y-210);
            gridCoordinate = (x, y);
        }

        public CollisionComponent CollisionComponent => _collisionComponent;
        public InventoryComponent InventoryComponent => _inventoryComponent;
        public StatsComponent StatsComponent => _statsComponent;
        public InventoryManager InventoryManager => inventoryManager;
        public SkillComponent SkillComponent => _skillComponent;

        public (int, int)GridCoordinate => gridCoordinate;
        public string Name => "Player";

        public CharacterType Type =>CharacterType.hero;
    }
}