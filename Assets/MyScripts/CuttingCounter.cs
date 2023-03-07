using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _cutKitchenObjectSO;

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

    public override void InteractAlterne(Player player)
    {
        if (HasKitchenObject())
        {
            //There is KitcheObject here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(_cutKitchenObjectSO, this);
        }
    }
}