using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeSOList;
    private List<RecipeSO> _watingRecipeSoList;

    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitngRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        _watingRecipeSoList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer += Time.deltaTime;

        if (_spawnRecipeTimer > _spawnRecipeTimerMax)
        {
            _spawnRecipeTimer = 0;

            if (_watingRecipeSoList.Count < _waitngRecipeMax)
            {
                RecipeSO waitingRecipeSO = _recipeSOList.recipeSOList[UnityEngine.Random.Range(0, _recipeSOList.recipeSOList.Count)];
                _watingRecipeSoList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
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
                    foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSo)
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
                    _watingRecipeSoList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, new EventArgs());
                    OnRecipeSuccess?.Invoke(this, new EventArgs());
                    return;
                }
            }
        }
        //Not Much is found
        //Player did not deliver a correct recipe
        OnRecipeFailed?.Invoke(this, new EventArgs());
    }

    public List<RecipeSO> GetWatingRecipeSOList()
    {
        return _watingRecipeSoList;
    }
}