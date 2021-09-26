using System;
using Game.Shooting;
using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {

        public int health=10;

        public int damage = 5;
        public bool Killed { get; private set; }


        private void OnTriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent<Bullet>(out var bullet) && bullet.shotByPlayer)
            {
                health -= bullet.damage;
                if (health<=0&&!Killed)
                {
                    Killed = true;
                    OnKill();
                    bullet.gameObject.SetActive(false);
                    bullet._moving = false;
                }
            }
        }

        protected virtual void OnKill()
        {
            Destroy(gameObject);
        }
    }
}
