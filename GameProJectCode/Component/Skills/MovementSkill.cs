using Gamee.Interface;
using Microsoft.Xna.Framework;
using Gamee;

public class MovementSkill : Skill
{
    public (int x, int y) Direction { get; private set; }
    public MovementType MovementType {  get; private set; }

    public MovementSkill(string name, string description, int cooldown, int executionTime, (int x, int y) direction, MovementType movementType)
        : base(name, description, cooldown, executionTime)
    {
        MovementType = movementType;
        Direction = direction;
    }

    public void Execute(ICharacter user, GameGrid grid, (int x, int y) currentPosition)
    {
        if (MovementType == MovementType.relative)
        {
            var newX = currentPosition.x + Direction.x;
            var newY = currentPosition.y + Direction.y;
            if (newX < 0 || newX >= grid.GetCells().GetLength(0) || newY < 0 || newY >= grid.GetCells().GetLength(1))
            {
                user.ChangeFightPos(grid, currentPosition.x, currentPosition.y);
            }
            else
            {
                user.ChangeFightPos(grid, newX, newY);
            }
        }
        else if (MovementType == MovementType.absolute)
        {
            user.ChangeFightPos(grid, Direction.x, Direction.y);
        }
    }
}
