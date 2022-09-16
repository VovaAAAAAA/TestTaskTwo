using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeAttack : EdgeAbility
{
    public override void Ability(Enemy enemy)
    {
        enemy.Damage += 5;
    }

    public override void Ability(Controller player)
    {
        player.Damage += 5;
    }
}
