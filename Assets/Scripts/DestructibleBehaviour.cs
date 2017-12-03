using UnityEngine;

public class DestructibleBehaviour : MonoBehaviour
{
	[SerializeField]
	private int _hp;

	private int _maxHp;

	private Animator _animator;

	[SerializeField]
	private HPDisplay _hpDisplay;

	private void Awake()
	{
		_maxHp = _hp;
		_animator = GetComponent<Animator>();
		_hpDisplay.Set(_maxHp, _hp, 0);
	}

	public int TakeDamage(int damage)
	{
		_hpDisplay.Set(_maxHp, _hp, 0);

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