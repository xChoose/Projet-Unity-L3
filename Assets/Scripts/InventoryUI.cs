using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventaire;
    [SerializeField] private GameObject prefabSlot;
    private List<GameObject> inventaireUI = new List<GameObject>();

    private GameObject slotStock = null;

    void Start() 
    {
        Invoke("InitInventaireUI",0.1f);
    }

    public GameObject GetSlotStock() {
        return slotStock;
    }

    public void SetInventory(Inventory newInventaire) {
        inventaire = newInventaire;
    }

    public void SetSlotStock(GameObject slot) {
        slotStock = slot;
    } 

    public int GetIndexSlot(GameObject slot) {
        int index = 0;
        for (int i = 0; i < inventaire.GetTaille(); i++) {
            if (inventaireUI[i] == slot) {
                index = i;
            }
        }
        return index;
    }

    void InitInventaireUI() 
    {
        for (int i = 0; i < inventaire.GetTaille(); i++) {
            GameObject instantiatedPrefab = Instantiate(prefabSlot, transform.position, Quaternion.identity);
            instantiatedPrefab.GetComponent<CasesUI>().SetInventory(inventaire.getNameInventory());
            inventaireUI.Add(instantiatedPrefab);
        }

        foreach(GameObject obj in inventaireUI) {
            if (obj != null) {
                obj.transform.SetParent(transform);
            } else {
                Debug.LogWarning("Un des objets à mettre en enfant est null !");
            }
        }
        ShowItemAndQuantity();
    }

    public void ShowItemAndQuantity() {
        List<Cases> inventory = inventaire.GetInventory();
        for (int i = 0; i < inventaire.GetTaille(); i++) {
            Image iconItem = inventaireUI[i].transform.GetChild(1).GetComponent<Image>();
            // Si c'est une case vide alors on affiche rien
            if (inventory[i].GetItem().GetIdItem() == 0) {
                iconItem.color = new Color32(0,0,0,0);
            } else {
                iconItem.color = new Color32(255,255,255,255);
                iconItem.sprite = inventory[i].GetItem().GetSprite();
            }

            TextMeshProUGUI quantity = inventaireUI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            // Si c'est une case vide alors on affiche rien
            if (inventory[i].GetItem().GetIdItem() == 0) {
                quantity.text = " ";
            } else {
                quantity.text = inventory[i].GetCapacity().ToString();
            }
        }
    }

    public void SwapCasesUI(GameObject slot) {
        if (slotStock == null) {
            slotStock = slot;
        }
        else {
            int index1 = GetIndexSlot(slotStock);
            int index2 = GetIndexSlot(slot);
            Debug.Log("slotStock : " + index1);
            Debug.Log("slot : " + index2);
            inventaire.SwapCases(index1,index2);
            slotStock = null;

            ShowItemAndQuantity();
        }
    }
}
