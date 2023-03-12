using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter _plateCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateVisualGameObjectList = new List<GameObject>();

    private void Start()
    {
        _plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        _plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
        _plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);

        _plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}