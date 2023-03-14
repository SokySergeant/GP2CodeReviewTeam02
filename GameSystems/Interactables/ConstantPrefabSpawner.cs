using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class ConstantPrefabSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float timeOffset;
    [SerializeField] private float timeBetween;
    [SerializeField] private GameObject prefab;

    [SerializeField] private StudioEventEmitter spawnEmitter;

    private int _totalPrefabs = 0;
    private List<GameObject> _prefabsWaiting = new List<GameObject>();
    


    private void OnEnable()
    {
        StartCoroutine(SpawnPrefabs());
    }



    private IEnumerator SpawnPrefabs()
    {
        yield return new WaitForSeconds(timeOffset);
        
        while(true)
        {
            yield return new WaitForSeconds(timeBetween);
            
            if(_totalPrefabs < 10)
            {
                var tempPrefab = Instantiate(prefab, spawnPos.position, Quaternion.identity);
                tempPrefab.GetComponent<SpawnedItem>().onReset += Recall;
                tempPrefab.transform.SetParent(transform);
                _totalPrefabs++;
            } else if(_prefabsWaiting.Count > 0)
            {
                var tempPrefab = _prefabsWaiting[0];
                tempPrefab.transform.position = spawnPos.position;
                tempPrefab.SetActive(true);
                _prefabsWaiting.Remove(tempPrefab);
            }

            spawnEmitter.Play();
        }
    }



    private void Recall(GameObject obj)
    {
        obj.SetActive(false);

        _prefabsWaiting.Add(obj);
    }



}
