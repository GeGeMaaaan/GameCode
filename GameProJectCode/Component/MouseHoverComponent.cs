using Gamee;
using Gamee._Models;
using Gamee.Manager;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices.Marshalling;
public class MouseHoverComponent : Component
{
    private Rectangle _boundingBox;
    private bool _isHovered;
    private Vector2 mouseState; // Добавляем поле для хранения позиции мыши

    public MouseHoverComponent(Sprite owner) : base(owner)
    {
        UpdateBoundingBox();
    }
    private void UpdateBoundingBox()
    {
        _boundingBox = new Rectangle((int)_owner.Position.X, (int)_owner.Position.Y, _owner.Width, _owner.Height);
    }
    public override void Update()
    {
        UpdateBoundingBox();
        // Инициализируем поле позиции мыши
        mouseState = InputManager.MousePositionWorld;
        Rectangle mouseRectangle = new Rectangle((int)mouseState.X, (int)mouseState.Y, 1, 1);
        // Проверяем, наведена ли мышь на объект
        _isHovered = mouseRectangle.Intersects(_boundingBox);

        if (_isHovered)
        {
            HoverAction();
        }
    }

    public override void Draw()
    {
        // Компонент отвечает только за логику, поэтому рисовать не нужно
    }
    public virtual void HoverAction()
    {
        throw new NotImplementedException();
    }
    

    
}