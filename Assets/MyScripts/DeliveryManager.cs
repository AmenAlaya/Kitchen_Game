using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO _recipeSOList;
    private List<RecipeSO> _watingRecipeSoList;

    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitngRecipeMax = 4;

    private void Awake()
    {
        _watingRecipeSoList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer += Time.deltaTime;

        if (_spawnRecipeTimer > _spawnRecipeTimerMax)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_watingRecipeSoList.Count < _waitngRecipeMax)
            {
                RecipeSO watingRecipeSO = _recipeSOList.recipeSOList[Random.Range(0, _recipeSOList.recipeSOList.Count)];
                _watingRecipeSoList.Add(watingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _watingRecipeSoList.Count; i++)
        {
            RecipeSO watingRecipeSo = _watingRecipeSoList[i];
            if (watingRecipeSo.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has Same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach (var recipeKitchenObjectSo in watingRecipeSo.kitchenObjectSOList)
                {
                    //Cycling through all ingredients in the recipe

                    bool ingredientFound = false;
                    foreach (var plateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredients on the plate
                        if (plateKitchenObject == recipeKitchenObjectSo)
                        {
                            //Ingredent Dos much
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //This recipe ingredient wa not on the plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    //player delevered the correct Recipe
                    Debug.Log("Player delevered the correct recipe!");
                    _watingRecipeSoList.RemoveAt(i);
                    return;
                }
            }
        }
    }
}