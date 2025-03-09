using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
#nullable enable
public class HealthBar : MonoBehaviour
{

    public Slider Slider;
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
