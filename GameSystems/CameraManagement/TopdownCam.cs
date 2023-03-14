using Cinemachine;
using GameSystems.Variables;
using UnityEngine;

public class TopdownCam : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private LayerMask whatIsCamBounds;
    [SerializeField] private FloatVariable camAngle;
    [SerializeField] private FloatVariable minZoom;
    [SerializeField] private FloatVariable maxZoom;
    
    [Header("References")]
    private Transform p1;
    private Transform p2;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform parent;
    private Transform _target;

    private float _dist;
    private Vector3 _newTargetPos;
    private Vector3 _newTargetX;
    private Vector3 _newTargetZ;
    private bool _isWithinXBound;
    private bool _isWithinZBound;
    private Vector3 _camDir;
    private Vector3 _newCamOffset;

    private CinemachineVirtualCamera _vCam;
    private CinemachineTransposer _transposer;

    

    private void Start()
    {
        Setup();
    }


    
    private void Setup()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _transposer = _vCam.GetCinemachineComponent<CinemachineTransposer>();
        
        var players = GameObject.FindGameObjectsWithTag("Player");
        p1 = players[0].transform;
        p2 = players[1].transform;

        GetNewTargetPos();
        _target = Instantiate(targetPrefab, _newTargetPos, Quaternion.identity, parent).transform;
        _vCam.m_Follow = _target;
        _vCam.m_LookAt = _target;
        
        SetCamAngle(camAngle.Value);
    }
    


    void Update()
    {
        GetNewTargetPos();
        CheckIfWithinBounds();
        MoveTarget();
        SetCamOffset();
    }



    private void SetCamAngle(float angle)
    {
        Vector3 newOffset = new Vector3(0, 1 / Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad)), -1);
        _camDir = _transposer.m_FollowOffset.normalized;
        _transposer.m_FollowOffset = newOffset;
    }



    private void GetNewTargetPos()
    {
        Vector3 dir = (p1.position - p2.position).normalized;
        _dist = Vector3.Distance(p1.position, p2.position) / 2;
        _newTargetPos = p2.position + (dir * _dist);
    }



    private void CheckIfWithinBounds()
    {
        _newTargetX = _target.position + new Vector3(_newTargetPos.x - _target.position.x, 0, 0);
        _isWithinXBound = Physics.CheckSphere(_newTargetX, 0.1f, whatIsCamBounds);
        
        _newTargetZ = _target.position + new Vector3(0, 0, _newTargetPos.z - _target.position.z);
        _isWithinZBound = Physics.CheckSphere(_newTargetZ, 0.1f, whatIsCamBounds);
    }



    private void MoveTarget()
    {
        if(_isWithinXBound && _isWithinZBound)
        {
            _target.position = Vector3.Lerp(_target.position, _newTargetPos, 0.1f);
        }else if(_isWithinXBound)
        {
            _target.position = Vector3.Lerp(_target.position, _newTargetX, 0.1f);
        }else if(_isWithinZBound)
        {
            _target.position = Vector3.Lerp(_target.position, _newTargetZ, 0.1f);
        }
    }



    private void SetCamOffset()
    {
        float op = Mathf.Tan(60) * _dist;
        op = Mathf.Max(minZoom.Value, op);
        op = Mathf.Min(maxZoom.Value, op);
        _newCamOffset = _camDir * (op);
        
        _transposer.m_FollowOffset = _newCamOffset;
    }
    

    
}