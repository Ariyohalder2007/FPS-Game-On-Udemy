using System;
using Game.Shooting;
using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {

        public int health=10;

        public int damage = 5;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Bullet>(out var bullet) && bullet.shotByPlayer)
            {
                health -= bullet.damage;
                if (health<=0)
                {
                    Destroy(gameObject);
                    bullet.gameObject.SetActive(false);
                    bullet._moving = false;
                }
            }
        }
    }
}
