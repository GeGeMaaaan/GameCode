using Gamee._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Interface
{
    public interface IActive
    {
        ActiveObjectComponent ActiveObjectComponentForFight { get; }
        ActiveObjectComponent ActiveObjectComponent { get; }
        CollisionComponent CollisionComponent { get; }
        void DrawActive();
    }
}
