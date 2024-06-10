using Gamee._Models;
using Gamee._Models.Components;
using Gamee.Interface;
using Gamee.Manager;
using System.Collections.Generic;

namespace Gamee._Manager
{
    public class CollisionManager
    {
        private Hero _hero;
        private MovementComponent movementComponent;
        public CollisionManager(Hero hero)
        {
            _hero = hero;
             movementComponent = _hero.GetComponent<MovementComponent>();
        }
        public void HandleCollisions(List<Sprite> collidableObjects)
        {
            movementComponent.CanMoveLeft = true;
            movementComponent.CanMoveRight = true;
            foreach (Sprite coll in collidableObjects)
                HandleCollisionBlock(coll,_hero.movementHeroManager);
        }
        public void HandleCollisionBlock(Sprite otherSprite, MovementHeroManager movementHeroManager)
        {
            CollisionComponent otherCollision = otherSprite.GetComponent<CollisionComponent>();
            Rectangle heroBounds = _hero.CollisionComponent.BoundingBox;
            Rectangle spriteBounds = otherCollision.BoundingBox;

            if (otherCollision.CollisionType!=CollisionType.None && _hero.CollisionComponent.Intersects(spriteBounds))
            {
                if (heroBounds.Right > spriteBounds.Left && heroBounds.Left < spriteBounds.Right)
                {
                    if (heroBounds.Right -5 > spriteBounds.Left)
                    {
                        movementComponent.CanMoveLeft = false;
                        movementHeroManager.CollideLeft();
                    }
                    if (heroBounds.Left +5 < spriteBounds.Right)
                    {
                        movementComponent.CanMoveRight = false;
                        movementHeroManager.CollideRight();
                    }

                }
            }         
        }



    }
}

