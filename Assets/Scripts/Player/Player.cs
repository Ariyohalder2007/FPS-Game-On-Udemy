
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    
    [Header("Gameplay")]
    [SerializeField] private Camera playerCamera;

    public int ammo;
    // Start is called before the first frame update
    void Start()
    {
        //Using the new Input System
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Fire.performed += Fire;

        ammo = 10;
    }

    //<summary>Get bullet from object pooler and set the position</summary>
    void Fire(InputAction.CallbackContext context)
    {
        GameObject bulletObj=ObjectPoolingManager.Instance.GetBullet();
        bulletObj.transform.position = playerCamera.transform.position;
        bulletObj.transform.forward = playerCamera.transform.forward;
    }
}
