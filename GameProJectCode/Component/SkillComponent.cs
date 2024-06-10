using Gamee;
using Gamee._Manager;
using Gamee._Models;
using Gamee.Interface;
using Gamee.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

public class SkillComponent : Component
{
    private bool isAFight = false;
    private List<Skill> _skillsLeftHand;
    private List<Skill> _skillsRightHand;
    private List<Skill> _skillsRegular;
    private List<Skill> _skillCell;
    private List<SkillIcon> _skillIcons;
    private ICharacter owner;
    private Skill _selectedSkill;
    private GameServiceContainer _services;

    public SkillComponent(Sprite owner, GameServiceContainer services) : base(owner)
    {
        this.owner = owner as ICharacter;
        _services = services;
        _skillCell = new List<Skill>();
        _skillIcons = new List<SkillIcon>();
        _skillsLeftHand = new List<Skill>();
        _skillsRegular = new List<Skill>();
        _skillsRightHand = new List<Skill>();
        SetHandSkill();
        AddMovementSkills();
        
    }
    public void SetHandSkill()
    {
        var RightWeapon = (owner.InventoryComponent.equipment.RightHand.GetItem()) as WeaponItem;
        var LeftWeapon = (owner.InventoryComponent.equipment.LeftHand.GetItem()) as WeaponItem;
        if(RightWeapon != null)
        {
            _skillsRightHand = RightWeapon.GetSkills();
        }
        else
        {
            _skillsRightHand = WithoutWeaponSkill();
        }
        if(LeftWeapon != null)
        {
            _skillsLeftHand = LeftWeapon.GetSkills();
        }
        else
        {
            _skillsLeftHand = WithoutWeaponSkill();
        }
        
        
    }
    public void SetCellSkill(List<Skill> skills)
    {
        _skillCell = skills;
    }
    public List<Skill> GetCellSkills()
    {
        return _skillCell;
    }
    public List<Skill> GetLeftHandSkills()
    {
        return _skillsLeftHand;
    }
    public List<Skill> GetRightHandSkills()
    {
        return _skillsRightHand;
    }
    public void SetRegularSkills(List<Skill> skills)
    {
        _skillsRegular = skills;
    }
    
    public List<Skill> GetRegularSkills()
    {
        return _skillsRegular;
    }
    public List<Skill> GetAllSkills()
    {
        return _skillsLeftHand.Concat(_skillsRightHand).Concat(_skillsRegular).ToList();
    }
    private List<Skill> WithoutWeaponSkill()
    {
        List<Skill> _skills = new List<Skill>();
        _skills.Add(new AttackSkill("Удар кулоком", "Обычный удар", 3, 3, 2, 4,1));
        _skills.Add(new AttackSkill("Мощный удар кулоком", "Обычный удар", 5, 5, 3, 5,2));
        return _skills;
    }

    private void AddMovementSkills()
    {
        _skillsRegular.Add(new MovementSkill("Перемещение влево", "Перемещает персонажа на одну клетку влево",0, 1, (-1,0), MovementType.relative));
        _skillsRegular.Add(new MovementSkill("Перемещение вправо", "Перемещает персонажа на одну клетку вправо",0, 1, (1,0), MovementType.relative));
    }
    public void AddSkill(Skill skill)
    {
        _skillsRegular.Add(skill);
    }
        
    public void EnterAFight()
    {
        isAFight = true;
    }



    public override void Update()
    {
        SetHandSkill();
    }


    public override void Draw()
    {
       
    }
}
