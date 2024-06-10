using System;
using Gamee.Interface;

public class ComboSkill : Skill
{
    private AttackSkill _attackSkill;
    private MovementSkill _movementSkill;

    public ComboSkill(string name, string description, int cooldown, int executionTime, AttackSkill attackSkill, MovementSkill movementSkill)
        : base(name, description, cooldown, executionTime)
    {
        _attackSkill = attackSkill;
        _movementSkill = movementSkill;
        Range = attackSkill.Range;          
    }
    public void ExecuteMovement(ICharacter user, GameGrid grid, (int x, int y) currentPosition)
    {
        
        _movementSkill.Execute(user, grid, currentPosition);
    }
    public void ExecuteAttack(ICharacter user, ICharacter target)
    {
        _attackSkill.Execute(user, target);
    }
}
