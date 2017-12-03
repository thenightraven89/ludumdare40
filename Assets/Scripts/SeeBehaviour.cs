using UnityEngine;

public class SeeBehaviour : MonoBehaviour
{
	private Ray _ray;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			SendMessageUpwards("SetTargetPlayer", other.gameObject.transform);
		}

		//_ray = new Ray(transform.position, transform.forward);
		//var sightCollider = GetComponent<BoxCollider>();
		//RaycastHit[] hits = Physics.RaycastAll(_ray, sightCollider.size.z);

		//foreach (var hit in hits)
		//{
		//	Debug.Log(hit.collider.gameObject.name);
		//}

		//if (hits != null && hits.Length > 0 && hits[0].collider == other)
		//{
		//	if (other.gameObject.CompareTag("Player"))
		//		SendMessageUpwards("SetTargetPlayer", other.transform);
		//}
		//// else it means that it's behind a wall so just ignore it
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(_ray);
	}
}