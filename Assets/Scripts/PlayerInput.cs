using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private float _xAxis;
	private float _yAxis;
	private Vector3 _movement;
	private Transform _t;

	private const float SPEED = 4f;

	private void Awake()
	{
		_xAxis = 0f;
		_yAxis = 0f;
		_t = transform;
	}

	// Update is called once per frame
	void Update()
	{
		_xAxis = Input.GetAxis("Horizontal");
		_yAxis = Input.GetAxis("Vertical");

		_movement = new Vector3(_xAxis, 0f, _yAxis).normalized * Time.deltaTime * SPEED;
		_t.Translate(_movement);

	}

	private void Roll()
	{
		// roll forward, drag back
	}
}