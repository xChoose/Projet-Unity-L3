using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsNewScene : MonoBehaviour
{
    private Player player;
    private Inventory inventaire;
    private bool lancement;
    // Start is called before the first frame update
    void Start()
    {

        lancement  = GameManager.Instance.GetLancement();
        if (lancement) {
            lancement = false;
            gameObject.SetActive(false);
        } else {
            player = GameObject.Find("Player").GetComponent<Player>();
            player.SetStamina(GameManager.Instance.GetStamina()); 
        }
    }

    public bool GetLancement() {
        return lancement;
    }
}
