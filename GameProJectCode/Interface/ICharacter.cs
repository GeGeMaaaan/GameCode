using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Interface
{
    public interface ICharacter
    {
        CharacterType Type { get; }
        InventoryComponent InventoryComponent { get; }
        StatsComponent StatsComponent { get; }
        SkillComponent SkillComponent { get; }
        CollisionComponent CollisionComponent { get; }
        (int, int) GridCoordinate { get; }
        void ChangeFightPos(GameGrid grid,int x,int y);
        string Name {  get; }
    }
}
