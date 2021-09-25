
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    
    [Header("Gameplay")]
    [SerializeField] private Camera playerCamera;

    [SerializeField] private float knockbackForce=10f;

    private bool isHurt;
    public float hurtDuration = .5f;
    

    public int initialAmmo=12;
    public int initialHealth = 100;
    
   [HideInInspector] public int ammo { get; private set; }
   [HideInInspector] public int health { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //Using the new Input System
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Fire.performed += Fire;
        ammo = initialAmmo;
        health = initialHealth;



    }

    //<summary>Get bullet from object pooler and set the position</summary>
    void Fire(InputAction.CallbackContext context)
    {
        if (ammo > 0)
        {

            ammo--;
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
            ammo += crate.ammo;
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (!isHurt)
            { 
                health -= enemy.damage;
                isHurt = true;
                Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                Vector3 knockbackDir = (hurtDirection + Vector3.up).normalized;
                StartCoroutine(HurtRoutine());
            }
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }
}
