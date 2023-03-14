using UnityEngine;

namespace GameSystems.ObjectFactories {
    [CreateAssetMenu(menuName = "Programmer Tools/Object Factories/Specific Factory")]
    public class SpecificFactory : GameObjectFactory 
    {
        ExampleClass Get (ExampleClass prefab) {
            ExampleClass instance = CreateGameObjectInstance(prefab);
            instance.OriginFactory = this;
            return instance;
        }

   

      

        private class ExampleClass : MonoBehaviour{
            SpecificFactory _originFactory;

            public SpecificFactory OriginFactory {
                get => _originFactory;
                set {
                    Debug.Assert(_originFactory == null, "Redefined origin factory!");
                    _originFactory = value;
                }
            }
        
        }
    }
}
