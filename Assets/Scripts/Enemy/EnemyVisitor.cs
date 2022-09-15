using UnityEngine;
using System;
public class EnemyVisitor : IEnemyVisitor
{
    public void Visit(Cat cat)
    {
        Bank.AddCoins(10);
    }

    public void Visit(Pig pig)
    {
        Bank.AddCoins(50);
    }

    public void Visit(Bear bear)
    {
        Bank.AddCoins(100);
    }
}
