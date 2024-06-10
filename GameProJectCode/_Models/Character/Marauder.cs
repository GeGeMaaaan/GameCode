using Gamee._Models;
using Gamee.Interface;
using Gamee;
using System.Collections.Generic;
using Gamee.Components;

public class Marauder : Sprite,ICharacter
{
    private readonly InventoryComponent _inventoryComponent;
    private readonly AnimationComponent _animationComponent;
    private readonly CollisionComponent collisionComponent;
    private readonly SkillComponent _skillComponent;
    private RenderComponent _renderComponent;
    private StatsComponent _statsComponent;
    private (int, int) gridCoordinate;
    private Dictionary<MainStats, int> stats;
    public Marauder(Vector2 position, string textureName, GameServiceContainer services) : base(position, textureName, services)
    {
        collisionComponent = new CollisionComponent(this, CollisionType.None, false);
        stats = new Dictionary<MainStats, int>();
        stats[MainStats.dexterity] = 3;
        stats[MainStats.strength] = 4;
        stats[MainStats.intelligence] = 2;
        stats[MainStats.gun_mastery] = 5;
        stats[MainStats.medicine] = 3;
        stats[MainStats.diplomacy] = 3;
        _statsComponent = new StatsComponent(this,services);
        _statsComponent.SetBaseStats(20, 5, 2, stats, 0);
        _renderComponent = new RenderComponent(this);    
        _inventoryComponent = new InventoryComponent(this, 10, _statsComponent);
        _skillComponent = new SkillComponent(this, services);
        AddComponent(_renderComponent);
        AddComponent(_statsComponent);
        AddComponent(_skillComponent);
    }
    public override void Update()
    {
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
    public void DrawActive()
    {
        foreach (var component in _components)
        {
            if (component is RenderComponent)
            {
                (component as RenderComponent).DrawActive();
            }
            else
            {
                component.Draw();
            }
        }
    }

    public void ChangeFightPos(GameGrid grid, int x, int y)
    {
        var cellPos = grid.GetCellPos(x, y);
        gridCoordinate = (x, y);
        Position = new Vector2(cellPos.X- 100 , cellPos.Y - 230);
    }


    public StatsComponent StatsComponent => _statsComponent;

    public SkillComponent SkillComponent =>_skillComponent;

    public  (int, int) GridCoordinate => gridCoordinate;
    public string Name => "Marauder";

    public InventoryComponent InventoryComponent => _inventoryComponent;

    public CollisionComponent CollisionComponent => collisionComponent;

    public CharacterType Type => CharacterType.enemy;
}