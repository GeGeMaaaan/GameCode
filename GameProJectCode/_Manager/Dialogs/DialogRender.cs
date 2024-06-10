using Gamee._Manager;
using Gamee;
using System.Collections.Generic;

public class DialogRenderer
{
    private Texture2D speakerTexture;
    private Texture2D textFieldTexture;
    private Texture2D varianceFieldTexture;
    private Texture2D darkerTexture;
    private SpriteFont font;
    private Texture2D[] diceTextures;
    private Vector2 cameraPos;
    private int speakerImageWidth;
    private int textFieldWidth;
    private GameServiceContainer gameService;
    private TypewriterText typewriterText;
    public DialogRenderer(GameServiceContainer gameService, int textFieldWidth, int speakerImageWidth)
    {
        
        this.gameService = gameService;
        this.textFieldWidth = textFieldWidth;
        this.speakerImageWidth = speakerImageWidth;
        LoadContent();
        cameraPos = gameService.GetService<GameObjectManager>().CurrentScreen.Camera.Position;
    }

    private void LoadContent()
    {
        textFieldTexture = Globals.Content.Load<Texture2D>("dialog/TextField");
        speakerTexture = Globals.Content.Load<Texture2D>("dialog/SpeakerImage");
        varianceFieldTexture = Globals.Content.Load<Texture2D>("dialog/VarianceField");
        darkerTexture = Globals.Content.Load<Texture2D>("dialog/darker");
        font = Globals.Content.Load<SpriteFont>("font/File");

        diceTextures = new Texture2D[6];
        for (int i = 0; i < 6; i++)
        {
            diceTextures[i] = Globals.Content.Load<Texture2D>($"dice/dice{i + 1}");
        }
    }
    public void UpdateTypewritedText(TypewriterText typewriterText)
    {
        this.typewriterText = typewriterText;
        this.typewriterText.Update();
    }
    public void DrawTextField(Vector2 position, bool needToDrawText, string Speaker)
    {
        Globals.SpriteBatch.Draw(textFieldTexture, position, Color.White);
        if (Speaker != "System" && Speaker!="Game")
        {
            if(Speaker=="Андрей Левин")
                Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>($"dialog/Speaker/{Speaker}"), new Vector2(position.X, position.Y - 100), Color.White);
            else
                Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>($"dialog/Speaker/{Speaker}"), new Vector2(position.X+ textFieldWidth-speakerImageWidth, position.Y - 100), Color.White);
        }
        if (needToDrawText)
        {
            if (Speaker == "Андрей Левин")
                typewriterText.Draw(font, new Vector2(position.X + speakerImageWidth, position.Y + 50), Color.White);
            else if (Speaker != "Game")
                typewriterText.Draw(font, new Vector2(position.X, position.Y + 50), Color.White);
            else typewriterText.Draw(font, new Vector2(position.X + 50, position.Y + 50), Color.White);
        }
    }

    public void DrawDarker()
    {
        Globals.SpriteBatch.Draw(darkerTexture, new Vector2(cameraPos.X - 2000, cameraPos.Y - 1080), Color.White * 0.8f);
    }

    public void DrawVariances(Vector2 position, List<string> variance)
    {
        foreach (string var in variance)
        {
            Globals.SpriteBatch.Draw(varianceFieldTexture, position, Color.White);
            Globals.SpriteBatch.DrawString(font, var, position, Color.White);
            position = new Vector2(position.X, position.Y + 100);
        }
    }
    public void DrawCheckInfoWhenHover(Vector2 position,(MainStats,int) checkInfo,Dictionary<MainStats,int> heroStats)
    {
        var checkInfoText = $"Нужно {checkInfo.Item2} {checkInfo.Item1} ";
        var heroStatsText = $"У вас {heroStats[checkInfo.Item1]} {checkInfo.Item1}";
        int successfulOutcomes = 0;
        int totalOutcomes = 36; // 6 * 6 = 36 возможных исходов при броске двух кубиков d6

        for (int dice1 = 1; dice1 <= 6; dice1++)
        {
            for (int dice2 = 1; dice2 <= 6; dice2++)
            {
                int sum = dice1 + dice2 + heroStats[checkInfo.Item1];
                if (sum > checkInfo.Item2)
                {
                    successfulOutcomes++;
                }
            }
        }
        var chanceText = $"Шанс {(successfulOutcomes * 100 / totalOutcomes)}%";
        Globals.SpriteBatch.DrawString(font, checkInfoText, new Vector2(position.X, position.Y + 100), Color.White);
        Globals.SpriteBatch.DrawString(font, chanceText, new Vector2(position.X, position.Y + 0), Color.White);
        Globals.SpriteBatch.DrawString(font, heroStatsText, new Vector2(position.X, position.Y + -100), Color.White);
    }
    public void DrawDice(Vector2 position, int dice1, int dice2,int needRoll, (MainStats,int) statInfo)
    {
        var checkInfo = $"{statInfo.Item1} {needRoll}";
        Globals.SpriteBatch.DrawString(font, checkInfo, new Vector2(position.X + speakerImageWidth + 80, position.Y + 100), Color.White);
        Globals.SpriteBatch.Draw(diceTextures[dice1 - 1], new Vector2(position.X+speakerImageWidth+50, position.Y+200), Color.White);
        Globals.SpriteBatch.Draw(diceTextures[dice2 - 1], new Vector2(position.X + speakerImageWidth+150, position.Y + 200), Color.White);
        var newText = $"{dice1} + {dice2} + {(statInfo).Item2} = {dice1 + dice2 + statInfo.Item2}";
        Globals.SpriteBatch.DrawString(font, newText, new Vector2(position.X + speakerImageWidth, position.Y + 400), Color.White);
        if(dice1 + dice2 + statInfo.Item2 >= needRoll||(dice1 == 6&& dice2==6)&&(!(dice1==1&&dice2==1)))
        {
            Globals.SpriteBatch.DrawString(font, "Успех", new Vector2(position.X + speakerImageWidth+100, position.Y + 500), Color.White);
        }
        else
        {
            Globals.SpriteBatch.DrawString(font, "Неудача", new Vector2(position.X + speakerImageWidth+100, position.Y + 500), Color.White);
        }
    }
    public SpriteFont Font => font;
    public int TextFieldWidth => textFieldWidth; 
    public int SpeakerImageWidth => speakerImageWidth;
}
