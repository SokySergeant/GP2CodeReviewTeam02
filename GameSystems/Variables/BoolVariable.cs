using UnityEngine;

namespace GameSystems.Variables
{
    [CreateAssetMenu(menuName = "Designer Tools/Variables/Bool Variable")]
    public class BoolVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public bool Value;

        public void SetValue(bool value)
        {
            Value = value;
        }

        public void SetValue(BoolVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(bool amount)
        {
            Value = amount;
        }

        public void ApplyChange(BoolVariable amount)
        {
            Value = amount.Value;
        }
    }
}