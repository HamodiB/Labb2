using Labb2.Shared.Abstract;
using Labb2.Shared.SharedGameLogic;

namespace Labb2.BussinessLogic.Concrete.EnemyTypes;

public class Rat : Enemy
{
    private int _randomPosDirection = 4;

    public Rat(string name, char symbol, ConsoleColor color, int hp, int x, int y) : base(name, symbol, color, hp, x, y)
    {
    }

    public override void Update(int playerX, int playerY)
    {
        NextX = X;
        NextY = Y;

        int direction = Random.Shared.Next(_randomPosDirection);

        switch (direction)
        {
            case 0:
                NextY--;
                break;

            case 1:
                NextY++;
                break;

            case 2:
                NextX--;
                break;

            case 3:
                NextX++;
                break;
        }
    }
}