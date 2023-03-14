using System.Collections;
using FMODUnity;
using UnityEngine;

public class InteractableBreakable : Interactable
{
    [Header("Breakable variables")]
    [SerializeField] private GameObject boxObj;
    [SerializeField] private GameObject deathCol;
    [SerializeField] private StudioEventEmitter breakEmitter;
    [SerializeField] private StudioEventEmitter hitEmitter;
    [SerializeField] private ParticleSystem _onBreakingParticleSystem = null;

    private bool _canBreak;


    protected override void OnEnableForInheriting()
    {
        if(_onBreakingParticleSystem != null)
        {
            _onBreakingParticleSystem.Stop();
        }

        _canBreak = true;
    }
    
    

    protected override void TurnOnForInheriting()
    {
        if(!_canBreak) return;

        StartCoroutine(Break());
    }


    
    private IEnumerator Break()
    {
        _canBreak = false;
        
        breakEmitter.Play();
        boxObj.SetActive(false);
        deathCol.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        if(_onBreakingParticleSystem != null)
        {
            _onBreakingParticleSystem.Play();
            yield return new WaitWhile(() => _onBreakingParticleSystem.isPlaying);
        }
        
        Destroy(gameObject);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        hitEmitter.Play();
    }
}
