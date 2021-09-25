using UnityEngine;

namespace Game.Shooting
{
    public class AmmoCrate : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private float rotationSpeed=180f;
        public int ammo = 12;

        private void Update()
        {
            container.transform.Rotate(Vector3.up,rotationSpeed*Time.deltaTime);
        }
    }
}
