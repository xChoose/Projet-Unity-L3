using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cases
{
    private Items item = null;
    private int capacity = 0;

    public Cases(Items item, int capacity) {
        this.item = item;
        this.capacity = capacity;
    }

    public Items GetItem() {
        return item;
    }

    public void SetItem(Items item) {
        this.item = item;
    }

    public int GetCapacity() {
        return capacity;
    }

    public void SetCapacity(int capacity) {
        this.capacity = capacity;
    }

    public void Add(Items newItem, int newCapacity) 
    {
        if (item == null) {
            item = newItem;
        }
        capacity += newCapacity;
    }

    public void Vider()
    {
        item = null;
    }
}
