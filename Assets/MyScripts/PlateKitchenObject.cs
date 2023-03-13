using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlateCompleteVisual;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectList;

    private List<KitchenObjectSO> _kitchenObjectSOList = new List<KitchenObjectSO>();

    public bool TryAddIgredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!_validKitchenObjectList.Contains(kitchenObjectSO))
        {
            //Not valid in gradient
            return false;
        }

        if (_kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already have that type
            return false;
        }
        else
        {
            _kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }

    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return _kitchenObjectSOList;
    }
}
