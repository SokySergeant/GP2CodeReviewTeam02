using GameSystems.Interactables.EffectStuff;

namespace GameSystems.Interactables {
    public class InteractableAlterableConveyorBelt : Interactable
    {
        private AlterableConveyor _conveyorBelt;


    
        protected override void OnEnableForInheriting()
        {
            _conveyorBelt = GetComponent<AlterableConveyor>();
        }

    

        protected override void TurnOnForInheriting() {
            _conveyorBelt.ToggleDirection();
        }
    }
}