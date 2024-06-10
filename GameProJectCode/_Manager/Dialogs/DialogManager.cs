using Gamee._Manager;
using Gamee._Models;

public class DialogManager
{
    private DialogState dialogState;
    private DialogRenderer dialogRenderer;
    private DialogInputHandler dialogInputHandler;
    private Counter counter;
     private StatsComponent statsComponent;
    public DialogManager(Dialog dialog, GameServiceContainer gameService)
    {
        statsComponent = gameService.GetService<Hero>().StatsComponent;
        dialogState = new DialogState(dialog);
        if (dialogState.CurrentPhrase == null)
        {
            dialogState.CurrentPhrase = dialog.GetPhrases()[0];
        }
        dialogRenderer = new DialogRenderer(gameService, 612, 275);
        counter = new Counter();
        counter.CurrentCounter = 0;
        dialogInputHandler = new DialogInputHandler(dialogState, dialogRenderer, counter, gameService.GetService<EventManager>(),gameService.GetService<CheckController>());
    }

    public void Update()
    {
        dialogInputHandler.HandleInput();
        
            dialogRenderer.UpdateTypewritedText(dialogInputHandler.typewriterText);
        
        
    }

    public void Draw()
    {
        if (dialogState.CurrentDialog.Type == "Solo")
        {
            if (dialogState.IsVariancePhrase)
            {
                
                if (dialogState.IsHover)
                {
                    dialogRenderer.DrawCheckInfoWhenHover(new Vector2(dialogRenderer.TextFieldWidth, 300), dialogState.currentHoverInfo, statsComponent.GetCurrentStats());
                }
                dialogRenderer.DrawTextField(new Vector2(0, 300), false, "Андрей Левин");
                dialogRenderer.DrawVariances(new Vector2(dialogRenderer.TextFieldWidth, 500), dialogState.CurrentDialog.GetVarianceName());
                dialogState.WaitPlayerResponse = true;
            }
            else
            {
                if (dialogState.CurrentPhrase.Speaker == "System" && dialogState.CurrentPhrase.Text == "Check")
                {
                    
                    dialogRenderer.DrawTextField(new Vector2(0, 300), true, dialogState.CurrentPhrase.Speaker);
                    dialogRenderer.DrawDice(new Vector2(0, 300), dialogInputHandler.LastDice1, dialogInputHandler.LastDice2, dialogInputHandler.lastNeedRoll, dialogInputHandler.lastStat);
                }
                else if (dialogState.CurrentPhrase.Speaker == "Андрей Левин" || dialogState.CurrentPhrase.Speaker == "Game")
                {
                    
                    dialogRenderer.DrawTextField(new Vector2(0, 300), true, dialogState.CurrentPhrase.Speaker);
                }

            }
        }
        else if (dialogState.CurrentDialog.Type == "Dialog")
        {
            if (dialogState.IsVariancePhrase)
            {
               
                if (dialogState.IsHover)
                {
                    dialogRenderer.DrawCheckInfoWhenHover(new Vector2(dialogRenderer.TextFieldWidth, 300), dialogState.currentHoverInfo, statsComponent.GetCurrentStats());
                }
                dialogRenderer.DrawTextField(new Vector2(0, 300), false, "Андрей Левин");
                dialogRenderer.DrawVariances(new Vector2(dialogRenderer.TextFieldWidth, 500), dialogState.Dialogs[dialogState.NumOfDialog].GetVarianceName());
                dialogState.WaitPlayerResponse = true;
            }
            else
            {
                if (dialogState.CurrentPhrase.Speaker == "System" && dialogState.CurrentPhrase.Text == "Check")
                {
                    
                    dialogRenderer.DrawTextField(new Vector2(0, 300), true, dialogState.CurrentPhrase.Speaker);
                    dialogRenderer.DrawDice(new Vector2(0, 300), dialogInputHandler.LastDice1, dialogInputHandler.LastDice2, dialogInputHandler.lastNeedRoll, dialogInputHandler.lastStat);
                }
                else if (dialogState.CurrentPhrase.Speaker == "Андрей Левин" || dialogState.CurrentPhrase.Speaker == "Game")
                {
                    
                    dialogRenderer.DrawTextField(new Vector2(0, 300), true, dialogState.CurrentPhrase.Speaker);
                }
                else
                {
                    dialogRenderer.DrawTextField(new Vector2(1920 - dialogRenderer.TextFieldWidth, 300), true, dialogState.CurrentPhrase.Speaker);
                }
            }
        }
            
        
        
    }
}
