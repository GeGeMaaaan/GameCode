using Gamee;
using Gamee._Manager;
using Gamee._Models;
using Gamee._Models.Components;
using Gamee.Components;
using Gamee.Interface;
using Gamee.Items;
using Gamee.Manager;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class HeroInventoryManager:InventoryManager
{
    public HeroInventoryManager(InventoryComponent inventoryComponent, Texture2D texture) :base(inventoryComponent, texture)
    {
        ChangeItemInRow(7);
        
    }
    public override void  Update()
    {
        _position = new Vector2(960,400);
        if (InputManager.WasKeyPressed(Keys.I))
        {
            _inventoryComponent.ToggleInventoryVisibility();
        }
        BasicUpdate();
    }

    public override void Draw()
    {
        BasicDraw();
    }
    
   
}