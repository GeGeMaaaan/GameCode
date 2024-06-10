namespace Gamee._Models
{
    public class Background : Sprite
    {
        private RenderComponent _renderComponent;
        public Background(Vector2 pos, string textureName, GameServiceContainer services) : base(pos, textureName, services)
        {
            _renderComponent = new RenderComponent(this);
            AddComponent(_renderComponent);
        }
        public override void Update()
        {
        }
        public override void Draw()
        {
            _renderComponent.Draw();
        }
        
    }
}
