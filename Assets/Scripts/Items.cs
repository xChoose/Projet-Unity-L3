using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Items", menuName = "Items/New Item")] //Menu personnalisé pour crée de nouveau item sur unity directement
public class Items : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private string id_item;
    [SerializeField] private int max_capacity;

    public Items(string id_item, int max_capacity) {
        this.id_item = id_item;
        this.max_capacity = max_capacity;
    }

    public string GetIdItem() {
        return id_item;
    }

    public int GetMaxCapacity() {
        return max_capacity;
    }
}