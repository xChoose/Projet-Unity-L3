using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CasesUI : MonoBehaviour, IPointerDownHandler
{
    private Button bouton;
    private GameObject inventory;
    private Chest chest;
    private GameObject slotStock;
    // Start is called before the first frame update
    void Start()
    {
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(() => OnClickButton());
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void OnClickButton()
    {
        InventoryUI inventoryUIScript = GetComponentInParent<InventoryUI>();
        GameObject slot = this.gameObject;
        if (inventoryUIScript.GetSlotStock() == null) {
            inventoryUIScript.SetSlotStock(slot);
        }
        else {
            inventoryUIScript.SwapCasesUI(slot);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Vérifier si l'événement est lié à un objet interactif
        if (eventData.pointerCurrentRaycast.isValid)
        {
            chest = GameObject.Find("Chest").GetComponent<Chest>();
            if (chest.GetSlot() == null) {
                chest.SetSlot(eventData.pointerCurrentRaycast.gameObject);
                slotStock = eventData.pointerCurrentRaycast.gameObject;
                chest.SetNom(inventory);
            } else {
                if ((chest.GetNom() == "Chest") && (eventData.pointerCurrentRaycast.gameObject.name == "Player")) {
                    chest.SwapChest("Chest", eventData.pointerCurrentRaycast.gameObject);
                    slotStock = null;
                    chest.SetSlot(null);
                } else if ((chest.GetNom() == "Player") && (eventData.pointerCurrentRaycast.gameObject.name == "Chest")) {
                    chest.SwapChest("Player", eventData.pointerCurrentRaycast.gameObject);
                    slotStock = null;
                    chest.SetSlot(null);
                } else {
                    chest.SetSlot(null);
                }
            }
            // Faire quelque chose avec l'objet cliqué
            Debug.Log("Vous avez cliqué sur : " + inventory.name);
        }
    }

    public string GetNomInventory() {
        return inventory.name;
    }

    public void SetInventory(GameObject nameInventory) {
        inventory = nameInventory;
    }
}
