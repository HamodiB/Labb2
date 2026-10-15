using Labb2.Shared.Abstract;

namespace Labb2.BussinessLogic.Concrete.LevelObstacles;

public class Wall : LevelElement
{
    public bool Seen { get; set; }

    public Wall(int x, int y)
    {
        X = x;
        Y = y;

        Symbol = '#';
        Color = ConsoleColor.Gray;
    }
}
