
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class ShootingEnemy : Enemy
    {
        private Player _player;
        public float shootingInterval = 2f;
        private float _shootingTimer;
        public float shootingDistance = 3;
        

        private void Awake()
        {
            _player=GameObject.FindObjectOfType<Player>();
            _shootingTimer = Random.Range(0, shootingInterval);
        }

        private void Update()
        {
            _shootingTimer -= Time.deltaTime;
            if (_shootingTimer<=0 && Vector3.Distance(transform.position, _player.transform.position)<=shootingDistance)
            {
                _shootingTimer = shootingInterval;
                Shoot();
            }
        }

        void Shoot()
        {
            
            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (_player.transform.position - transform.position).normalized;
            

        }
    }
}
