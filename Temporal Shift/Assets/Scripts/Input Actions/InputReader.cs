using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action<bool> AbilityEvent;
    public event Action<bool> RewindEvent;
    public event Action<bool> InteractEvent;
    public event Action<bool> Weapon1Event;
    public event Action<bool> Weapon2Event;
    public event Action<bool> Weapon3Event;

    public Vector2 AimPosition { get; private set; }

    public bool JumpState { get; private set; }



    private Controls controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AbilityEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            AbilityEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            InteractEvent?.Invoke(false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnWeapon1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Weapon1Event?.Invoke(true);
        }
        else if (context.canceled)
        {
            Weapon1Event?.Invoke(false);
        }
    }

    public void OnWeapon2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Weapon2Event?.Invoke(true);
        }
        else if (context.canceled)
        {
            Weapon2Event?.Invoke(false);
        }
    }

    public void OnWeapon3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Weapon3Event?.Invoke(true);
        }
        else if (context.canceled)
        {
            Weapon3Event?.Invoke(false);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent?.Invoke();
        }

    }

    public void OnRewindTime(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RewindEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            RewindEvent?.Invoke(false);
        }
    }
}
