using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsNewScene : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;
    private Inventory inventaire;
    public bool lancement;

    void Start() {
    }

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


            /*List<Cases> test = GameManager.Instance.player.GetComponent<Inventory>().GetInventory();
            for (int i =0; i < test.Count; i++) {
                Debug.Log("Test :" + test[i].GetItem().GetIdItem());
            }*/


            GameObject.Find("Content").GetComponent<InventoryUI>().SetInventory(GameManager.Instance.player.GetComponent<Inventory>());
            if (GameObject.Find("Portal").GetComponent<Portail>().GetScene() == "SampleScene") {
                GameObject.Find("ContentChest").GetComponent<InventoryUI>().SetInventory(GameObject.Find("Chest").GetComponent<Inventory>());
            }
            playerScript.SetStamina(GameManager.Instance.GetStamina()); 
            playerScript.SetHealth(GameManager.Instance.GetHealth());
        }
    }

    public bool GetLancement() {
        return lancement;
    }
}
