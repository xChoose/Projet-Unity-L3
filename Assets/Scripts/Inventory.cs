using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Items item; 
    [SerializeField] private static int l = 10;
    [SerializeField] private static int w = 3;
    private List<Cases> inventaire = new List<Cases>();

    // Start is called before the first frame update
    void Start()
    {
        inventaire.Add(new Cases(item, 1));
        Debug.Log("Case" + inventaire[0].getItem());
        for (int i = 1; i < l*w; i++) {
            inventaire.Add(new Cases(null, 0));
            Debug.Log("Case " + i + ": " + inventaire[i].getItem());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Items item, int newCapacity){
        Cases slot = FindItemCase(item);
        if (slot != null) {
            if (slot.getCapacity() + newCapacity <= slot.getItem().getMaxCapacity()) {
                slot.Add(item, newCapacity);
            }
            else {
                int reste = slot.getItem().getMaxCapacity() - slot.getCapacity();
                slot.Add(item, reste);
                newCapacity -= reste;
                AddItem(item,reste);
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
            if(slot.getItem() == item) {
                return slot;
            }
        }
        return null;
    }

    private Cases FindEmptyCase(){
        foreach(Cases slot in inventaire) {
            if(slot.getItem() == null) {
                return slot;
            }
        }
        return null;
    }

    /*public void Delete(int place)
    {
        if (inventaire[place].getItem().getCapacity() <= 0) {
            inventaire[place].Vider();
        }
    }*/
}
