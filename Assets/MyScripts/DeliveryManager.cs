using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DeliveryManager : NetworkBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeSOList;
    private List<RecipeSO> _watingRecipeSoList;

    private float _spawnRecipeTimer = 4f;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitngRecipeMax = 4;
    private int _successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        _watingRecipeSoList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!IsServer) return;


        _spawnRecipeTimer += Time.deltaTime;

        if (_spawnRecipeTimer > _spawnRecipeTimerMax)
        {
            _spawnRecipeTimer = 0;

            if (_watingRecipeSoList.Count < _waitngRecipeMax)
            {
                int waitingRecipeSOIndex = UnityEngine.Random.Range(0, _recipeSOList.recipeSOList.Count);

                SpawnNewWaitingRecipeClientRpc(waitingRecipeSOIndex);

            }
        }
    }
    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSOIndex)
    {
        RecipeSO waitingRecipeSO = _recipeSOList.recipeSOList[waitingRecipeSOIndex];
        _watingRecipeSoList.Add(waitingRecipeSO);

        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
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
                    DeliverCorrectRecipeServerRpc(i);

                    return;
                }
            }
        }
        //Not Much is found
        //Player did not deliver a correct recipe
        DeliverIncorrectRecipeServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        OnRecipeFailed?.Invoke(this, new EventArgs());
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeSOListIndex)
    {
        DeliverCorrectRecipeClientRpc(waitingRecipeSOListIndex);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int waitingRecipeSOListIndex)
    {
        _successfulRecipesAmount++;
        _watingRecipeSoList.RemoveAt(waitingRecipeSOListIndex);

        OnRecipeCompleted?.Invoke(this, new EventArgs());
        OnRecipeSuccess?.Invoke(this, new EventArgs());
    }

    public List<RecipeSO> GetWatingRecipeSOList()
    {
        return _watingRecipeSoList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return _successfulRecipesAmount;
    }
}