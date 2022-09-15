using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    protected int _damage;

    protected abstract void Attack(int damage);
    public abstract void Move();
    protected abstract void Die();
}
