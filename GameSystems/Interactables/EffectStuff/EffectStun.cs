using System.Collections;
using GameSystems.Interactables.EffectStuff;
using GameSystems.PlayerMovement;
using GameSystems.Variables;
using UnityEngine;

public class EffectStun : EffectAbstract
{
    [SerializeField] private BoolVariable playerCanMove;
    [SerializeField] private float playerStunDuration;
    [SerializeField] private BoxCollider selfCol;


    protected override void OnPlayerEnter(GameObject player)
    {
        selfCol.enabled = false;
        StartCoroutine(StunPlayer(player));
    }

    private IEnumerator StunPlayer(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.currentCanMove = false;

        yield return new WaitForSeconds(playerStunDuration);

        playerMovement.currentCanMove = playerCanMove.Value;
    }
}
