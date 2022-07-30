using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider Slider;

    public void SetMaxHealth(int health)
    {
        Slider.maxValue = health;
        Slider.value = health;
    }

    public void SetHealth(int health)
    {
        Slider.value = health;
    }
}
