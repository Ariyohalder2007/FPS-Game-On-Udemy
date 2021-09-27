using System;
using System.Collections;
using Game.Shooting;
using StarterAssets;
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
        public bool Killed { get; private set; }

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
            if (Ammo > 0 )
            {
                
                Ammo--;
                var bulletObj = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObj.transform.position = playerCamera.transform.position;
                bulletObj.transform.forward = playerCamera.transform.forward;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Collect AmmoCrate
            if (other.TryGetComponent<AmmoCrate>(out AmmoCrate crate))
            {
                Ammo += crate.ammo;
                Destroy(other.gameObject);
            }
            // Collect HealthCrate
            else if (other.TryGetComponent<HealthCrate>(out HealthCrate healthCrate))
            {
                Health += healthCrate.health;
                Destroy(other.gameObject);
            }
           
            //Checking for hurt 
            else if (!_isHurt)
            {
                GameObject hazard = null;
             if (other.TryGetComponent<Enemy.Enemy>(out Enemy.Enemy enemy) && enemy.Killed)
             {
                 hazard = enemy.gameObject;
                 Health -= enemy.damage;
                 
             }

             else if (other.TryGetComponent<Bullet>(out var bullet))
             {
                 if (!bullet.shotByPlayer)
                 {
                     hazard = bullet.gameObject;
                     Health -= bullet.damage;
                     bullet.gameObject.SetActive(false);
                     bullet._moving = false;
                 }
             }

             if (hazard)
             {
                 _isHurt = true;
                 var hurtDirection = (transform.position - hazard.transform.position).normalized;
                 var knockbackDir = (hurtDirection + Vector3.up).normalized;
                 GetComponent<ForceReceiver>().AddForce(knockbackDir, knockBackForce);
                 StartCoroutine(HurtRoutine());
             }

             if (Health<=0 && !Killed)
             {
                 Killed = true;
                 OnKill();
             }
            }
        }

        void OnKill()
        {
            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            _playerInputActions.Player.Disable();
            GetComponentInChildren<WeaponSway>().input.Player.Disable();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            
        }

        IEnumerator HurtRoutine()
        {
            yield return new WaitForSeconds(hurtDuration);
            _isHurt = false;
        }

       
    }
}
