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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a PLate
                    if (plateKitchenObject.TryAddIgredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //player is not carrying the Plate but somthing else
                    if (GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIgredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //player have nothing
                GetKitchenObject().SetKitchenObjectParents(player);
            }
        }
    }
}