using UnityEngine;

namespace Game.Shooting
{
    public class HealthCrate : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private float rotationSpeed=180f;
        public int health = 50;

        private void Update()
        {
            container.transform.Rotate(Vector3.up,rotationSpeed*Time.deltaTime);
        }
    }
}
