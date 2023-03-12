using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnstateChangedEventArgs> OnStateChanged;

    public class OnstateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        frying,
        fried,
        burned
    }

    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BuringRecipeSO[] _buringRecipeSOArray;

    private State _state;

    private float fryingTimer;
    private FryingRecipeSO _fryingRecipeSO;
    private float _burningTimer;
    private BuringRecipeSO _buringRecipeSO;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;

                case State.frying:

                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / _fryingRecipeSO.FryingTimerMax
                    });

                    if (fryingTimer > _fryingRecipeSO.FryingTimerMax)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_fryingRecipeSO.ouput, this);

                        _burningTimer = 0;
                        _state = State.fried;

                        _buringRecipeSO = GetBurningRecipeSOInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnstateChangedEventArgs
                        {
                            state = _state
                        });
                    }

                    break;

                case State.fried:
                    _burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _burningTimer / _buringRecipeSO.BurningTimerMax
                    });

                    if (_burningTimer > _buringRecipeSO.BurningTimerMax)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_buringRecipeSO.ouput, this);

                        _burningTimer = 0;
                        _state = State.burned;
                        OnStateChanged?.Invoke(this, new OnstateChangedEventArgs
                        {
                            state = _state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                    break;

                case State.burned: break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchenGameObject here
            if (player.HasKitchenObject())
            {
                //Player have kitcheGameObject
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Plate carry somthing that can be fried
                    player.GetKitchenObject().SetKitchenObjectParents(this);

                    _fryingRecipeSO = GetFryingRecipeSOInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnstateChangedEventArgs
                    {
                        state = _state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / _fryingRecipeSO.FryingTimerMax
                    }); ;
                }
            }
            else
            {
                //Player Have nothing
            }
        }
        else
        {
            //There is kitchenGameObject here
            if (player.HasKitchenObject())
            {
                //Player have kitcheGameObject
            }
            else
            {
                //player have nothing
                GetKitchenObject().SetKitchenObjectParents(player);

                _state = State.Idle;

                OnStateChanged?.Invoke(this, new OnstateChangedEventArgs
                {
                    state = _state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOInput(inputKitchenObjectSO);
        fryingRecipeSO = GetFryingRecipeSOInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.ouput;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var fryingRecipeSO in _fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BuringRecipeSO GetBurningRecipeSOInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var BurningRecipeSO in _buringRecipeSOArray)
        {
            if (BurningRecipeSO.input == inputKitchenObjectSO)
            {
                return BurningRecipeSO;
            }
        }
        return null;
    }
}