using Gamee._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SpeakComponent : ActiveObjectComponent
{
    private List<string> dialogs;
    public SpeakComponent(Sprite owner, ActiveType activeType) : base(owner, activeType)
    {
        dialogs = new List<string>();
        AddInteraction("Поговорить",GetDialog);
    }
    public void AddDialog(string name)
    {
        dialogs.Add(name);
    }
    public void RemoveDialog(string name)
    {
        dialogs.Remove(name);
    }
    public void GetDialog()
    {
        _owner.gameObjectManager.StartDialog(dialogs[0]);
        
    }
}
