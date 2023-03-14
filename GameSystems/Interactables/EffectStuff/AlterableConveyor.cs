using UnityEngine;

namespace GameSystems.Interactables.EffectStuff {
    public class AlterableConveyor : EffectAbstract {
        private MaterialPropertyBlock _materialProperties;
        private MeshRenderer _renderer;
        private float _offset;
        private float _timeElapsed;
        [SerializeField] private float _beltSpeed;
        private const float MagicMultipler = 3f;
        [SerializeField] private ParticleSystem[] vfx;
    
    
    
        void Start()
        {
            InitializeTexture();
        }
    
    
    
        void FixedUpdate()
        {
            _timeElapsed = Time.fixedDeltaTime;
            AnimateTexture(_timeElapsed);
        }
    
   
    
        protected override void OnPlayerStay(GameObject player)
        {
            player.GetComponent<CharacterController>().Move(transform.forward * _beltSpeed * MagicMultipler * Time.fixedDeltaTime);
        }
        protected override void OnPlayerExit(GameObject player)
        {
            player.GetComponent<PlayerMovement.PlayerMovement>().impact += transform.forward  * _beltSpeed;
        }
        protected override void OnItemStay(GameObject item)
        {
            item.transform.position += transform.forward * _beltSpeed * MagicMultipler  * Time.fixedDeltaTime;
        }

        public void ToggleDirection()
        {
            _beltSpeed = -_beltSpeed;

            for(int i = 0; i < vfx.Length; i++)
            {
                vfx[i].transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
            for(int i = 0; i < vfx.Length; i++)
            {
                vfx[i].Play();
            }
        }
    
        private void AnimateTexture(float _timeElapsed)
        {
            _offset += _timeElapsed * _beltSpeed;
            _materialProperties.SetFloat(
                "_OffsetSample", _offset);
            _renderer.SetPropertyBlock(_materialProperties);
        }
        private void InitializeTexture() {
            _materialProperties = new MaterialPropertyBlock();
            _materialProperties.SetFloat(
                "_OffsetSample", _offset);
            _renderer = GetComponentInChildren<MeshRenderer>();
            _renderer.SetPropertyBlock(_materialProperties);
    
        }
    }
}
