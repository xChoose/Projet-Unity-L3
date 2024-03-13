using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public Items item; 
    public Items item2;
    [SerializeField] private int l;
    [SerializeField] private static int w = 3;
    private List<Cases> inventaire;
    [SerializeField] private GameObject nomInventaire;
    
    void Start()
    {
        if (inventaire == null) {
            inventaire = new List<Cases>();
            InitInventaire();
            AddItem(item,10);
            AddItem(item2,10);
        }
        
        /*for (int i = 0; i < GetTaille(); i++) {
            Debug.Log("Inventaire : " + inventaire);
        }*/
    }

    public GameObject getNameInventory() {
        return nomInventaire;
    }
    
    public List<Cases> GetInventory() {
        return inventaire;
    }

    public void SetInventory(List<Cases> newInventory) {
        inventaire = newInventory;
    }

    public int GetTaille() 
    {
        return l*w;
    }

    public Cases GetCases(int i) {
        return inventaire[i];
    }

    public void InitInventaire() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Items vide = gameManager.FindItemsDictionary(1);
        for (int i = 0; i < l*w; i++) {
            inventaire.Add(new Cases(vide,0));
        }
    }

    public void AddItem(Items item, int newCapacity){
        Cases slot = FindItemCase(item);
        if (slot != null) {
            if (slot.GetCapacity() + newCapacity <= slot.GetItem().GetMaxCapacity()) {
                slot.Add(item, newCapacity);
            }
            else {
                int reste = slot.GetItem().GetMaxCapacity() - slot.GetCapacity();
                newCapacity -= reste;
                slot.Add(item, reste);
                AddItem(item,newCapacity);
            }
        }
        else {
            slot = FindEmptyCase();
            if (slot != null) {
                slot.Add(item, newCapacity);
            } 
        }
    }

    private Cases FindItemCase(Items item) {
        foreach(Cases slot in inventaire) {
            if(slot.GetItem() == item && (slot.GetCapacity() < slot.GetItem().GetMaxCapacity())) {
                return slot;
            }
        }
        return null;
    }

    private Cases FindEmptyCase(){
        foreach(Cases slot in inventaire) {
            if(slot.GetItem().GetIdItem() == 0) {
                return slot;
            }
        }
        return null;
    }

    public void SwapCases(int slot1, int slot2){
        Cases tmp = inventaire[slot1];
        inventaire[slot1] = inventaire[slot2];
        inventaire[slot2] = tmp;
    }

    public void DropItem() {
        InventoryUI inventaireUI = GameObject.Find("Content").GetComponent<InventoryUI>();
        if (inventaireUI.GetSlotStock() != null) {
            int index = inventaireUI.GetIndexSlot(inventaireUI.GetSlotStock());
            inventaire[index].SetCapacity(inventaire[index].GetCapacity()-1);
            if (inventaire[index].GetCapacity() <= 0) {
                inventaire[index].Vider();
            }
            inventaireUI.ShowItemAndQuantity();
        }
    }
}
 