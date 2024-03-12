using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    private Player player;
    private Image staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar = GetComponent<Image>();
        staminaBar.fillAmount = player.GetStamina().GetCurrentStamina() / 100;
    }
}
