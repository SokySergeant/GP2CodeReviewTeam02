using System.Collections;
using GameSystems.GameEventLogic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameEvent fadeInEndGame;
    [SerializeField] private GameEvent loadMainMenu;
    [SerializeField] private GameEvent switchToWinMusic;
    [SerializeField] private GameEvent stopPlayerMovement;
    
    
    
    public void WinGame()
    {
        StartCoroutine(WinGameRoutine());
    }



    private IEnumerator WinGameRoutine()
    {
        switchToWinMusic.Raise();
        stopPlayerMovement.Raise();
        fadeInEndGame.Raise();

        yield return new WaitForSeconds(7f);
        
        loadMainMenu.Raise();
    }
}
