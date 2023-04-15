using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instantce { get; private set; }


    public event EventHandler OnPlayerStart;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternet;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause
    }

    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        Instantce = this;

        _playerInputAction = new PlayerInputAction();
 
        if (PlayerPrefs.HasKey(Constants.PLAYER_PREFS_BINDINGS))
        {
            _playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Constants.PLAYER_PREFS_BINDINGS));
        }

        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Interact.performed += Interact_performed;
        _playerInputAction.Player.InteractAlternet.performed += InteractAlternet_performed;
        _playerInputAction.Player.Pause.performed += Pause_performed;
        _playerInputAction.Player.StartGame.performed += StartGame_performed;


    }


    private void OnDestroy()
    {
        _playerInputAction.Player.Interact.performed -= Interact_performed;
        _playerInputAction.Player.InteractAlternet.performed -= InteractAlternet_performed;
        _playerInputAction.Player.Pause.performed -= Pause_performed;
        _playerInputAction.Player.StartGame.performed -= StartGame_performed;
        _playerInputAction.Dispose();
    }
    private void StartGame_performed(InputAction.CallbackContext obj)
    {
        OnPlayerStart?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
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
            case Binding.Move_Up:
                return _playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return _playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return _playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return _playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return _playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return _playerInputAction.Player.InteractAlternet.bindings[0].ToDisplayString();
            case Binding.Pause:
                return _playerInputAction.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Gamepad_Interact:
                return _playerInputAction.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return _playerInputAction.Player.InteractAlternet.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return _playerInputAction.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        _playerInputAction.Player.Disable();

        InputAction inputAction;
        int buidingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = _playerInputAction.Player.Move;
                buidingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = _playerInputAction.Player.Move;
                buidingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = _playerInputAction.Player.Move;
                buidingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = _playerInputAction.Player.Move;
                buidingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = _playerInputAction.Player.Interact;
                buidingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = _playerInputAction.Player.InteractAlternet ;
                buidingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = _playerInputAction.Player.Pause;
                buidingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = _playerInputAction.Player.Interact;
                buidingIndex = 1;
                break;
            case Binding.Gamepad_InteractAlternate:
                inputAction = _playerInputAction.Player.InteractAlternet;
                buidingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                inputAction = _playerInputAction.Player.Pause;
                buidingIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(buidingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                _playerInputAction.Player.Enable();
                onActionRebound();
                PlayerPrefs.SetString(Constants.PLAYER_PREFS_BINDINGS, _playerInputAction.SaveBindingOverridesAsJson());

            })
            .Start();


    }
}