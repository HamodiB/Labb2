using Labb2.BussinessLogic.Concrete.LevelObstacles;
using Labb2.BussinessLogic.Concrete.PlayerTypes;
using Labb2.Shared.Abstract;
using System.Linq;

namespace Labb2.BussinessLogic.Concrete.GameLogic;

public class Game
{
    private LevelData level;
    private PlayerEntity player;
    private string message = "";

    public Game(LevelData level)
    {
        this.level = level;

        player = new PlayerEntity(
            level.PlayerStartX,
            level.PlayerStartY);
    }

    public void Run()
    {
        Console.CursorVisible = false;
        DrawLevel();

        while (player.HP > 0)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                break;
            }

            int x = player.X;
            int y = player.Y;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    y--;
                    break;

                case ConsoleKey.DownArrow:
                    y++;
                    break;

                case ConsoleKey.LeftArrow:
                    x--;
                    break;

                case ConsoleKey.RightArrow:
                    x++;
                    break;
                    
                default:
                    continue;
            }

            message = "";

            if (x != player.X || y != player.Y)
            {
                MovePlayer(x, y);
            }

            if (player.HP > 0)
            {
                UpdateEnemies();
            }

            DrawLevel();
        }

        Console.SetCursorPosition(0, level.Height + 3);
        Console.CursorVisible = true;

        if (player.HP <= 0)
        {
            Console.WriteLine("Game Over!");
        }
        else
        {
            Console.WriteLine("Spelet avslutades.");
        }
    }

    private LevelElement? FindElement(int x, int y)
    {
        foreach (LevelElement element in level.Elements)
        {
            if (element.X == x && element.Y == y)
            {
                return element;
            }
        }

        return null;
    }

    private void MovePlayer(int x, int y)
    {
        if (!level.IsInside(x, y))
        {
            return;
        }

        LevelElement? obstacle = FindElement(x, y);

        if (obstacle is Enemy enemy)
        {
            Fight(enemy, true);
        }
        else if (obstacle == null)
        {
            player.X = x;
            player.Y = y;
        }
    }

    private void UpdateEnemies()
    {
        List<Enemy> enemies = level.Elements
            .OfType<Enemy>()
            .ToList();

        foreach (Enemy enemy in enemies)
        {
            if (enemy.HP <= 0)
            {
                continue;
            }

            enemy.Update(player.X, player.Y);
            MoveEnemy(enemy);

            if (player.HP <= 0)
            {
                break;
            }
        }
    }

    private void MoveEnemy(Enemy enemy)
    {
        int x = enemy.NextX;
        int y = enemy.NextY;

        if (!level.IsInside(x, y))
        {
            return;
        }

        if (x == player.X && y == player.Y)
        {
            Fight(enemy, false);
            return;
        }

        if (FindElement(x, y) == null)
        {
            enemy.X = x;
            enemy.Y = y;
        }
    }

    private void Fight(Enemy enemy, bool playerAttacksFirst)
    {
        if (playerAttacksFirst)
        {
            PlayerAttack(enemy);

            if (enemy.HP > 0)
            {
                EnemyAttack(enemy);
            }
        }
        else
        {
            EnemyAttack(enemy);

            if (player.HP > 0)
            {
                PlayerAttack(enemy);
            }
        }

        if (enemy.HP <= 0)
        {
            level.Elements.Remove(enemy);
            message += enemy.Name + " dog. ";
        }
    }

    private void PlayerAttack(Enemy enemy)
    {
        int attack = player.AttackDice.Throw();
        int defence = enemy.DefenceDice.Throw();
        int damage = Math.Max(0, attack - defence);

        enemy.HP = Math.Max(0, enemy.HP - damage);

        message += "Du gjorde " + damage
            + " skada på " + enemy.Name + ". ";
    }

    private void EnemyAttack(Enemy enemy)
    {
        int attack = enemy.AttackDice.Throw();
        int defence = player.DefenceDice.Throw();
        int damage = Math.Max(0, attack - defence);

        player.HP = Math.Max(0, player.HP - damage);

        message += enemy.Name + " gjorde "
            + damage + " skada på dig. ";
    }

    private void DrawLevel()
    {
        Console.Clear();

        foreach (LevelElement element in level.Elements)
        {
            int dx = element.X - player.X;
            int dy = element.Y - player.Y;

            bool isVisible = dx * dx + dy * dy <= 25;

            if (element is Wall wall)
            {
                if (isVisible)
                {
                    wall.Seen = true;
                }

                if (wall.Seen)
                {
                    wall.Draw();
                }
            }
            else if (isVisible)
            {
                element.Draw();
            }
        }

        player.Draw();

        Console.SetCursorPosition(0, level.Height + 1);
        Console.Write("HP: " + player.HP);

        Console.SetCursorPosition(0, level.Height + 2);
        Console.Write(message);
    }
}