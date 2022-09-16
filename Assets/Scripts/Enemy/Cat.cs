using UnityEngine;
using UnityEngine.AI;

public class Cat : Enemy, IDamageble
{
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    private Animator _animator;
    private GameObject _player;

    private Vector3 _randomPosition;
    [SerializeField] private Transform _fightZone;

    private readonly EnemyVisitor _enemyVisitor = new EnemyVisitor();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Controller>().gameObject;
        _navMeshPath = new NavMeshPath();
    }

    public void TakeDamage(int damage)
    {
        if(_health > 0)
        {
            _health -= damage;
            Debug.Log("Cat health = " + _health);
        }
        else
        {
            _health = 0;
            Die();
        }
    }

    protected override void Die()
    {
        _animator.SetBool("isDeath", true);
        _enemyVisitor.Visit(this);
    }

    public override void Move()
    {
        var distance = (_player.transform.position - transform.position).magnitude;

        if (distance > 2)
        {
            _agent.SetDestination(_player.transform.position);
            _animator.SetBool("isRun", true);
        }
        else
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttack", true);
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

    public override void Retrect()
    {
        var distance = (_randomPosition - transform.position).magnitude;

        if (distance > 1)
        {
            _agent.SetDestination(_randomPosition);
        }
        else
        {
            GetCorrectPoint = false;
            _agent.isStopped = true;
            _animator.SetBool("isRun", false);
            transform.LookAt(_player.transform.position);
        }
    }

    protected override void Attack()
    {
        if(_player.TryGetComponent(out IDamageble damageble))
        {
            damageble.TakeDamage(_damage);
        }
    }
}
