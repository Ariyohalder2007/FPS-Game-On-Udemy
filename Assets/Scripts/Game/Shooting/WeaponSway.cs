using UnityEngine;


namespace Game.Shooting
{
    public class WeaponSway : MonoBehaviour {

        [Header("Sway Settings")]
        [SerializeField] private float smooth;
        [SerializeField] private float multiplier;
        public com.ariyo.StarterAssets.StarterAssets input;

        private void Awake()
        {
            input = new com.ariyo.StarterAssets.StarterAssets();
            input.Player.Look.Enable();
        }

        private void Update()
        {
        
        
            //Get Values Using new Input system

            float mouseX = input.Player.Look.ReadValue<Vector2>().x * Time.deltaTime*multiplier;
            float mouseY = input.Player.Look.ReadValue<Vector2>().y * Time.deltaTime*multiplier;
       

            // calculate target rotation
            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            // rotate 
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }
}