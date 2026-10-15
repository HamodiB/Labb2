using Labb2.Shared.Abstract;
using Labb2.Shared.SharedGameLogic;

namespace Labb2.BussinessLogic.Concrete.PlayerTypes;

public class PlayerEntity : LevelElement
{
    public int HP { get; set; } = 100;

    public Dice AttackDice { get; } = new Dice(2, 6, 2);
    public Dice DefenceDice { get; } = new Dice(2, 6, 0);

    public PlayerEntity(int x, int y)
    {
        X = x;
        Y = y;

        Symbol = '@';
        Color = ConsoleColor.Blue;
    }
}