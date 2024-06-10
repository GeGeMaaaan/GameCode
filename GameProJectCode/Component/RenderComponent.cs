using Gamee;
using Gamee._Models;

public class RenderComponent : Component
{

    public RenderComponent(Sprite _owner) : base(_owner)
    {
    }
    public override void Update()
    {
        // Пустая реализация метода Update
        // RenderComponent не требует обновления, поэтому этот метод остается пустым
    }
    public override void Draw()
    {
        Globals.SpriteBatch.Draw(_owner.Texture, _owner.Position, Color.White);
    }
    public void DrawActive()
    {
        Globals.SpriteBatch.Draw(_owner.Texture, _owner.Position, Color.Yellow);
    }
    public virtual void Draw(int newWidth, int newHieght)
    {
        //Меняю логику в классе Draw, бог не любит меня
        _owner.Width = newWidth;
        _owner.Height = newHieght;

        Globals.SpriteBatch.Draw(_owner.Texture,
            new Rectangle((int)_owner.Position.X, (int)_owner.Position.Y, newWidth, newHieght),
            Color.White);

    }
}