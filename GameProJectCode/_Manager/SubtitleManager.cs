using System.Collections.Generic;
using Gamee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gamee._Manager
{
    public class SubtitleManager
    {
        private TypewriterText subtitle;
        private float displayTime;
        private float elapsedTime;
        private SpriteFont font = Globals.Content.Load<SpriteFont>("font/FileSmall");
        public SubtitleManager()
        {
            displayTime = 3f;
        }

        public void Update()
        {
            if (subtitle != null)
            {
                subtitle.Update();
                if (subtitle.IsFinished)
                {
                    elapsedTime += Globals.TotalSeconds;
                    if (elapsedTime >= displayTime)
                    {
                        subtitle = null;
                        elapsedTime = 0f;
                    }
                }
            }
        }

        public void Draw()
        {
            if (subtitle != null)
            {
                Vector2 aboba = font.MeasureString(subtitle.Text);
               
                subtitle.Draw(font,new Vector2(990-aboba.X/2, 1000), Color.White);
                
                
            }
        }

        public void AddSubtitle(string text)
        {
            subtitle = new TypewriterText(font,text, 25,1980);
            elapsedTime = 0f; 
        }
    }
}
