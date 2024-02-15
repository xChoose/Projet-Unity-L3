using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventaire;
    [SerializeField] private GameObject prefabSlot;
    private List<GameObject> inventaireUI = new List<GameObject>();

    void Start() 
    {
        InitInventaireUI();
    }

    void Update()
    {
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

}
