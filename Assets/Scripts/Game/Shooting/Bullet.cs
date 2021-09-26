using System;
using UnityEngine;

namespace Game.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 8f;
        private float _lifeTimer;
        public float lifeDuration=2f;
        public int damage = 5;
        public bool _moving;
        public bool shotByPlayer;

        private void Start()
        {
            _lifeTimer = lifeDuration;
            _moving = true;
        }

        private void OnEnable()
        {
            _moving = true;
            _lifeTimer = lifeDuration;
        }

        private void Update()
        {
            if (_moving)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer<=0)
            {
                gameObject.SetActive(false);
                _moving = false;

            }
        }

      
    }
}
