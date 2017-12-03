using System.Collections;
using UnityEngine;

public class DashBehaviour : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			SendMessageUpwards("PauseMovement", true);
			SendMessageUpwards("SetInvulnerable", true);
			StartCoroutine(Dash(other.transform.position));
		}
	}

	private IEnumerator Dash(Vector3 target)
	{
		yield return null;
	}
}