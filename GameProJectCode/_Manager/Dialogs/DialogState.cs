
using System.Collections.Generic;

public class DialogState
{
    public Dialog CurrentDialog { get; set; }
    public List<Dialog> Dialogs { get; private set; }
    public int NumOfDialog { get; set; }
    public Phrase CurrentPhrase { get; set; }
    public bool IsVariancePhrase { get; set; }
    public bool WaitPlayerResponse { get; set; }
    public bool IsHover { get; set; }
    public (MainStats,int) currentHoverInfo { get; set; }
    public List<string> EventList { get; private set; }
    public List<string> ListCheckForRemove { get; private set; }

    public DialogState(Dialog initialDialog)
    {
        CurrentDialog = initialDialog;
        Dialogs = new List<Dialog> { initialDialog };
        EventList = new List<string>();
        ListCheckForRemove = new List<string>();
        NumOfDialog = 0;
        IsVariancePhrase = false;
        WaitPlayerResponse = false;
        IsHover = false;

    }

    public void AddDialog(Dialog dialog)
    {
        Dialogs.Add(dialog);
        NumOfDialog++;
    }

    public void ResetDialogCounter()
    {
        NumOfDialog = 0;
    }
}
