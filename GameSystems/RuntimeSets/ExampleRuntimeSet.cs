using UnityEngine;

namespace GameSystems.RuntimeSets {
    [CreateAssetMenu(menuName = "Programmer Tools/Runtime Sets/Example Runtime Set")]

    public class ExampleRuntimeSet : BaseRuntimeSet<ExampleClass>
    {

    }

    public class ExampleClass : MonoBehaviour {
        public ExampleRuntimeSet RuntimeSet;
        private void OnEnable()
        {
            RuntimeSet.Add(this);
        }

        private void OnDisable()
        {
            RuntimeSet.Remove(this);
        }
    }
}
