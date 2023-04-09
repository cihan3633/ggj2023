using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Health UI")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Image fillImage;
    [SerializeField] Color fullHealthColor;
    [SerializeField] Color zeroHealthColor;

    [Header("Gun UI")]
    [SerializeField] private Image[] bulletsImage;
    [SerializeField] private Color bulletColor;

    Player player;
    GunController gunController;

    void Start()
    {
        player = FindObjectOfType<Player>();
        gunController = player.GetComponent<GunController>();
    }

    void Update()
    {
        if (player != null)
        {
            SetHealthUI(player.GetHealth());
        }
        if (gunController != null)
        {
            SetBulletsUI(gunController.GetRemainingBullets);
        }
    }

    void SetHealthUI(float health)
    {
        healthSlider.value = health;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health / player.GetStartingHealth());
    }

    void SetBulletsUI(int remainingBullets)
    {
        int bulletsImageLength = bulletsImage.Length;
        for (int i = 0; i < bulletsImageLength; i++)
        {
            if (i < remainingBullets)
            {
                bulletsImage[i].color = bulletColor;
            }
            else bulletsImage[i].color = Color.black;
        }
    }
}
