using FMODUnity;
using GameSystems.AgentLogic;
using GameSystems.PlayerMovement;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] private Transform deathParticles;
    [SerializeField] private StudioEventEmitter deathEmitter;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var crawler = other.GetComponent<PlayerCrawlerMovement>();
            if(crawler != null)
            {
                if(crawler.isInPipe) return;
            }
            
            if(deathParticles != null)
            {
                Vector3 deathPos = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                deathParticles.position = deathPos;
                deathParticles.GetComponent<ParticleSystem>().Play();
            }
            
            deathEmitter.Play();
            other.GetComponent<PlayerManager>().KillPlayer();
        }
    }

    
}