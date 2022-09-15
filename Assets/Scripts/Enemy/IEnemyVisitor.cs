using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyVisitor
{
    void Visit(Cat cat);
    void Visit(Pig pig);
    void Visit(Bear bear);
}
