using System.Collections;
using Game.Shooting;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
    
        [Header("Gameplay")]
        [SerializeField] private Camera playerCamera;

        [SerializeField] private float knockBackForce=10f;

        private bool _isHurt;
        public float hurtDuration = .5f;
    

        public int initialAmmo=12;
        public int initialHealth = 100;
    
        [HideInInspector] public int Ammo { get; private set; }
        [HideInInspector] public int Health { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            //Using the new Input System
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Fire.performed += Fire;
            Ammo = initialAmmo;
            Health = initialHealth;



        }

        //<summary>Get bullet from object pooler and set the position</summary>
        void Fire(InputAction.CallbackContext context)
        {
            if (Ammo > 0)
            {

                Ammo--;
                GameObject bulletObj = ObjectPoolingManager.Instance.GetBullet();
                bulletObj.transform.position = playerCamera.transform.position;
                bulletObj.transform.forward = playerCamera.transform.forward;
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Collect AmmoCrate
            if (hit.gameObject.TryGetComponent<AmmoCrate>(out AmmoCrate crate))
            {
                Ammo += crate.ammo;
                Destroy(hit.gameObject);
            }
            else if (hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if (!_isHurt)
                { 
                    Health -= enemy.damage;
                    _isHurt = true;
                    Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                    Vector3 knockbackDir = (hurtDirection + Vector3.up).normalized;
                    GetComponent<ForceReceiver>().AddForce(knockbackDir, knockBackForce);
                    StartCoroutine(HurtRoutine());
                }
            }
        }

        IEnumerator HurtRoutine()
        {
            yield return new WaitForSeconds(hurtDuration);
            _isHurt = false;
        }
    }
}
