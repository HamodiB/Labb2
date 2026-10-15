using Labb2.BussinessLogic.Concrete.EnemyTypes;
using Labb2.BussinessLogic.Concrete.LevelObstacles;
using Labb2.Shared.Abstract;

public class LevelData
{
    private List<LevelElement> elements = new List<LevelElement>();
    private string[] lines = Array.Empty<string>();

    public List<LevelElement> Elements
    {
        get { return elements; }
    }

    public int PlayerStartX { get; private set; }
    public int PlayerStartY { get; private set; }

    public int Height
    {
        get { return lines.Length; }
    }

    public void Load(string filename)
    {
        lines = File.ReadAllLines(filename);
        elements.Clear();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char symbol = lines[y][x];

                switch (symbol)
                {
                    case '#':
                        elements.Add(new Wall(x, y));
                        break;

                    case 'r':
                        Rat rat = new Rat("Rat", symbol, ConsoleColor.Gray, 10, x, y);
                        rat.InitializeAttackDice(1, 6, 3);
                        rat.InitializeDefenceDice(1, 6, 1);
                        elements.Add(rat);
                        break;

                    case 's':
                        Snake snake = new Snake("Snake", symbol, ConsoleColor.Green, 25, x, y);
                        snake.InitializeAttackDice(3, 4, 2);
                        snake.InitializeDefenceDice(1, 8, 5);
                        elements.Add(snake);
                        break;

                    case '@':
                        PlayerStartX = x;
                        PlayerStartY = y;
                        break;
                }
            }
        }
    }

    public bool IsInside(int x, int y)
    {
        if (y < 0 || y >= lines.Length)
        {
            return false;
        }

        return x >= 0 && x < lines[y].Length;
    }
}