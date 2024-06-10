using Gamee._Models;
using Gamee.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gamee;
using Microsoft.VisualBasic;
using Gamee.Manager;
using Gamee._Manager;
public delegate void InteractionAction();
public class ActiveObjectComponent : Component
{
    private bool _active=false;
    private Vector2 _targetPoint;
    private float _targetRadius;
    private List<Interaction> _interactions = new List<Interaction>();
    public ActiveType ActiveType;
    private Texture2D menuTexture;
    private int width;
    private int height;
    private InteractionMenuManager InteractionMenuManager;
    public ActiveObjectComponent(Sprite owner,ActiveType activeType):base(owner)
    {
        ActiveType = activeType;
        width = owner.Width;
        height = owner.Height;
        _targetPoint = new Vector2(owner.Position.X+width / 2, owner.Position.Y+ height / 2);//середина объекта
        _targetRadius =( width / 2);
        menuTexture = Globals.Content.Load<Texture2D>("InteractionMenuBase");
        InteractionMenuManager = new InteractionMenuManager(_owner, _interactions, menuTexture);
        
    }
    public void PlaceTargePoint(Vector2 newPoint)
    {
        _targetPoint = newPoint;
    }
    public void AddInteraction(string name,InteractionAction action)
    {
        _interactions.Add(new Interaction(name,action));
    }
    public void RemoveInteraction(string name, InteractionAction action)
    {
        _interactions.Remove(new Interaction(name, action));
    }
    public void Interact()
    {
        if (_interactions.Count == 1)
        {
            _interactions[0].Action.Invoke();
            _active = !_active;
        }
        else
        {
            InteractionMenuManager.ToggleVisibility();
        }
    }
    public override void Update()
    {
        width = _owner.Texture.Width;
        height = _owner.Texture.Height;
        _targetPoint = new Vector2(_owner.Position.X + width / 2, _owner.Position.Y + height / 2);//середина объекта
        _targetRadius = width / 2 + 20;
    }

    public override void Draw()
    {
        
    }
    public InteractionMenuManager menuManager => InteractionMenuManager;
    public Vector2 TargetPoint => _targetPoint;
    public float TargetRadius => _targetRadius;
    public bool Active => _active;
}