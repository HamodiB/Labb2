namespace Labb2.Shared.SharedGameLogic;

public class Dice
{
    private int _numberOfDice;
    private int _sidesPerDice;
    private int _modifiers;
    public Dice( int numberOfDice, int sidesPerDice, int modifiers)
    {
        _numberOfDice = numberOfDice;
        _sidesPerDice = sidesPerDice;
        _modifiers = modifiers;
    }

    public int Throw()
    {
        int total = _modifiers;

        for (int i = 0; i < _numberOfDice; i++)
        {
            total += Random.Shared.Next(1, _sidesPerDice + 1);
        }

        return total;
    }
    

    public override string ToString()
    {
        if (_modifiers >= 0)
        {
            return _numberOfDice + "d" + _sidesPerDice + "+" + _modifiers;
        }
        else
        {
            return _numberOfDice + "d" + _sidesPerDice + _modifiers;
        }
    }
}