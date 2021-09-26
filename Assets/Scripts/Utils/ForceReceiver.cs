using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    [RequireComponent(typeof(CharacterController))]
    public class ForceReceiver : MonoBehaviour
    {
        [SerializeField] private float deceleration=5;
        [SerializeField]private float mass = 3;
        private Vector3 _intensity;
        private CharacterController _character;

        private void Awake()
        {
            _intensity = Vector3.zero;
            _character = GetComponent<CharacterController>();

        }

        private void Update()
        {
            if (_intensity.magnitude>0.2f)
            {
                _character.Move(_intensity * Time.deltaTime);
                
            }
            _intensity=Vector3.Lerp(_intensity, Vector3.zero, deceleration*Time.deltaTime);
        }

        public void AddForce(Vector3 direction, float force)
        {
            _intensity += direction.normalized * force / mass;
        }
    }
}
