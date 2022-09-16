using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTreatment : EdgeAbility
{
    public override void Ability(Enemy enemy)
    {
        enemy.Health += 5;
    }

    public override void Ability(Controller player)
    {
        player.Health += 10;
    }
}
