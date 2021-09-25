using TMPro;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game")]
        public Player player;
        
        [Header("UI")] 
        public TMP_Text ammoText;
        public TMP_Text healthText;

        private void Update()
        {
            ammoText.text = player.Ammo + "/" + player.initialAmmo;
            healthText.text = "Health: " + player.Health;
        }
    }
}
