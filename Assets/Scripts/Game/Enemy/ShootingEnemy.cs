
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ShootingEnemy : Enemy
    {
        private Player _player;
        public float shootingInterval = 2f;
        private float _shootingTimer;
        private float _chasingTimer;
        public float shootingDistance = 3;
        private NavMeshAgent _agent;
        public float chasingDistance = 12f;
        public float chasingInterval = 2f;
        public AudioSource audioSource;
        public AudioClip deathAudio;
        

        private void Awake()
        {
            _player=GameObject.FindObjectOfType<Player>();
            _shootingTimer = Random.Range(0, shootingInterval);
            _chasingTimer = Random.Range(0, chasingInterval);
            _agent = GetComponent<NavMeshAgent>();
            
        }

        private void Update()
        {
            if (_player.Killed)
            {
                _agent.enabled = false;
                this.enabled = false;
            }
            // Shooting Part
            _shootingTimer -= Time.deltaTime;
            if (_shootingTimer<=0 && Vector3.Distance(transform.position, _player.transform.position)<=shootingDistance)
            {
                _shootingTimer = shootingInterval;
                Shoot();
               
            }
            
            // Chasing Part
            _chasingTimer -= Time.deltaTime;
            if (_chasingTimer<=0 && Vector3.Distance(transform.position, _player.transform.position)<=chasingDistance)
            {
                _agent.SetDestination(_player.transform.position);
                _chasingTimer = chasingInterval;
            }
        }

        void Shoot()
        {
            
            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (_player.transform.position - transform.position).normalized;
            

        }

        protected override void OnKill()
        {
            audioSource.PlayOneShot(deathAudio);
            _agent.enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<CapsuleCollider>().isTrigger = false;
            transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
            this.enabled = false;
        }
    }
}
