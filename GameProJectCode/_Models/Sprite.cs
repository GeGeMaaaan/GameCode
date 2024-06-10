using Gamee._Manager;
using Gamee.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models
{
    public class Sprite
    {
        public List<Component> _components = new List<Component>();
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int Width;
        public int Height;
        public GameObjectManager gameObjectManager;
        public EventManager eventManager;
        public GameServiceContainer services;
        public Sprite(Vector2 position, string textureName, GameServiceContainer gameServiceContainer)
        {
            services = gameServiceContainer;
            gameObjectManager = gameServiceContainer.GetService<GameObjectManager>();
            eventManager = gameServiceContainer.GetService<EventManager>();
            Texture = Globals.Content.Load<Texture2D>(textureName);
            Position = position;
            Width = Texture.Width;
            Height = Texture.Height;
        }
        
        public void AddComponent(Component component)
        {
            _components.Add(component);
        }

        // Метод для удаления компонента
        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        // Метод для проверки наличия компонента
        public bool HasComponent<T>() where T : Component
        {
            return _components.Any(c => c is T);
        }
        public T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component is T typedComponent)
                {
                    return typedComponent;
                }
            }
            return null; // Если компонент не найден
        }
        public virtual void DeleteSprite()
        {
            gameObjectManager.RemoveSprite(this);
        }
        public virtual void AddSprite(Sprite sprite)
        {
            gameObjectManager.AddSprite(sprite);
        }
        public virtual void Update()
        {
            
        }
        public virtual void Draw()
        {
        }
    }
}
