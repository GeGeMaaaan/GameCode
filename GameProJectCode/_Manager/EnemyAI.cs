using System;
using System.Collections.Generic;
using System.Linq;
using Gamee.Interface;

public class EnemyAI
{
    private ICharacter _enemy;
    private Random _random;
    private FightManager _fightManager;
    private GridCell targetPos;
    private ICharacter hero;
    private Queue<Action> _actionQueue;

    public EnemyAI(ICharacter enemy, FightManager fightManager)
    {
        _enemy = enemy;
        _random = new Random();
        _fightManager = fightManager;
        _actionQueue = new Queue<Action>();
    }

    public void TakeTurn()
    {
        if (_actionQueue.Count > 0)
        {
            var action = _actionQueue.Dequeue();
            action.Invoke();
            return;
        }

        var skills = _enemy.SkillComponent.GetAllSkills();
        var attackSkills = skills.OfType<AttackSkill>().ToList();
        var moveSkills = skills.OfType<MovementSkill>().ToList();
        var skill = SelectSkill(attackSkills, moveSkills);
        if (skill != null)
        {
            ExecuteSkill(skill, targetPos);
        }
    }

    private Skill SelectSkill(List<AttackSkill> attackSkills, List<MovementSkill> moveSkills)
    {
        foreach (var skill in attackSkills.OrderBy(x => _random.Next()))
        {
            var combatants = _fightManager.Participants;
            foreach (var combatant in combatants.OrderBy(x => _random.Next()))
            {
                if (combatant.Type != CharacterType.enemy)
                {
                    if (combatant.Type == CharacterType.hero)
                    {
                        hero = combatant;
                    }
                    targetPos = _fightManager._combatantPosDic[combatant];
                    if (_fightManager.IsTargetInRange(skill, _enemy, targetPos))
                    {
                        return skill;
                    }
                }
            }
        }

        if (moveSkills.Any())
        {
            var moveSkill = SelectMovementSkill(moveSkills);
            return moveSkill;
        }

        return null;
    }

    private MovementSkill SelectMovementSkill(List<MovementSkill> moveSkills)
    {
        if (hero == null)
        {
            hero = _fightManager.Participants.FirstOrDefault(c => c.Type == CharacterType.hero);
        }

        if (hero != null)
        {
            var heroPos = _fightManager._combatantPosDic[hero].Coordinate;
            var enemyPos = _fightManager._combatantPosDic[_enemy].Coordinate;

            int deltaX = heroPos.x - enemyPos.x;
            int deltaY = heroPos.y - enemyPos.y;

            (int x, int y) direction = (0, 0);
            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                direction.x = deltaX > 0 ? 1 : -1;
            }
            else
            {
                direction.y = deltaY > 0 ? 1 : -1;
            }

            var suitableMoveSkill = moveSkills.FirstOrDefault(skill =>
                skill.MovementType == MovementType.relative && skill.Direction == direction);

            if (suitableMoveSkill == null)
            {
                var moveToTargetPos = (heroPos.x, heroPos.y);
                suitableMoveSkill = moveSkills.FirstOrDefault(skill =>
                    skill.MovementType == MovementType.absolute && skill.Direction == moveToTargetPos);
            }

            return suitableMoveSkill ?? moveSkills[_random.Next(moveSkills.Count)];
        }

        return moveSkills[_random.Next(moveSkills.Count)];
    }

    private void ExecuteSkill(Skill skill, GridCell targetPos)
    {
        if (skill is AttackSkill)
        {
            _fightManager.ScheduleSkillExecution(skill, _enemy, _fightManager.CurrentTurn + skill.ExecutionTime, targetPos);
        }
        else if (skill is MovementSkill)
        {   
            _fightManager.ScheduleSkillExecution(skill, _enemy, _fightManager.CurrentTurn + skill.ExecutionTime);
        }
    }

    public List<GridCell> GetPlannedAttackTargets()
    {
        var plannedAttacks = new List<GridCell>();

        foreach (var scheduledSkill in _fightManager.GetScheduledSkills())
        {
            if (scheduledSkill.User == _enemy && scheduledSkill.Skill is AttackSkill)
            {
                plannedAttacks.Add(scheduledSkill.Target);
            }
        }

        return plannedAttacks;
    }

    public void AddAction(Action action)
    {
        _actionQueue.Enqueue(action);
    }
}
