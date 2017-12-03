using System.Collections;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _anticipationLength;

	[SerializeField]
	private float _recoveryLength;

	private Animator _animator;

	[SerializeField]
	private int _damage;

	[SerializeField]
	private float _damageRadius;

	private DestructibleBehaviour _target;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			_target = other.gameObject.GetComponent<DestructibleBehaviour>();
			StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack()
	{
		SendMessageUpwards("PauseMovement");

		while (_target != null && _target.IsAlive() && Vector3.Distance(_target.transform.position, transform.position) < _damageRadius)
		{
			_animator.Play("meleeAttack");
			yield return new WaitForSeconds(_anticipationLength);
			if (Vector3.Distance(_target.transform.position, transform.position) < _damageRadius)
			{
				_target.TakeDamage(_damage);
			}
			yield return new WaitForSeconds(_recoveryLength);
		}

		SendMessageUpwards("ResumeMovement");
	}
}