using Gamee;
using Gamee._Manager;
using Gamee._Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class StatsComponent : Component
{
    private bool isAFight = false;
    public int CurrentHealth = 1;
    private Dictionary<string, int> Parameter;
    private Dictionary<MainStats, int> Stats;

    private Dictionary<string, int> BaseParameter;
    private Dictionary<MainStats, int> BaseStats;

    private Dictionary<string, int> BonusParameter;
    private Dictionary<MainStats, int> BonusStats;

    private bool needToUpdateStats = true;
    private Sprite owner;
    private SpriteFont _font;
    private Texture2D _healthBarTexture;
    private Vector2 _healthBarPosition;
    private Vector2 _healthBarSize;

    public StatsComponent(Sprite owner, GameServiceContainer services)
        : base(owner)
    {
        this.owner = owner;
        _font = Globals.Content.Load<SpriteFont>("font/FileSmall");
        _healthBarTexture = Globals.Content.Load<Texture2D>("HealthBar");
        _healthBarPosition = new Vector2(owner.Position.X,owner.Position.Y-100);
        _healthBarSize = new Vector2(200,18);

        services.GetService<EventManager>().Subscribe("EnterAFight", EnterAFight);
        BaseStats = new Dictionary<MainStats, int>
        {
            { MainStats.dexterity, 1 },
            { MainStats.strength, 1 },
            { MainStats.gun_mastery, 1 },
            { MainStats.diplomacy, 1 },
            { MainStats.intelligence, 1 },
            { MainStats.medicine, 1 }
        };

        BaseParameter = new Dictionary<string, int>
        {
            { "MaxHealth", 10 },
            { "Damage", 1 },
            { "Defense", 1 }
        };

        BonusParameter = new Dictionary<string, int>
        {
            { "MaxHealth", 0 },
            { "Damage", 0 },
            { "Defense", 0 }
        };

        Parameter = new Dictionary<string, int>
        {
            { "MaxHealth", 0 },
            { "Damage", 0 },
            { "Defense", 0 }
        };

        Stats = new Dictionary<MainStats, int>
        {
            { MainStats.dexterity, 0 },
            { MainStats.strength, 0 },
            { MainStats.gun_mastery, 0 },
            { MainStats.diplomacy, 0 },
            { MainStats.intelligence, 0 },
            { MainStats.medicine, 0 }
        };

        BonusStats = new Dictionary<MainStats, int>
        {
            { MainStats.dexterity, 0 },
            { MainStats.strength, 0 },
            { MainStats.gun_mastery, 0 },
            { MainStats.diplomacy, 0 },
            { MainStats.intelligence, 0 },
            { MainStats.medicine, 0 }
        };
    }
    public Dictionary<MainStats, int> GetCurrentStats()
    {
        return Stats;
    }
    public Dictionary<string, int> GetCurrentParameter()
    {
        return Parameter;
    }
    public void EnterAFight()
    {
        isAFight = true;
    }

    private void DrawHealthBar()
    {
        float healthPercentage = (float)CurrentHealth / Parameter["MaxHealth"];
        Vector2 healthBarFillSize = new Vector2(_healthBarSize.X * healthPercentage, _healthBarSize.Y);
        Globals.SpriteBatch.Draw(_healthBarTexture, new Rectangle(_healthBarPosition.ToPoint(), _healthBarSize.ToPoint()), Color.Black);
        Globals.SpriteBatch.Draw(_healthBarTexture, new Rectangle(_healthBarPosition.ToPoint(), healthBarFillSize.ToPoint()), Color.White);
        string healthText = $"{CurrentHealth} / {Parameter["MaxHealth"]}";
        Vector2 textSize = _font.MeasureString(healthText);
        Vector2 textPosition = _healthBarPosition + new Vector2((_healthBarSize.X - textSize.X) / 2, (_healthBarSize.Y - textSize.Y) / 2);
        Globals.SpriteBatch.DrawString(_font, healthText, textPosition, Color.White);
    }

    public void SetBaseStats(int maxHealth, int damage, int defense, Dictionary<MainStats, int> stats, int ChangeFromMaxHealth = 0)
    {
        BaseStats = stats;
        BaseParameter["MaxHealth"] = maxHealth;
        BaseParameter["Damage"] = damage;
        BaseParameter["Defense"] = defense;
        CurrentHealth = BaseParameter["MaxHealth"] + ChangeFromMaxHealth;
        needToUpdateStats = true;
    }

    public void ModifyStatsByEquipment(Dictionary<string, int> ParameterChange, Dictionary<MainStats, int> statsChange, int CurrentHpChange = 0)
    {
        foreach (var parameter in ParameterChange)
        {
            BonusParameter[parameter.Key] = parameter.Value;
        }
        foreach (var stats in statsChange)
        {
            BonusStats[stats.Key] += stats.Value;
        }
        needToUpdateStats = true;
    }

    public void ModifyStatsByItem(Dictionary<string, int> ParameterChange, Dictionary<MainStats, int> statsChange, int CurrentHpChange = 0)
    {
        foreach (var parameter in ParameterChange)
        {
            BonusParameter[parameter.Key] = parameter.Value;
        }
        foreach (var stats in statsChange)
        {
            BonusStats[stats.Key] += stats.Value;
        }
        needToUpdateStats = true;
    }

    private void ResetDictValuesToZero(Dictionary<string, int> inputDict)
    {
        List<string> keys = new List<string>(inputDict.Keys);
        foreach (var key in keys)
        {
            inputDict[key] = 0;
        }
    }

    private void ResetDictValuesToZero(Dictionary<MainStats, int> inputDict)
    {
        List<MainStats> keys = new List<MainStats>(inputDict.Keys);
        foreach (var key in keys)
        {
            inputDict[key] = 0;
        }
    }

    public void SetNewStats()
    {
        Parameter["MaxHealth"] = BonusParameter["MaxHealth"] + BaseParameter["MaxHealth"];
        if (CurrentHealth > Parameter["MaxHealth"]) CurrentHealth = Parameter["MaxHealth"];
        Parameter["Damage"] = BonusParameter["Damage"] + BaseParameter["Damage"];
        Parameter["Defense"] = BonusParameter["Defense"] + BaseParameter["Defense"];
        ResetDictValuesToZero(BonusParameter);
        Stats[MainStats.dexterity] = BonusStats[MainStats.dexterity] + BaseStats[MainStats.dexterity];
        Stats[MainStats.strength] = BonusStats[MainStats.strength] + BaseStats[MainStats.strength];
        Stats[MainStats.gun_mastery] = BonusStats[MainStats.gun_mastery] + BaseStats[MainStats.gun_mastery];
        Stats[MainStats.diplomacy] = BonusStats[MainStats.diplomacy] + BaseStats[MainStats.diplomacy];
        Stats[MainStats.intelligence] = BonusStats[MainStats.intelligence] + BaseStats[MainStats.intelligence];
        Stats[MainStats.medicine] = BonusStats[MainStats.medicine] + BaseStats[MainStats.medicine];
        ResetDictValuesToZero(BonusStats);
    }

    public override void Draw()
    {
        
            DrawHealthBar();
        
    }

    public override void Update()
    {
        if (CurrentHealth <= 0)
        {
            owner.DeleteSprite();
        }
        _healthBarPosition = new Vector2(owner.Position.X, owner.Position.Y - 100);
        if (needToUpdateStats)
        {
            SetNewStats();
            needToUpdateStats = false;
        }
    }
}
