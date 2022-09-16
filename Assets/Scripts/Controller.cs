using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour, IDamageble
{
    [SerializeField] private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, 200);
        }
    }

    private int _damage;
    public int Damage
    {
        get { return _damage; }
        set
        {
            _damage = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    public bool GetCorrectPoint { get; private set; }
    private NavMeshAgent _agent;
    private Animator _animator;

    private GameObject _enemy;

    [SerializeField] private Transform _fightZone;
    private Vector3 _randomPosition;
    private NavMeshPath _navMeshPath;

    private void Awake()
    {
        _agent = GetComponent< NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemy = FindObjectOfType<Enemy>().gameObject;
        _navMeshPath = new NavMeshPath();
    }

    public void Move()
    {
        var distance = (_enemy.transform.position - transform.position).magnitude;

        if (distance > 2)
        {
            _agent.SetDestination(_enemy.transform.position);
            _animator.SetBool("isRun", true);
        }
        else
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttack", true);
        }
    }

    public void Attack()
    {
        if (_enemy.TryGetComponent(out IDamageble damageble))
        {
            damageble.TakeDamage(_damage);
        }
    }

    private void Die()
    {
        _animator.SetBool("isDeath", true);
    }

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
            Die();
        }
    }

    public void RetreatCalculatePath()
    {
        GetCorrectPoint = false;
        while (!GetCorrectPoint)
        {
            NavMeshHit _navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * 5 + _fightZone.position, out _navMeshHit, 10, NavMesh.AllAreas);
            _randomPosition = _navMeshHit.position;

            if (NavMesh.CalculatePath(transform.position, _randomPosition, NavMesh.AllAreas, _navMeshPath))
            {
                if (_navMeshPath.status == NavMeshPathStatus.PathComplete) GetCorrectPoint = true;
                _animator.SetBool("isAttack", false);
                _animator.SetBool("isRun", true);
            }
        }

        _agent.isStopped = false;
    }

    public void Retrect()
    {
        var distance = (_randomPosition - transform.position).magnitude;

        if (distance > 1)
        {
            _agent.SetDestination(_randomPosition);
        }
        else
        {
            GetCorrectPoint = false;
            _animator.SetBool("isRun", false);
            _agent.isStopped = true;
            transform.LookAt(_enemy.transform.position);
        }
    }
}
