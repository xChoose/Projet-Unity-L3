using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public Items item; 
    [SerializeField] private static int l = 7;
    [SerializeField] private static int w = 3;
    private List<Cases> inventaire = new List<Cases>();
    private Cases dernierSlot = null;

    // Start is called before the first frame update
    void Start()
    {
        InitInventaire();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetTaille() 
    {
        return l*w;
    }

    public Cases GetCases(int i) {
        return inventaire[i];
    }

    public void InitInventaire() {
        for (int i = 0; i < l*w; i++) {
            inventaire.Add(new Cases(null,0));
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
            if(slot.GetItem() == null) {
                return slot;
            }
        }
        return null;
    }

    /*private void OnClickCases() {
        if (Input.GetMouseButtonDown(0)) {
            if (dernierSlot == null) {
                dernierSlot = //Mettre la case où le click se trouve
            }
            else {
                SwapCases(//Cases actuel de la souris, dernierSlot);
            }
        }
    }*/

    public void SwapCases(Cases slot1, Cases slot2){
        Items item = slot1.GetItem();
        int capacity = slot1.GetCapacity();
        slot1.SetItem(slot2.GetItem());
        slot1.SetCapacity(slot2.GetCapacity());
        slot2.SetItem(item);
        slot2.SetCapacity(capacity);
    }

    public void DropItem() {
        if (dernierSlot != null) {
            dernierSlot.SetCapacity(dernierSlot.GetCapacity()-1);
            if (dernierSlot.GetCapacity() <= 0) {
                dernierSlot.Vider();
            }
        }
    }
}
