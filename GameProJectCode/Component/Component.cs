using Gamee._Models;

public abstract class Component
{
    protected Sprite _owner;

    public Component(Sprite owner)
    {
        _owner = owner;
    }
    public abstract void Update();
    public abstract void Draw();
}