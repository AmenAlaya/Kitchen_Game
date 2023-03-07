using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchenGameObject here
            if (player.HasKitchenObject())
            {
                //Player have kitcheGameObject
                player.GetKitchenObject().SetKitchenObjectParents(this);
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
            }
        }
    }
}