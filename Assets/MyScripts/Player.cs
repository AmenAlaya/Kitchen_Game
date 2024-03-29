using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{

    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnPlayerPickedSomething;
    public static void ResetSatticData()
    {
        OnAnyPlayerSpawned = null;
        OnAnyPlayerSpawned = null;
    }
    public static Player LocalInstance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private LayerMask _counterMask;
    [SerializeField] private Transform _kitchenOptionHoldPoint;

    private Vector3 _lastinteractionDir;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    

    private bool _isWalking;

    


    private void Start()
    {
        GameInput.Instantce.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instantce.OnInteractAlternet += GameInput_OnInteractAlternet;
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
            OnAnyPlayerSpawned?.Invoke(this,EventArgs.Empty);
        }
    }

    private void GameInput_OnInteractAlternet(object sender, EventArgs e)
    {
        if (!KitcheGameManager.Instance.isGamePlaying()) return;

        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlterne(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitcheGameManager.Instance.isGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        if (!IsOwner) { return; }
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = GameInput.Instantce.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastinteractionDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastinteractionDir, out RaycastHit raycastHit, interactDistance, _counterMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter BaseCounter))
            {
                //Clear Counter
                if (BaseCounter != _selectedCounter)
                {
                    SetSelectedCounter(BaseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instantce.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerReduis = 0.7f;
        float playerHight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerReduis, moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move toward moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = (moveDir.x < -0.5f || moveDir.x < +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerReduis, moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the X
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = (moveDir.z < -0.5f || moveDir.z < +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerReduis, moveDirZ, moveDistance);
                //Attempt only Z movement

                if (canMove)
                {
                    //Can move only on The Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move on any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        _isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this._selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = _selectedCounter,
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenOptionHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPlayerPickedSomething?.Invoke(this,EventArgs.Empty);
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}