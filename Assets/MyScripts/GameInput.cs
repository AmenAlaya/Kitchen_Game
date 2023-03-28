using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instantce { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternet;
    public event EventHandler OnPauseAction;

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
        OnPauseAction?.Invoke(this,EventArgs.Empty);
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
}