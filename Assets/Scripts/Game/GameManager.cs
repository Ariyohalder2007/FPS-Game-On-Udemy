
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    public Player player;

    [Header("UI")] 
    public TMP_Text ammoText;
    public TMP_Text healthText;

    private void Update()
    {
        ammoText.text = player.ammo + "/" + player.initialAmmo;
        healthText.text = "Health: " + player.health;
    }
}
