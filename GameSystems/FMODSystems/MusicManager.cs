using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter mainMenuMusic;
    [SerializeField] private StudioEventEmitter tutorialMusic;
    [SerializeField] private StudioEventEmitter inGameMusic;
    [SerializeField] private StudioEventEmitter endGameMusic;
    [SerializeField] private StudioEventEmitter puzzleSolving2Music;
    [SerializeField] private StudioEventEmitter puzzleSolving3Music;
    


    public void SwitchToMainMenuMusic()
    {
        if(mainMenuMusic.IsPlaying()) return;
        mainMenuMusic.Play();
        tutorialMusic.Stop();
        inGameMusic.Stop();
        endGameMusic.Stop();
        puzzleSolving2Music.Stop();
        puzzleSolving3Music.Stop();
    }
    
    public void SwitchToTutorialMusic()
    {
        if(tutorialMusic.IsPlaying()) return;
        mainMenuMusic.Stop();
        tutorialMusic.Play();
        inGameMusic.Stop();
        endGameMusic.Stop();
        puzzleSolving2Music.Stop();
        puzzleSolving3Music.Stop();
    }
    
    public void SwitchToInGameMusic()
    {
        if(inGameMusic.IsPlaying()) return;
        mainMenuMusic.Stop();
        tutorialMusic.Stop();
        inGameMusic.Play();
        endGameMusic.Stop();
        puzzleSolving2Music.Stop();
        puzzleSolving3Music.Stop();
    }
    
    public void SwitchToEndGameMusic()
    {
        if(endGameMusic.IsPlaying()) return;
        mainMenuMusic.Stop();
        tutorialMusic.Stop();
        inGameMusic.Stop();
        endGameMusic.Play();
        puzzleSolving2Music.Stop();
        puzzleSolving3Music.Stop();
    }

    public void SwitchToPuzzleSolving2Music()
    {
        if(puzzleSolving2Music.IsPlaying()) return;
        mainMenuMusic.Stop();
        tutorialMusic.Stop();
        inGameMusic.Stop();
        endGameMusic.Stop();
        puzzleSolving2Music.Play();
        puzzleSolving3Music.Stop();
    }
    
    public void SwitchToPuzzleSolving3Music()
    {
        if(puzzleSolving3Music.IsPlaying()) return;
        mainMenuMusic.Stop();
        tutorialMusic.Stop();
        inGameMusic.Stop();
        endGameMusic.Stop();
        puzzleSolving2Music.Stop();
        puzzleSolving3Music.Play();
    }

    public void SetMusicVolume(float vol)
    {
        RuntimeManager.StudioSystem.setParameterByName("MusicVolume", vol);
    }

    public void SetOtherVolume(float vol)
    {
        RuntimeManager.StudioSystem.setParameterByName("OtherSoundEffectVolume", vol);
    }
}
