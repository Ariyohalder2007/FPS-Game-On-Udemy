using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game")]
        public Player player;

        [SerializeField] private float resetTimer=3f;
        private bool gameOver;
        [SerializeField] private GameObject enemyContainer;



        [Header("UI")]
        [SerializeField] private GameObject mobileUI;
        public TMP_Text ammoText;
        public TMP_Text healthText;
        [SerializeField] private TMP_Text enemyCountText;
        [SerializeField] private TMP_Text infoText;
        [SerializeField] private Image crossHairImage;




        private void Start()
        {
            if (Application.platform==RuntimePlatform.Android)
            {
                mobileUI.SetActive(true);
            }
            infoText.gameObject.SetActive(false);
            crossHairImage.gameObject.SetActive(true);
	    Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }


        private void Update()
        {
            int aliveEnemies = 0;
            ammoText.text = player.Ammo+"";
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
                gameOver = true;
            }

            if (player.Killed)
            {
                infoText.text = "You Lose!";

                gameOver = true;
            }

            if (gameOver)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                infoText.gameObject.SetActive(true);
                crossHairImage.gameObject.SetActive(false);
                resetTimer -= Time.deltaTime;
                infoText.text += "\n Reloading in "+Mathf.RoundToInt(resetTimer)+"...";
                if (resetTimer<=0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

            }
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
