using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject inventoryChest;
    private bool ouvre;
    private GameObject slotStock;
    private GameObject nomInventory;
    private GameObject inventoryPlayer;

    void Start() {
        inventoryChest = GameObject.Find("InventoryChest");
        if (inventoryChest != null) {
            inventoryChest.gameObject.SetActive(false);
        }
        inventoryPlayer = GameObject.Find("Player").GetComponent<Player>().GetInventoryPlayer();
    }

    void Update() {
        if (ouvre) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (inventoryChest.gameObject.activeSelf) {
                    inventoryChest.gameObject.SetActive(false);
                } else {
                    inventoryChest.gameObject.SetActive(true);
                }
                inventoryPlayer = GameObject.Find("Player").GetComponent<Player>().GetInventoryPlayer();
                if (inventoryPlayer.gameObject.activeSelf) {
                    inventoryPlayer.gameObject.SetActive(false);
                } else {
                    inventoryPlayer.gameObject.SetActive(true);
                    inventoryPlayer.gameObject.transform.position = new Vector3(966f,310f,0f);
                }
            }
        }
    }
     
    void OnTriggerEnter2D(Collider2D other) {
        ouvre = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        ouvre = false;
    }

    public string GetNom() {
        return nomInventory.name;
    }

    public void SetNom(GameObject newNom) {
        nomInventory = newNom;
    }
 
    public GameObject GetSlot() {
        return slotStock;
    }

    public void SetSlot(GameObject newSlot) {
        slotStock = newSlot;
    }

    public void SwapChest(string name, GameObject slot) {
        InventoryUI inventoryUIC = GameObject.Find("ContentChest").GetComponent<InventoryUI>();
        InventoryUI inventoryUIP = GameObject.Find("Content").GetComponent<InventoryUI>();
        Inventory inventoryC = inventoryChest.GetComponent<Inventory>();
        Inventory inventoryP = GameObject.Find("Inventory").GetComponent<Inventory>();

        if (name == "Player") {

            int index1 = inventoryUIP.GetIndexSlot(slotStock);
            int index2 = inventoryUIC.GetIndexSlot(slot);
            Cases tmp = inventoryP.GetInventory()[index1];
            inventoryP.GetInventory()[index1] = inventoryC.GetInventory()[index2];
            inventoryC.GetInventory()[index2] = tmp;
        }
        if (name == "Chest") {
            int index1 = inventoryUIC.GetIndexSlot(slotStock);
            int index2 = inventoryUIP.GetIndexSlot(slot);
            Cases tmp = inventoryC.GetInventory()[index1];
            inventoryC.GetInventory()[index1] = inventoryP.GetInventory()[index2];
            inventoryP.GetInventory()[index2] = tmp;
        }

        inventoryUIC.ShowItemAndQuantity();
        inventoryUIP.ShowItemAndQuantity();
    }
}
 