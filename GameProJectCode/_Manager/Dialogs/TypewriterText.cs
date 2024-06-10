using System.Collections.Generic;
using Gamee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TypewriterText
{
    private Queue<string> textLines;
    private string currentText;
    private string displayedText;
    private float timer;
    private float delayPerCharacter;
    private int currentCharacterIndex;

    public bool IsFinished { get; private set; }

    public TypewriterText(SpriteFont font, string text, float charactersPerSecond, int width)
    {
        textLines = new Queue<string>(GetWrappedTextLines(font, text, width));
        if (textLines.Count > 0)
        {
            currentText = textLines.Dequeue();
        }
        displayedText = "";
        timer = 0f;
        delayPerCharacter = 1f / charactersPerSecond;
        currentCharacterIndex = 0;
        IsFinished = false;
    }

    public void Update()
    {
        if (!IsFinished)
        {
            timer += Globals.TotalSeconds;

            while (timer >= delayPerCharacter)
            {
                if (currentCharacterIndex < currentText.Length)
                {
                    displayedText += currentText[currentCharacterIndex];
                    currentCharacterIndex++;
                    timer -= delayPerCharacter;
                }
                else
                {
                    if (textLines.Count > 0)
                    {
                        currentText = textLines.Dequeue();
                        displayedText += "\n";
                        currentCharacterIndex = 0;
                    }
                    else
                    {
                        IsFinished = true;
                        break;
                    }
                }
            }
        }
    }

    public void Draw(SpriteFont font, Vector2 position, Color color)
    {
        Globals.SpriteBatch.DrawString(font, displayedText, position, color);
    }

    private string[] GetWrappedTextLines(SpriteFont font, string text, int width)
    {
        string[] words = text.Split(' ');
        string wrappedText = "";
        string line = "";

        foreach (string word in words)
        {
            if (font.MeasureString(line + word).X > width)
            {
                wrappedText += line.Trim() + "\n";
                line = "";
            }
            line += word + " ";
        }

        wrappedText += line.Trim();
        return wrappedText.Split('\n');
    }

    public string DisplayedText => displayedText;
    public string Text => currentText;
}
