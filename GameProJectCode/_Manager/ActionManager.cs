using Gamee._Models;
using Gamee.Interface;
using Gamee.Manager;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Gamee._Manager
{
    public class ActionManager
    {
        private bool _previousClickState = false;
        private bool CanActive = true;
        private bool FightInteraction = false;

        public ActionManager(GameServiceContainer serviceContainer)
        {
            EventManager eventManager = serviceContainer.GetService<EventManager>();
            eventManager.Subscribe("StartDialog", DisableActive);
            eventManager.Subscribe("DialogEnd", EnableActive);
            eventManager.Subscribe("StartFight", FightInteractionEnable);
            eventManager.Subscribe("EndFight", FightInteractionDisable);
        }

        public void Update(List<Sprite> activeSprite, List<Sprite> activeUI, GameServiceContainer serviceContainer)
        {
            if (CanActive)
            {
                bool currentClickState = InputManager.wasClickForInteraction;

                if (!_previousClickState && currentClickState)
                {
                    HandleActiveSprites(activeSprite, serviceContainer);
                    HandleActiveUIElements(activeUI, serviceContainer);
                }

                _previousClickState = currentClickState; // Обновляем предыдущее состояние клика
            }
        }

        private void HandleActiveSprites(List<Sprite> activeSprite, GameServiceContainer serviceContainer)
        {
            Hero hero = serviceContainer.GetService<Hero>();
            Vector2 clickPosition = InputManager.MousePositionWorld;
            Rectangle mouseRectangle = new Rectangle((int)clickPosition.X, (int)clickPosition.Y, 1, 1);

            foreach (StandartActiveSprite obj in activeSprite)
            {
                ActiveObjectComponent activeObjectComponent;
                if (!FightInteraction)
                {

                    activeObjectComponent = obj.ActiveObjectComponent;
                }
                else
                {
                    activeObjectComponent = obj.ActiveObjectComponentForFight;
                }
                CollisionComponent collisionObjectComponent = obj.CollisionComponent;

                if (collisionObjectComponent.Intersects(mouseRectangle))
                {
                    Vector2 heroCenter = new Vector2(hero.Position.X + hero.Width / 2, hero.Position.Y + hero.Height / 2);
                    Vector2 objectCenter = new Vector2(obj.Position.X + obj.Width / 2, obj.Position.Y + obj.Height / 2);
                    float objectRadius = Math.Max(obj.Width, obj.Height) / 2f;
                    float distance = Vector2.Distance(heroCenter, objectCenter);

                    if (activeObjectComponent.ActiveType == ActiveType.NearPlayer && distance < objectRadius + 200)
                    {
                        activeObjectComponent.Interact();
                    }
                    else if (activeObjectComponent.ActiveType == ActiveType.OnlyClick)
                    {
                        activeObjectComponent.Interact();
                    }
                }
            }
        }

        private void HandleActiveUIElements(List<Sprite> activeUI, GameServiceContainer serviceContainer)
        {
            Vector2 clickPosition = InputManager.MousePositionForUI;
            Rectangle mouseRectangle = new Rectangle((int)clickPosition.X, (int)clickPosition.Y, 1, 1);

            foreach (Sprite obj in activeUI)
            {
                ActiveObjectComponent activeObjectComponent = obj.GetComponent<ActiveObjectComponent>();
                CollisionComponent collisionObjectComponent = obj.GetComponent<CollisionComponent>();

                if (collisionObjectComponent.Intersects(mouseRectangle))
                {
                    Hero hero = serviceContainer.GetService<Hero>();
                    Vector2 heroCenter = new Vector2(hero.Position.X + hero.Width / 2, hero.Position.Y + hero.Height / 2);
                    Vector2 objectCenter = new Vector2(obj.Position.X + obj.Width / 2, obj.Position.Y + obj.Height / 2);
                    float objectRadius = Math.Max(obj.Width, obj.Height) / 2f;
                    float distance = Vector2.Distance(heroCenter, objectCenter);

                    if (activeObjectComponent.ActiveType == ActiveType.NearPlayer && distance < objectRadius + 200)
                    {
                        activeObjectComponent.Interact();
                    }
                    else if (activeObjectComponent.ActiveType == ActiveType.OnlyClick)
                    {
                        activeObjectComponent.Interact();
                    }
                }
            }
        }

        public void DisableActive()
        {
            CanActive = false;
        }

        public void EnableActive()
        {
            CanActive = true;
        }
        public void FightInteractionEnable()
        {
            FightInteraction = true;
        }
        public void FightInteractionDisable()
        {
            FightInteraction = false;
        }
    }
}
