using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instantce { get; private set; }



    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternet;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        move_Up,
        move_Down,
        move_Left,
        move_Right,
        Interact,
        InteractAlternate,
        Pause
    }
    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        Instantce = this;

        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Interact.performed += Interact_performed;
        _playerInputAction.Player.InteractAlternet.performed += InteractAlternet_performed;
        _playerInputAction.Player.Pause.performed += Pause_performed;

    }

    private void OnDestroy()
    {
        _playerInputAction.Player.Interact.performed -= Interact_performed;
        _playerInputAction.Player.InteractAlternet.performed -= InteractAlternet_performed;
        _playerInputAction.Player.Pause.performed -= Pause_performed;

        _playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void InteractAlternet_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternet?.Invoke(this, EventArgs.Empty);
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.move_Up:
                return _playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.move_Down:
                return _playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.move_Left:
                return _playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.move_Right:
                return _playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return _playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return _playerInputAction.Player.InteractAlternet.bindings[0].ToDisplayString();
            case Binding.Pause:
                return _playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding)
    {
        _playerInputAction.Player.Disable();

        _playerInputAction.Player.Move.PerformInteractiveRebinding(1)
            .OnComplete(callback =>
            {
                Debug.Log(callback.action.bindings[1].path);
                Debug.Log(callback.action.bindings[1].overridePath);

                _playerInputAction.Player.Enable();
            })
            .Start();
        

    } 
}