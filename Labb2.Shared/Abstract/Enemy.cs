using System.ComponentModel.DataAnnotations;
using Labb2.Shared.SharedGameLogic;

namespace Labb2.Shared.Abstract;


public abstract class Enemy : LevelElement
{
    public string Name { get; set; }
    public int HP { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public int NextX { get; set; }
    public int NextY { get; set; }

    public Enemy(string name, char symbol, ConsoleColor color, int hp, int x, int y)
    {
        X = x;
        Y = y;

        Symbol = symbol;
        Color = color;

        Name = name;
        HP = hp;

        
    }
    public virtual void InitializeAttackDice(int numberOfDice = 1, int sidesPerDice = 6, int modifiers = 3) 
    => AttackDice = new Dice(numberOfDice, sidesPerDice, modifiers);
    public virtual void InitializeDefenceDice(int numberOfDice = 1, int sidesPerDice = 6, int modifiers = 3) 
    =>  DefenceDice = new Dice(numberOfDice, sidesPerDice, modifiers);
    
    public abstract void Update(int PlayerX, int PlayerY);
}