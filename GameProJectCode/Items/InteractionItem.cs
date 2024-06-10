using Gamee._Models;

public class InteractionForItem
{
    public string Name { get; private set; }
    public InteractionItem Action { get; private set; }

    public InteractionForItem(string name, InteractionItem action)
    {
        Name = name;
        Action = action;
    }
    public  bool Equals(InteractionForItem obj)
    {
        var name = obj;
        if (name.Name == Name)
        {
            return true;
        }
        return false;
    }
}
