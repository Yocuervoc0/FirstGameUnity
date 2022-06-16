using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    public BarType type;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        switch (type)
        {
            case BarType.health:
                slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case BarType.Mana:
                slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BarType.health:
                slider.value = GameObject.Find("Player")
                    .GetComponent<PlayerController>()
                    .healthPoints;
                break;
            case BarType.Mana:
                slider.value = GameObject.Find("Player")
                    .GetComponent<PlayerController>()
                    .manaPoints;
                break;

        }
    }
}

    public enum BarType
{
    health,
    Mana
}

