using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, 150);
        }
    }

    protected int _damage;
    public int Damage
    {
        get { return _damage; }
        set
        {
            _damage = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    public bool GetCorrectPoint { get; protected set; }

    protected abstract void Attack();
    public abstract void Move();
    public abstract void Retrect();
    protected abstract void Die();
}
