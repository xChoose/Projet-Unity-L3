using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Player player;
    private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar = GetComponent<Image>();
        healthBar.fillAmount = player.GetHealth().GetCurrentHealth() / 100;
    }
}
