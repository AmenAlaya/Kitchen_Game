using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    public event EventHandler OnInteractAlternet;

    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Interact.performed += Interact_performed;
        _playerInputAction.Player.InteractAlternet.performed += InteractAlternet_performed;
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