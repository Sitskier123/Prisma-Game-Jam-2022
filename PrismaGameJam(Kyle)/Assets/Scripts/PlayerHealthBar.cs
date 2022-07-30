using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider Slider;

    public void SetHealth(int health)
    {
        Slider.value = health;
    }
}
