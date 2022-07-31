using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider Slider;
    public float health;

    public void SetMaxHealth(float health)
    {
        Slider.maxValue = health;
        Slider.value = health;
    }

    public void SetHealth(float health)
    {
        Slider.value = health;
    }
}
