using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsNewScene : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;
    private Inventory inventaire;
    public bool lancement;

    // Start is called before the first frame update
    void Awake()
    { 
        lancement  = GameManager.Instance.GetLancement();
        if (lancement) {
            lancement = false;
            gameObject.SetActive(false);
        } else {
            if (GameManager.Instance.player == null) {
                GameManager.Instance.player = Instantiate(player);
                GameManager.Instance.player.name = "Player";
                playerScript = GameObject.Find("Player").GetComponent<Player>();
            }
            GameManager.Instance.player.GetComponent<Inventory>().SetInventory(GameManager.Instance.GetInventory());
            GameObject.Find("Content").GetComponent<InventoryUI>().SetInventory(playerScript.GetComponent<Inventory>());
            playerScript.SetStamina(GameManager.Instance.GetStamina()); 
        }
    }

    public bool GetLancement() {
        return lancement;
    }
}
