using System.Collections;
using FMODUnity;
using GameSystems.AgentLogic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.RespawnSystem {
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private PlayerManager p1;
        [SerializeField] private PlayerManager p2;
        [SerializeField] private Image fadeOutImg;
        [SerializeField] private StudioEventEmitter gameOverEmitter;
        [SerializeField] private Interactable[] populatedInteractablesToReset;

        private bool _p1IsDead = false;
        private bool _p2IsDead = false;

        private bool _canReset = true;

        private void Start() {
            populatedInteractablesToReset = FindObjectsOfType<Interactable>();
        }
    
        private void OnEnable()
        {
            p1.onDeath += () => {_p1IsDead = true;};
            p2.onDeath += () => {_p2IsDead = true;};
        
            p1.onRevive += () => {_p1IsDead = false;};
            p2.onRevive += () => {_p2IsDead = false;};
        }


    
        private void OnDisable()
        {
            p1.onDeath -= () => {_p1IsDead = true;};
            p2.onDeath -= () => {_p2IsDead = true;};
        
            p1.onRevive -= () => {_p1IsDead = false;};
            p2.onRevive -= () => {_p2IsDead = false;};
        }


    
        private void Update()
        {
            if(!_canReset) return;
        
            if(_p1IsDead && _p2IsDead)
            {
                StartCoroutine(ResetRoom());
            }
        }



        private IEnumerator ResetRoom()
        {
            _canReset = false;
        
            gameOverEmitter.Play();

            p1.breakOutOfRespawnCoroutine = true;
            p2.breakOutOfRespawnCoroutine = true;
        
            p1.StopCoroutine(p1.respawnRoutine);
            p2.StopCoroutine(p2.respawnRoutine);

            float currentA = 0f;
            while(fadeOutImg.color.a < 1)
            {
                currentA += Time.fixedDeltaTime;
                fadeOutImg.color = new Color(1, 1, 1, currentA);
                yield return new WaitForFixedUpdate();
            }

            for(int i = 0; i < populatedInteractablesToReset.Length; i++)
            {
                if(populatedInteractablesToReset[i] == null) continue;
                populatedInteractablesToReset[i].ResetSelf();
            }
        
            p1.breakOutOfRespawnCoroutine = false;
            p2.breakOutOfRespawnCoroutine = false;

            p1.OnGameOverRespawn(3f);
            p2.OnGameOverRespawn(3f);

            yield return new WaitUntil(() => !_p1IsDead && !_p2IsDead);
        
            currentA = 1;
            while(fadeOutImg.color.a > 0)
            {
                currentA -= Time.fixedDeltaTime;
                fadeOutImg.color = new Color(1, 1, 1, currentA);
                yield return new WaitForFixedUpdate();
            }

            _canReset = true;
        }
    }
}
