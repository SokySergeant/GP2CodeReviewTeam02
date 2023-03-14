using GameSystems.Variables;
using UnityEngine;

namespace GameSystems.Configuration_Settings {
    [CreateAssetMenu(fileName = "New Configuration File",menuName = "Game Settings/Configuration File")]
    public class Configuration : ScriptableObject {
        [SerializeField] private float _volumeLevel = 1f;

        [SerializeField] public ScriptableObject[] Variables;


        public void ApplyVolumeLevel(float newVolume) {
            FloatVariable obj = (FloatVariable)FindSOVariable("MusicVolume");
            if(obj != null)
            {
                obj.Value = newVolume;
            }
        }
        
        
        public ScriptableObject FindSOVariable(string variableName) {
            for (int i = 0; i < Variables.Length; i++) {
                ScriptableObject obj = Variables[i];
                if (obj.name.Equals(variableName)) {
                    return Variables[i];
                }
            }
            return null;
        }
    }
}
