using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public Items item; 
    [SerializeField] private static int l = 10;
    [SerializeField] private static int w = 3;
    private List<Cases> inventaire = new List<Cases>();
    private Cases dernierSlot = null;

    // Start is called before the first frame update
    void Start()
    {
        string cheminDossierItems = "Assets/Scripts/InstancesItems";
        string[] fichiers = AssetDatabase.FindAssets("", new[] {cheminDossierItems});
        Dictionary<string, Items> instancesItems = new Dictionary<string,Items>();
        int i = 0;

        foreach(string fichierID in fichiers) {
            i++;
            string cheminItem = AssetDatabase.GUIDToAssetPath(fichierID);
            Items itemChargé = AssetDatabase.LoadAssetAtPath<Items>(cheminItem);
            if (itemChargé != null) {
                instancesItems.Add(itemChargé.getIdItem(),itemChargé);
            }
            else {
                Debug.LogError("Impossible de charger l'item à partir du chemin : " + cheminItem);
            }
            Debug.Log("Items " + i + ": " + itemChargé.name);
        }


        /*initInventaire();
        AddItem(item,10);
        SwapCases(inventaire[0], inventaire[3]);
        dernierSlot = inventaire[3];
        DropItem();
        for (int i = 0; i<l*w; i++) {
            Debug.Log("Case " + i + ": " + inventaire[i].getItem() + " Capacity : " + inventaire[i].getCapacity());
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initInventaire() {
        for (int i = 0; i < l*w; i++) {
            inventaire.Add(new Cases(null,0));
        }
    }

    public void AddItem(Items item, int newCapacity){
        Cases slot = FindItemCase(item);
        if (slot != null) {
            if (slot.getCapacity() + newCapacity <= slot.getItem().getMaxCapacity()) {
                slot.Add(item, newCapacity);
            }
            else {
                int reste = slot.getItem().getMaxCapacity() - slot.getCapacity();
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
            if(slot.getItem() == item && slot.getCapacity() < slot.getItem().getMaxCapacity()) {
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

    /*private void onClickCases() {
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
        Items item = slot1.getItem();
        int capacity = slot1.getCapacity();
        slot1.setItem(slot2.getItem());
        slot1.setCapacity(slot2.getCapacity());
        slot2.setItem(item);
        slot2.setCapacity(capacity);
    }

    public void DropItem() {
        if (dernierSlot != null) {
            dernierSlot.setCapacity(dernierSlot.getCapacity()-1);
            if (dernierSlot.getCapacity() <= 0) {
                dernierSlot.Vider();
            }
        }
    }
}
