using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CollisionType
{
    None,
    Solid
}
public enum ActiveType
{
    NearPlayer,
    OnlyClick,
    None,
}
public enum ItemType
{
    Weapon,
    Peaceable,
    Equitable,
    Consumable,
    None,
}
public enum BodyPart
{
    Boots,
    Arm,
    Body,
    Head,
    Decoration
}
public enum MainStats
{
    strength,
    diplomacy,
    dexterity,
    intelligence,
    medicine,
    gun_mastery
}
public enum SkillIconPosition
{
    left,
    mid,
    right,
}
public enum CharacterType
{
    enemy,
    ally,
    hero,
}
public enum MovementType
{
    absolute,
    relative
}
