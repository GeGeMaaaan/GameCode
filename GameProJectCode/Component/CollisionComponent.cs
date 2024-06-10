using Gamee._Models;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

public class CollisionComponent : Component
{
    private List<Rectangle> _boundingBoxes;
    private CollisionType _collisionType;
    private bool _collisionEnabled = true;
    private bool _static = true;

    // Конструктор для одного прямоугольного bounding box
    public CollisionComponent(Sprite owner, CollisionType collisionType, bool Static) : base(owner)
    {
        _boundingBoxes = new List<Rectangle>
        {
            new Rectangle((int)owner.Position.X, (int)owner.Position.Y, owner.Texture.Width, owner.Texture.Height)
        };
        _collisionType = collisionType;
        _static = Static;
    }

    // Конструктор для нескольких прямоугольных bounding box
    public CollisionComponent(Sprite owner, CollisionType collisionType, bool Static, List<Rectangle> boundingBoxes) : base(owner)
    {
        _boundingBoxes = boundingBoxes;
        _collisionType = collisionType;
        _static = Static;
    }

    public void ChangeCollisionBoxes(List<Rectangle> boundingBoxes)
    {
        _boundingBoxes = boundingBoxes;
    }

    public void ChangeCollision()
    {
        _collisionEnabled = !_collisionEnabled;
    }

    public override void Update()
    {
        if (!_static)
        {
            var offset = _owner.Position;
            for (int i = 0; i < _boundingBoxes.Count; i++)
            {
                var boundingBox = _boundingBoxes[i];
                _boundingBoxes[i] = new Rectangle(
                    (int)(offset.X),
                    (int)(offset.Y),
                    boundingBox.Width,
                    boundingBox.Height);
            }
        }
    }

    public override void Draw()
    {
        // Этот компонент не требует отрисовки, так что метод Draw оставляем пустым
    }

    public bool Intersects(Rectangle otherBoundingBox)
    {
        if (!_collisionEnabled)
            return false;

        foreach (var boundingBox in _boundingBoxes)
        {
            if (boundingBox.Intersects(otherBoundingBox))
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeCollisionType(CollisionType collisionType)
    {
        _collisionType = collisionType;
    }
    public Rectangle BoundingBox=> _boundingBoxes[0];
    public List<Rectangle> BoundingBoxes => _boundingBoxes;
    public CollisionType CollisionType => _collisionType;
    public bool CollisionEnabled => _collisionEnabled;
}
