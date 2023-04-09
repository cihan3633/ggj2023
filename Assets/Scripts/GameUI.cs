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

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            setHealthUI(player.GetHealth());
        }
    }

    void setHealthUI(float health)
    {
        healthSlider.value = health;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, health / player.GetStartingHealth());
    }
}
