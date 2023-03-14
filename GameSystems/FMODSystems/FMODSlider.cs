using FMODUnity;
using GameSystems.GameEventLogic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundSliderParameter
{
    MusicVolume,
    OtherVolume
}

public class FMODSlider : MonoBehaviour
{
    [SerializeField] private GameFloatEvent setNewFMODParameter;
    [SerializeField] private SoundSliderParameter sliderParameter;
    private Slider _slider;


    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener((e) => setNewFMODParameter.Raise(e));

        float sliderVal = 0.5f;
        
        switch(sliderParameter)
        {
            case SoundSliderParameter.MusicVolume:
                RuntimeManager.StudioSystem.getParameterByName("MusicVolume", out sliderVal);
                break;
            case SoundSliderParameter.OtherVolume:
                RuntimeManager.StudioSystem.getParameterByName("OtherSoundEffectVolume", out sliderVal);
                break;
        }

        _slider.value = sliderVal;
    }
}
