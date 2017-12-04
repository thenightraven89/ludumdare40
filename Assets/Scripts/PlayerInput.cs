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

	private const int DAMAGE = 1;

	public AnimationCurve _dashAnimaton;
	public AnimationCurve _recoilAnimation;

	private bool _isDashing;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
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
			if (_movement.magnitude > 0f)
			{
				_animator.Play("walk");
			}
			else
			{
				_animator.Play("idle");
			}

			_t.Translate(_movement, Space.World);
		}
	}

	private IEnumerator Dash()
	{
		_animator.Play("dash");
		_isDashing = true;
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

		_isDashing = false;
		_isUnavailableForInput = false;
		//_animator.Play("idle");
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
		if (_isDashing)
		{
			var destructible = other.GetComponent<DestructibleBehaviour>();
			if (destructible != null)
			{
				var blood = destructible.TakeDamage(DAMAGE);

				StopAllCoroutines();
				_isDashing = false;

				_isUnavailableForInput = true;
				StartCoroutine(Recoil());
			}
		}
	}
}