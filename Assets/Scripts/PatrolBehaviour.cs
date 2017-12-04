using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : MonoBehaviour
{
	private Transform _t;
	private Transform _targetPlayer;
	private Transform _target;
	public Transform[] _patrolTargets;
	private int _patrolTargetIndex;
	private NavMeshAgent _agent;
	private bool _isPaused;
	private Animator _animator;

	private void Awake()
	{
		_t = transform;
		_agent = GetComponent<NavMeshAgent>();
		_agent.autoBraking = false;
		_animator = GetComponent<Animator>();
		_isPaused = false;
		SetClosestPatrolTarget();
	}

	private void Update()
	{
		if (_isPaused) return;

		if (_targetPlayer == null)
		{
			if (_patrolTargets.Length > 0)
			{
				if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
				{
					_patrolTargetIndex = (_patrolTargetIndex + 1) % _patrolTargets.Length;
					_target = _patrolTargets[_patrolTargetIndex];
					_agent.destination = _target.position;
					_animator.Play("walk");
				}
			}
		}
		else
		{
			if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
			{
				_animator.Play("meleeAttack");
			}
			else
			{
				_agent.destination = _targetPlayer.position;
				_animator.Play("walk");
			}
		}
	}

	public int _damage;

	public void DealDamage()
	{
		_agent.destination = _targetPlayer.position;
		if (_targetPlayer != null && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
		{
			_targetPlayer.GetComponent<DestructibleBehaviour>().TakeDamage(_damage);
		}
		else
		{
			_animator.Play("idle");
		}
	}

	private void SetTargetPlayer(Transform target)
	{
		_targetPlayer = target;
	}

	private void SetClosestPatrolTarget()
	{
		Vector3 min = new Vector3(100f, 100f, 100f);
		for (int i = 0; i < _patrolTargets.Length; i++)
		{
			if (Vector3.Distance(_t.position, _patrolTargets[i].position) < Vector3.Distance(_t.position, min))
			{
				min = _patrolTargets[i].position;
				_target = _patrolTargets[i];
				_patrolTargetIndex = i;
			}
		}

		_agent.destination = _target.position;
		_animator.Play("walk");
	}
}