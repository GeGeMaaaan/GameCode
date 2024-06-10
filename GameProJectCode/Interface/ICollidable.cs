using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee.Interface
{
    public interface ICollidable
    {
        CollisionComponent CollisionComponent { get; } // Обязательное поле типа CollisionComponent
    }
}
