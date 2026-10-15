using Labb2.Shared.Abstract;
using Labb2.Shared.SharedGameLogic;

namespace Labb2.BussinessLogic.Concrete.EnemyTypes;

public class Snake : Enemy
{
    public Snake(string name, char symbol, ConsoleColor color, int hp, int x, int y) : base(name, symbol, color, hp, x, y)
    {
    }
    public override void Update(int playerX, int playerY)
    {
        NextX = X;
        NextY = Y;

        int dx = X - playerX;
        int dy = Y - playerY;

        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance > 2)
        {
            return;
        }

        if (Math.Abs(dx) >= Math.Abs(dy))
        {
            NextX += Math.Sign(dx);
        }
        else
        {
            NextY += Math.Sign(dy);
        }
    }
}