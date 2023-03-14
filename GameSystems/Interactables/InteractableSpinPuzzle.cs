using UnityEngine;

public class InteractableSpinPuzzle : Interactable
{
    [Header("Spin Puzzle Variables")]
    [SerializeField] private float spinSpeed;

    private bool _doSpin;
    private float _currentZ;

    private RectTransform _bottomPiece;
    private RectTransform _topPiece;
    
    
    
    private void FixedUpdate()
    {
        if(!_doSpin) return;

        _currentZ += spinSpeed * Time.fixedDeltaTime;
        _topPiece.rotation = Quaternion.Euler(0, 0, _currentZ);
        
        if(_currentZ > 360)
        {
            _currentZ = 0;
        }
    }

    

    protected override void Interacted()
    {
        if(!_canInteract) return;
        
        _doSpin = false;
        
        if(Mathf.Abs(_topPiece.eulerAngles.z - _bottomPiece.eulerAngles.z) < 10)
        {
            CompletedInteraction();
        } else
        {
            StartCooldown();
        }
    }

    

    protected override void OnPlayerEnter()
    {
        _bottomPiece = _tempUiPrefab.GetComponentsInChildren<RectTransform>()[1];
        _topPiece = _tempUiPrefab.GetComponentsInChildren<RectTransform>()[2];

        _bottomPiece.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        _topPiece.rotation = Quaternion.Euler(0, 0, _currentZ);
        
        _doSpin = true;
    }


    protected override void OnPlayerExit()
    {
        _doSpin = false;
    }
    

    
}
