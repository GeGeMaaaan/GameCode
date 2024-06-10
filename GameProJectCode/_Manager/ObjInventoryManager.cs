using Gamee._Models;
using Gamee.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager
{
    public class ObjInventoryManager:InventoryManager
    {
        public ObjInventoryManager(InventoryComponent inventory,Texture2D texture):base(inventory,texture)
        { 
            ChangeItemInRow(5);
            _position = new Vector2(980, 540);
        }
        public override void Update()
        {
            BasicUpdate();
        }
        public override void Draw()
        {
            BasicDraw();
        }
    }
}
