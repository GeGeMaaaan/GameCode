using Gamee._Models;

public class Interaction
{
    public string Name { get; private set; }
    public InteractionAction Action { get; private set; }

    public Interaction(string name, InteractionAction action)
    {
        Name = name;
        Action = action;
    }
    public override bool Equals(object obj)
    {
        var name = obj as Interaction;
        if (name.Name == Name)
        {
            return true;
        }
        return false;
    }
}
