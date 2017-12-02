using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private bool _dash;
	private float _xAxis;
	private float _yAxis;
	private Vector3 _movement;
	private Transform _t;

	private const float SPEED = 5f;

	private const float DASH_SPEED = 40f;
	private const float DASH_TIME = 0.25f;

	private const float RECOIL_SPEED = 10f;
	private const float RECOIL_TIME = 0.75f;

	public AnimationCurve _dashAnimaton;
	public AnimationCurve _recoilAnimation;

	private int _damage = 1;

	private void Awake()
	{
		_xAxis = 0f;
		_yAxis = 0f;
		_t = transform;
		_isUnavailableForInput = false;
	}

	private bool _isUnavailableForInput;

	// Update is called once per frame
	void Update()
	{
		if (_isUnavailableForInput) return;

		_xAxis = Input.GetAxis("Horizontal");
		_yAxis = Input.GetAxis("Vertical");
		_dash = Input.GetButton("Fire1");

		_movement = new Vector3(_xAxis, 0f, _yAxis).normalized * Time.deltaTime * SPEED;
		_t.LookAt(_t.position + _movement);

		if (_dash)
		{
			_isUnavailableForInput = true;
			StartCoroutine(Dash());
		}
		else
		{
			_t.Translate(_movement, Space.World);
		}

	}

	private IEnumerator Dash()
	{	
		float currentDashTime = 0f;
		float currentDashSpeed = 0f;
		while (currentDashTime < DASH_TIME)
		{
			currentDashSpeed = _dashAnimaton.Evaluate(currentDashTime / DASH_TIME) * Time.deltaTime * DASH_SPEED;
			_movement = _t.forward * currentDashSpeed;
			_t.Translate(_movement, Space.World);

			currentDashTime += Time.deltaTime;
			yield return null;
		}

		_isUnavailableForInput = false;
	}

	private IEnumerator Recoil()
	{
		float currentRecoilTime = 0f;
		float currentRecoilSpeed = 0f;
		while (currentRecoilTime < RECOIL_TIME)
		{
			currentRecoilSpeed = _recoilAnimation.Evaluate(currentRecoilTime / RECOIL_TIME) * Time.deltaTime * RECOIL_SPEED;
			_movement = -_t.forward * currentRecoilSpeed;
			_t.Translate(_movement, Space.World);

			currentRecoilTime += Time.deltaTime;
			yield return null;
		}

		_isUnavailableForInput = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		var destructible = other.GetComponent<DestructibleBehaviour>();
		if (destructible != null)
		{
			var blood = destructible.TakeDamage(_damage);
		}

		StopAllCoroutines();

		_isUnavailableForInput = true;
		StartCoroutine(Recoil());
	}
}