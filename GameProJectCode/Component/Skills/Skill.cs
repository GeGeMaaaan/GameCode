using Gamee;
using Gamee._Models;
using Gamee.Interface;
using System;


    public abstract class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cooldown { get; set; }
        public int ExecutionTime { get; set; }
        public int LastUsed { get; set; }
        public int Range { get; set; }
        public Skill(string name, string description, int cooldown, int executionTime,int range=999)
        {
            Name = name;
            Description = description;
            Cooldown = cooldown;
            ExecutionTime = executionTime;
            LastUsed = 0;
        }

        public bool IsReady(int currentTime)
        {
            return (currentTime - LastUsed) >= Cooldown;
        }

        //public abstract void Use(ICharacter user, ICharacter target, int currentTime);

    }
public class SkillIcon
{
    public Skill Skill { get; }
    public SkillIconPosition Position { get; }
    public Rectangle BoundingBox { get; }
    private Texture2D texture;
    private bool isHovered;
    private SpriteFont font;
    public SkillIcon(Skill skill, Rectangle boundingBox, Texture2D texture,SkillIconPosition pos)
    {
        Position = pos;
        font = Globals.Content.Load<SpriteFont>("font/FileSmall");
        Skill = skill;
        BoundingBox = boundingBox;
        this.texture = texture;
    }

    public void Update(Vector2 mousePosition)
    {
        isHovered = BoundingBox.Contains(mousePosition);
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(texture, BoundingBox, Color.White);
        if (isHovered)
        {
            DrawSkillInfo(Position);
        }
    }

    private void DrawSkillInfo(SkillIconPosition position)
    {
        string info = $"{Skill.Name}\n{Skill.Description}";
        var infoPosition = Vector2.Zero;
        if(position == SkillIconPosition.left)
            infoPosition = new Vector2(BoundingBox.Right + 10, BoundingBox.Top);
        else if (position == SkillIconPosition.right)
            infoPosition = new Vector2(BoundingBox.Right -300, BoundingBox.Top);
        else if (position == SkillIconPosition.mid)
            infoPosition = new Vector2(BoundingBox.Left, BoundingBox.Bottom+10);
        Globals.SpriteBatch.DrawString(font, info, infoPosition, Color.White);
    }
}



