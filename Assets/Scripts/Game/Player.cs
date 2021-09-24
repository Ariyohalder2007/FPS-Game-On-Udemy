
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    
    [Header("Gameplay")]
    [SerializeField] private Camera playerCamera;

    public int initialAmmo=12;
    
    public int ammo { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //Using the new Input System
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Fire.performed += Fire;
        ammo = initialAmmo;



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
}
