using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;

    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO _plateKitchenObjeectSO;

    private float _spawnPlateTimer;

    private int _plateSpawnedAmount;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;

        if (_spawnPlateTimer > Constants.SPAWN_PLATE_TIMER_MAX)
        {
            _spawnPlateTimer = 0;
            if (_plateSpawnedAmount < Constants.PLATE_SPAWNED_AMOUNT_MAX)
            {
                _plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty Handed
            if (_plateSpawnedAmount > 0)
            {
                //There plate
                _plateSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(_plateKitchenObjeectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}