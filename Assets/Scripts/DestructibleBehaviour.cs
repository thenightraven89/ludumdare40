using UnityEngine;

public class DestructibleBehaviour : MonoBehaviour
{
	[SerializeField]
	private int _hp;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public int TakeDamage(int damage)
	{
		// subtract resistance to damage and then

		int actualDamage = (int)Mathf.Clamp(damage, 0f, _hp);
		_hp -= actualDamage;

		if (_hp <= 0)
		{
			BeginDestruction();
		}
		else
		{
			// take damage animation
			_animator.Play("takeDamage");
		}

		// return blood
		return actualDamage;
	}

	private void BeginDestruction()
	{
		PSManager.Instance.EmitBlood(30, transform.position);
		Destroy(gameObject);
	}

	public bool IsAlive()
	{
		return _hp > 0;
	}
}