using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public InputReader inputReader;
    [SerializeField] private CharacterController characterController;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float acceleration = 10f;

    private float _speed;
    private Vector2 _inputDirection;
    private JumpAndGravity jumpAndGravity;

    public Vector3 externalMotion;

    private void Start()
    {
        jumpAndGravity = GetComponent<JumpAndGravity>();
    }

    private void Awake()
    {
        inputReader.MoveEvent += UpdateInputDirection;
    }

    private void OnDestroy()
    {
        inputReader.MoveEvent -= UpdateInputDirection;

    }

    private void UpdateInputDirection(Vector2 input)
    {
        _inputDirection = input;
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        //float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
        float targetSpeed = movementSpeed;

        if (_inputDirection == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;
        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * acceleration);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        Vector3 vec3InputDirection = new Vector3(_inputDirection.x, 0.0f, _inputDirection.y).normalized;

        if (_inputDirection != Vector2.zero)
        {
            vec3InputDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        }


        characterController.Move(vec3InputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, jumpAndGravity._verticalVelocity, 0.0f) * Time.deltaTime);
    }
}
