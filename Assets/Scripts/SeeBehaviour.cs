using UnityEngine;

public class SeeBehaviour : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			SendMessageUpwards("SetTargetPlayer", other.gameObject.transform);
		}
	}
}