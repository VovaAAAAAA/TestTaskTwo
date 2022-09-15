using UnityEngine;
using UnityEngine.AI;

public class Cat : Enemy, IDamageble
{
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    private Animator _animator;
    private GameObject _player;

    private bool _getCorrectPoint;
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
        _getCorrectPoint = false;
        while (!_getCorrectPoint)
        {
            NavMeshHit _navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * 10 + _fightZone.position, out _navMeshHit, 10, NavMesh.AllAreas);
            _randomPosition = _navMeshHit.position;

            if (NavMesh.CalculatePath(transform.position, _randomPosition, NavMesh.AllAreas, _navMeshPath))
            {
                if (_navMeshPath.status == NavMeshPathStatus.PathComplete) _getCorrectPoint = true;
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
            _agent.isStopped = true;
            _animator.SetBool("isRun", false);
            transform.LookAt(_player.transform.position);
        }
    }

    protected override void Attack(int damage)
    {
        Controller _playerController = _player.GetComponent<Controller>();
        _playerController.Health -= damage;
    }

    private void Update()
    {
        if(!_getCorrectPoint)
            Move();
        else
        {
            Retrect();
        }
            
    }
}
