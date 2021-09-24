
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    public Player player;

    [Header("UI")] public TMP_Text ammoText;

    private void Update()
    {
        ammoText.text = player.ammo + "/" + player.initialAmmo;
    }
}
