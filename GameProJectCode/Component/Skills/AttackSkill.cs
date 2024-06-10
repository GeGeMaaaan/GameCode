using Gamee._Models;
using Gamee.Interface;
using System;


public class AttackSkill : Skill
{
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
    public Func<ICharacter, bool> Condition { get; set; }
    public Func<ICharacter, int> DamageModifier { get; set; }
    public AttackSkill(string name, string description, int cooldown, int executionTime, int minDamage, int maxDamage,int range, Func<ICharacter, bool> condition = null, Func<ICharacter, int> damageModifier = null)
        : base(name, description, cooldown, executionTime)
    {
        Range = range;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        Condition = condition;
        DamageModifier = damageModifier;
    }

    

    public  void Execute(ICharacter user, ICharacter target)
    {
        int damage = new Random().Next(MinDamage, MaxDamage + 1);
        if (Condition != null && Condition(user))
        {
            damage += DamageModifier != null ? DamageModifier(user) : 0;
        }

        target.StatsComponent.CurrentHealth -= damage;
    }
}

