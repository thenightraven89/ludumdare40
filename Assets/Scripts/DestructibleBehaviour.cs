using UnityEngine;

public class DestructibleBehaviour : MonoBehaviour
{
	[SerializeField]
	private int _hp;

	public int TakeDamage(int damage)
	{
		// subtract resistance to damage and then

		int actualDamage = (int)Mathf.Clamp(damage, 0f, _hp);
		_hp -= actualDamage;

		if (_hp <= 0)
		{
			BeginDestruction();
		}

		// return blood
		return actualDamage;
	}

	private void BeginDestruction()
	{
		Destroy(gameObject);
	}
}