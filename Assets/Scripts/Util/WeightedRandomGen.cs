using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WeightedRandomGen<T>
{
    private List<WeightedItem<T>> weightedItemList;
    private int totalWeight = 0;
    public int TotalWeight
    {
        get { return totalWeight; }
    }

    public WeightedRandomGen()
    {
        weightedItemList = new List<WeightedItem<T>>();
    }

    public WeightedRandomGen(Dictionary<T,int> itemToWeight) : this()
    {
        foreach(KeyValuePair<T, int> kvp in itemToWeight)
        {
            AddItem(kvp.Key, kvp.Value);
        }
    }
    public void AddItem(T item, int weight)
    {
        this.totalWeight += weight;
        weightedItemList.Add(new WeightedItem<T>(item, totalWeight));
    }
    public T GetRandomItem()
    {
        int val = Random.Range(0, totalWeight);
        T elementToReturn = default(T);

        foreach (WeightedItem<T> wItem in weightedItemList)
        {
            if (val < wItem.weight)
            {
                elementToReturn = wItem.item;
                break;
            }
        }

        return elementToReturn;
    }

    private class WeightedItem<T>
    {
        public WeightedItem(T item, int weight)
        {
            this.item = item;
            this.weight = weight;
        }
        public T item;
        public int weight;
    }
}