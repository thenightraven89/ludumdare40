using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private bool _dash;
	private float _xAxis;
	private float _yAxis;
	private Vector3 _movement;
	private Transform _t;

	private const float SPEED = 4f;
	private const float DASH_SPEED = 20f;
	private const float DASH_TIME = 0.5f;

	public AnimationCurve _dashAnimaton;

	private void Awake()
	{
		_xAxis = 0f;
		_yAxis = 0f;
		_t = transform;
		_isDashing = false;
	}

	private bool _isDashing;

	// Update is called once per frame
	void Update()
	{
		if (_isDashing) return;

		_dash = Input.GetButton("Fire1");
		_xAxis = Input.GetAxis("Horizontal");
		_yAxis = Input.GetAxis("Vertical");

		if (_dash)
		{
			_isDashing = true;
			StartCoroutine(Dash());
		}
		else
		{
			_movement = new Vector3(_xAxis, 0f, _yAxis).normalized * Time.deltaTime * SPEED;
			_t.LookAt(_t.position + _movement);
			_t.Translate(_movement, Space.World);
		}

	}

	private IEnumerator Dash()
	{
		// step #1: decelerated speed boost
		
		float currentDashTime = 0f;
		while (currentDashTime < DASH_TIME)
		{
			float currentDashSpeed = _dashAnimaton.Evaluate(currentDashTime / DASH_TIME) * Time.deltaTime * DASH_SPEED;
			_movement = _t.forward * currentDashSpeed;
			_t.Translate(_movement, Space.World);

			currentDashTime += Time.deltaTime;
			yield return null;
		}


		_isDashing = false;
	}
}