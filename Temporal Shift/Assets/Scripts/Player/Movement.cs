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
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float acceleration = 10f;

    private float _speed;
    private bool isSprinting;
    [HideInInspector]
    public JumpAndGravity jumpAndGravity;

    public Vector3 externalMotion;
    public Vector2 _inputDirection;

    public bool canFly;

    private void Start()
    {
        jumpAndGravity = GetComponent<JumpAndGravity>();
    }

    private void Awake()
    {
        inputReader.MoveEvent += UpdateInputDirection;
        inputReader.SprintEvent += InputReader_SprintEvent;
    }


    private void OnDestroy()
    {
        inputReader.MoveEvent -= UpdateInputDirection;
        inputReader.SprintEvent -= InputReader_SprintEvent;

    }

    private void InputReader_SprintEvent(bool state)
    {
        isSprinting = state;
    }


    private void UpdateInputDirection(Vector2 input)
    {
        _inputDirection = input;
    }

    private void Update()
    {
        //Debug.Log("external movement " + externalMotion);
        Move();
    }


    private void Move()
    {
        float targetSpeed = isSprinting ? sprintSpeed: movementSpeed;
        //float targetSpeed = movementSpeed;

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

        

        // If not in fly mode, apply normal movement with gravity
        characterController.Move(externalMotion * Time.deltaTime);
        characterController.Move(vec3InputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, jumpAndGravity._verticalVelocity, 0.0f) * Time.deltaTime);
        

            
    }
}
