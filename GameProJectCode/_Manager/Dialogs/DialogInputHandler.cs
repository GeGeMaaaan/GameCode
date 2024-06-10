using Gamee._Manager;
using Gamee.Manager;
using System.Collections.Generic;
using System.Linq;

public class DialogInputHandler
{
    private DialogState dialogState;
    private DialogRenderer renderer;
    private Counter counter;
    private EventManager eventManager;
    private CheckController checkController;
    public TypewriterText typewriterText;
    private bool END;
    public int LastDice1;
    public int LastDice2;
    public (MainStats,int) lastStat;
    public int lastNeedRoll;
    private bool FirstClick = true;
    public DialogInputHandler(DialogState dialogState, DialogRenderer renderer, Counter counter, EventManager eventManager,CheckController checkController)
    {
        this.checkController = checkController;
        this.dialogState = dialogState;
        this.renderer = renderer;
        this.counter = counter;
        this.eventManager = eventManager;
        ProcessNextPhrase();

    }
    public void HandleInput()
    {
        if (END)
        {
            eventManager.Invoke("DialogEnd");
            foreach (var events in dialogState.EventList)
            {
                eventManager.Invoke(events);
            }
            foreach (var check in dialogState.ListCheckForRemove)
            {
                dialogState.Dialogs[0].RemoveDialogVariance(check);
            }
                
        }
        else if (dialogState.WaitPlayerResponse)
        {
            HandleHover(new Vector2(InputManager.MouseState.Position.X, InputManager.MouseState.Position.Y),dialogState.Dialogs[dialogState.NumOfDialog].GetVarianceCheck());
            if (InputManager.wasClickForInteraction)
                HandleMenuClick(new Vector2(InputManager.MouseState.Position.X, InputManager.MouseState.Position.Y), dialogState.Dialogs[dialogState.NumOfDialog].GetVarianceName());
        }
        else
        {
            if (InputManager.wasClickForInteraction)
            {
                if(counter.CurrentCounter >= dialogState.Dialogs[dialogState.NumOfDialog].GetPhrases().Count)
                {
                    MoveToPreviousDialog();
                }
                if (!END&&!FirstClick)
                {
                    ProcessNextPhrase();  
                }
                FirstClick = false;

                
            }
        }
    }

    private void ProcessNextPhrase()
    {
        var writeSome = false;
        
        while (!writeSome)
        {
            dialogState.CurrentPhrase = dialogState.Dialogs[dialogState.NumOfDialog].GetPhrases()[counter.CurrentCounter];
            if (dialogState.CurrentPhrase.HasVariance)
            {
                dialogState.IsVariancePhrase = true;
                writeSome = true;

            }
            else if (dialogState.CurrentPhrase.Speaker == "System")
            {
                if (dialogState.CurrentPhrase.Text == "Check")
                {
                    typewriterText = new TypewriterText(renderer.Font, "", 30, renderer.TextFieldWidth - renderer.SpeakerImageWidth);
                    PerformCheck();
                    writeSome= true;

                }
                else if ( dialogState.CurrentPhrase.Text.StartsWith("Event"))
                {
                    dialogState.EventList.Add(dialogState.CurrentPhrase.Text.Split()[1]);
                }
            }
            else
            { 
                writeSome = true;
                dialogState.IsVariancePhrase = false;
                if (dialogState.CurrentPhrase.Speaker != "Game")
                    typewriterText = new TypewriterText(renderer.Font, dialogState.CurrentPhrase.Text, 30, renderer.TextFieldWidth - renderer.SpeakerImageWidth);
                else
                    typewriterText = new TypewriterText(renderer.Font, dialogState.CurrentPhrase.Text, 30, renderer.TextFieldWidth - 50);
                
            }
            counter.CurrentCounter++;
        }

        
        // Если цикл завершен и фраза не найдена, перейти к предыдущему диалогу

    }

    private void PerformCheck()
    {

        var res = checkController.Check(dialogState.Dialogs[dialogState.NumOfDialog].Check.First().Key, dialogState.Dialogs[dialogState.NumOfDialog].Check.First().Value);
        dialogState.AddDialog(dialogState.Dialogs[dialogState.NumOfDialog].GetDialog(res.ToString()));
        //dialogState.EventList.Add(dialogState.Dialogs[dialogState.NumOfDialog].CheckName);
        counter = new Counter(counter);
        counter.CurrentCounter--;
        LastDice1 = checkController.GetLastDice1();
        LastDice2 = checkController.GetLastDice2();
        lastStat = checkController.GetLastStatCheck();
        lastNeedRoll = checkController.GetLastNeedCheck();
    }

    private void MoveToPreviousDialog()
    {
        while (counter != null && counter.CurrentCounter >= dialogState.Dialogs[dialogState.NumOfDialog].GetPhrases().Count)
        {
            dialogState.NumOfDialog--;
            counter = counter.prevCounter;
            if(counter != null)
            {
                counter.CurrentCounter++;
            } 
        }
        if (dialogState.NumOfDialog < 0)
        {
            END = true;
        }
    }
    private void HandleHover(Vector2 MousePosition, Dictionary<MainStats, int> Checks)
    {
        var position = new Vector2(renderer.TextFieldWidth, 500);
        dialogState.IsHover = false;
        for (int i = 0; i < Checks.Count; i++)
        {
            Rectangle menuBound = new Rectangle((int)position.X, (int)position.Y, 400, 100);

            if (MouseIntersect(MousePosition, menuBound))
            {
                dialogState.IsHover = true;
                dialogState.currentHoverInfo = (Checks.Keys.ToList()[i], Checks.Values.ToList()[i]);
                break;
            }

            position = new Vector2(position.X, position.Y + 100);
        }
        
    }
    private void HandleMenuClick(Vector2 clickPosition, List<string> variance)
    {
        var position = new Vector2(renderer.TextFieldWidth, 500);

        for (int i = 0; i < variance.Count; i++)
        {
            Rectangle menuBound = new Rectangle((int)position.X, (int)position.Y, 400, 100);

            if (MouseIntersect(clickPosition, menuBound))
            {
                dialogState.CurrentPhrase = new Phrase("Андрей Левин", variance[i]);
                typewriterText = new TypewriterText(renderer.Font, dialogState.CurrentPhrase.Text, 30, renderer.TextFieldWidth - renderer.SpeakerImageWidth);
                dialogState.AddDialog(dialogState.Dialogs[dialogState.NumOfDialog].GetDialog(dialogState.CurrentPhrase.Text));
                if (dialogState.Dialogs[dialogState.NumOfDialog].Check.Count>0)
                {
                    dialogState.ListCheckForRemove.Add(dialogState.CurrentPhrase.Text);
                }
                counter = new Counter(counter);
                dialogState.WaitPlayerResponse = false;
                dialogState.IsVariancePhrase = false;
                break;
            }

            position = new Vector2(position.X, position.Y + 100);
        }
    }

    private bool MouseIntersect(Vector2 clickPosition, Rectangle menuBound)
    {
        return menuBound.Contains(clickPosition);
    }
}
