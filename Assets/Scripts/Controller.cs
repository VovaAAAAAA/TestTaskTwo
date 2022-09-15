using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    [SerializeField] private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    private int _damage;
    private NavMeshAgent _agent;
    private Animator _animator;

    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _finalPosition;

    private void Awake()
    {
        _agent = GetComponent< NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("isRun", true);
        _agent.SetDestination(_startPosition.transform.position);
    }

    public void Move()
    {
        
    }
}
