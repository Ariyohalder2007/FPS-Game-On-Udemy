using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game")]
        public Player player;
        
        [Header("UI")] 
        public TMP_Text ammoText;
        public TMP_Text healthText;
        [SerializeField] private TMP_Text enemyCountText;
        [SerializeField] private TMP_Text infoText;
        [SerializeField] private Image crossHairImage;
        
        [SerializeField] private GameObject enemyContainer;
        

        private void Start()
        {
            infoText.gameObject.SetActive(false);
            crossHairImage.gameObject.SetActive(true);
        }


        private void Update()
        {
            int aliveEnemies = 0;
            ammoText.text = player.Ammo + "/" + player.initialAmmo;
            healthText.text = "Health: " + player.Health;
           
            foreach (var enemy in enemyContainer.GetComponentsInChildren<Enemy.Enemy>())
            {
                if (!enemy.Killed)
                {
                    aliveEnemies++;
                }
            }
            enemyCountText.text = "Enemies: "+aliveEnemies;
            if (aliveEnemies==0)
            {
                infoText.text = "You Win!";
                infoText.gameObject.SetActive(true);
                crossHairImage.gameObject.SetActive(false);
            }

            if (player.Killed)
            {
                infoText.text = "You Lose!";
                infoText.gameObject.SetActive(true);
                crossHairImage.gameObject.SetActive(false);
            }
        }
    }
}
