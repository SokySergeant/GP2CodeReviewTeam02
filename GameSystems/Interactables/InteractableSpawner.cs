using UnityEngine;

public class InteractableSpawner : Interactable
{
    [SerializeField] private GameObject prefabToSpawn;
    private GameObject _currentPrefab;


    
    protected override void TurnOnForInheriting()
    {
        if(_currentPrefab != null)
        {
            Destroy(_currentPrefab);
        }
        _currentPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
