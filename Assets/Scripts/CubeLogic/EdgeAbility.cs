using UnityEngine;

public abstract class EdgeAbility: MonoBehaviour
{
    public abstract void Ability(Enemy enemy);
    public abstract void Ability(Controller player);
}
