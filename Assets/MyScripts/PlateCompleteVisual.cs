using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [System.Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject _platekitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _KitchenObjectSOGameObjectList;

    private void Start()
    {
        _platekitchenObject.OnIngredientAdded += PlatekitchenObject_OnIngredientAdded;
        foreach (var KitchenObjectSOGameObject in _KitchenObjectSOGameObjectList)
        {
            KitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlatekitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var KitchenObjectSOGameObject in _KitchenObjectSOGameObjectList)
        {
            if (e.kitchenObjectSO == KitchenObjectSOGameObject.kitchenObjectSO)
            {
                KitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }


}
