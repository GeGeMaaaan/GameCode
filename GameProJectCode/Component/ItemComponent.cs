using Gamee._Models;
using Gamee.Components;
using Gamee.Interface;
using Gamee.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemComponent : ActiveObjectComponent
{
    private Item _item { get; set; }
    public ItemComponent(Sprite owner,Item item , ActiveType activeType) : base(owner,activeType)
    {
        _item = item;
    }
   
    public Item Item => _item;
}


