using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventaire;
    [SerializeField] private GameObject prefabSlot;
    private List<GameObject> inventaireUI = new List<GameObject>();
    private GameObject slotStock = null;

    void Start() 
    {
        InitInventaireUI();
    }

    void Update()
    {
    }

    GameObject GetSlotStock() {
        return slotStock;
    }

    void SetSlotStock(GameObject slot) {
        slotStock = slot;
    } 

    void InitInventaireUI() 
    {

        for (int i = 0; i < inventaire.GetTaille(); i++) {
            GameObject instantiatedPrefab = Instantiate(prefabSlot, transform.position, Quaternion.identity);
            inventaireUI.Add(instantiatedPrefab);
        }

        foreach(GameObject obj in inventaireUI) {
            if (obj != null) {
                obj.transform.SetParent(transform);
            } else {
                Debug.LogWarning("Un des objets à mettre en enfant est null !");
            }
        }
    }

    public void SwapCasesUI(GameObject slot) {
        if (slotStock == null) {
            slotStock = slot;
        }
        else {
            int index1 = 0;
            int index2 = 0;
            for (int i = 0; i < inventaire.GetTaille(); i++) {
                if (inventaireUI[i] == slotStock) {
                    index1 = i;
                }
                if (inventaireUI[i] == slot) {
                    index2 = i;
                }
            }

            GameObject tmp = inventaireUI[index1];
            inventaireUI[index1] = inventaireUI[index2];
            inventaireUI[index2] = tmp;

            inventaire.SwapCases(index1,index2);
        }
    }

}
