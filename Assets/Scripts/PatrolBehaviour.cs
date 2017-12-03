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
		_animator = GetComponent<Animator>();
		_isPaused = false;
	}

	private void Update()
	{
		if (_isPaused) return;

		if (_targetPlayer == null)
		{
			_animator.Play("walk");
			if (_patrolTargets.Length > 0)
			{
				if (_target == null || PlanarDistance(_t.position, _target.position) < float.Epsilon)
				{
					_patrolTargetIndex = (_patrolTargetIndex + 1) % _patrolTargets.Length;
					_target = _patrolTargets[_patrolTargetIndex];
					_agent.SetDestination(_target.position);
				}
			}
		}
		else
		{
			if (PlanarDistance(_t.position, _targetPlayer.position) <= _agent.stoppingDistance)
			{
				_agent.isStopped = true;
			}
			else
			{
				_agent.SetDestination(_targetPlayer.position);
				_agent.isStopped = false;
				_animator.Play("walk");
			}
		}
	}

	private float PlanarDistance(Vector3 from, Vector3 to)
	{
		Vector3 planarFrom = new Vector3(from.x, 0f, from.z);
		Vector3 planarTo = new Vector3(to.x, 0f, to.z);
		return Vector3.Distance(planarFrom, planarTo);
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

		_agent.SetDestination(_target.position);
		_animator.Play("walk");
	}

	private void PauseMovement()
	{
		_agent.isStopped = true;
		_isPaused = true;
	}

	private void ResumeMovement()
	{
		_isPaused = false;
	}
}