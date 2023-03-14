
public class InteractableConveyorBelt : Interactable
{
    private ConveyorBelt _conveyorBelt;


    
    protected override void OnEnableForInheriting()
    {
        _conveyorBelt = GetComponent<ConveyorBelt>();
    }

    

    protected override void TurnOnForInheriting()
    {
        switch(_conveyorBelt.beltDir)
        {
            case BeltDir.Backward:
                _conveyorBelt.SetPushDir(BeltDir.Forward);
                break;
            case BeltDir.Forward:
                _conveyorBelt.SetPushDir(BeltDir.Backward);
                break;
        }
    }
}
