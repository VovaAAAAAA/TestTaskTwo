using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeAttackKnife : EdgeAbility
{
    public override void Ability(Enemy enemy)
    {
        enemy.Damage += 10;
    }

    public override void Ability(Controller player)
    {
        player.Damage += 15;
    }
}
