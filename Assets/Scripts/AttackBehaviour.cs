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

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}


}