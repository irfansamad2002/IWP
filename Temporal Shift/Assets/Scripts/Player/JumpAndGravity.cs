using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndGravity : MonoBehaviour
{

    private bool Grounded = true;
	[SerializeField] private float _groundedRadius = 0.5f;
	[SerializeField] private LayerMask _groundLayers;
	[SerializeField] private float _gravity = -15.0f;
	[SerializeField] private float _jumpHeight = -15.0f;
	[SerializeField] private InputReader _inputReader;
	[SerializeField] CharacterController _controller;


	public float GroundedOffset = -0.14f;

	private float _jumpTimeout = 0.1f;
	private float _fallTimeout = 0.15f;

	public float _verticalVelocity;
	private float _terminalVelocity = 53.0f;


	// timeout deltatime
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;

	private bool _jump = false;

    private void OnEnable()
    {
		_inputReader.JumpEvent += HandleJumpEvent;
    }

    private void OnDisable()
    {
		_inputReader.JumpEvent -= HandleJumpEvent;
	}

    private void HandleJumpEvent()
    {
		//Debug.Log("Jump");
		_jump = true;

	}

	private void Start()
    {
		// reset our timeouts on start
		_jumpTimeoutDelta = _jumpTimeout;
		_fallTimeoutDelta = _fallTimeout;
	}

    // Update is called once per frame
    void Update()
    {
		
		Gravity();
        GroundedCheck();
    }

	private void Gravity()
    {
		if (Grounded)
        {
			// reset the fall timeout timer
			_fallTimeoutDelta = _fallTimeout;

			// stop our velocity dropping infinitely when grounded
			if (_verticalVelocity < 0.0f)
			{
				_verticalVelocity = -2f;
			}
			//Jump
			if (_jump && _jumpTimeoutDelta <= 0.0f)
            {
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				_verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

			}

			// jump timeout
			if (_jumpTimeoutDelta >= 0.0f)
			{
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
        {
			// reset the jump timeout timer
			_jumpTimeoutDelta = _jumpTimeout;

			// fall timeout
			if (_fallTimeoutDelta >= 0.0f)
			{
				_fallTimeoutDelta -= Time.deltaTime;
			}

			_jump = false;

		}

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (_verticalVelocity < _terminalVelocity)
		{
			_verticalVelocity += _gravity * Time.deltaTime;
		}

		//_controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
    }

	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (Grounded) Gizmos.color = transparentGreen;
		else Gizmos.color = transparentRed;

		// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), _groundedRadius);
	}
}
