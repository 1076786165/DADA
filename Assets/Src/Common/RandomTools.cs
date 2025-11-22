using System;
using System.Collections.Generic;

public class RandomTool
{
    private class WeightedItem
    {
        public object Item { get; set; }
        public int Weight { get; set; }

        public WeightedItem(object item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }

    private List<WeightedItem> items = new List<WeightedItem>();
    private int totalItemCount = 0;
    private Random random = new Random();

    public RandomTool() { }

    public RandomTool(IEnumerable<int> weights)
    {
        InitWithArray(weights);
    }

    public RandomTool(IEnumerable<Tuple<object, int>> itemsWithWeights)
    {
        InitWithDoubleArr(itemsWithWeights);
    }

    public static int SingleInRange(int from, int max)
    {
        return new Random().Next(from, max + 1);
    }

    public void Add(object item, int weight)
    {
        items.Add(new WeightedItem(item, weight));
        totalItemCount += weight;
    }

    public object Roll()
    {
        if (items.Count == 0) return null;
        
        int ran = random.Next(1, totalItemCount + 1);
        for (int i = 0; i < items.Count; i++)
        {
            int left = ran - items[i].Weight;
            if (left <= 0)
            {
                return items[i].Item;
            }
            ran = left;
        }
        return items[items.Count - 1].Item;
    }

    public object RollAndConsume()
    {
        if (items.Count == 0) return null;
        
        int ran = random.Next(1, totalItemCount + 1);
        for (int i = 0; i < items.Count; i++)
        {
            int left = ran - items[i].Weight;
            if (left <= 0)
            {
                object ret = items[i].Item;
                totalItemCount -= items[i].Weight;
                items.RemoveAt(i);
                return ret;
            }
            ran = left;
        }
        
        object lastItem = items[items.Count - 1].Item;
        items.RemoveAt(items.Count - 1);
        return lastItem;
    }

    public int Count()
    {
        return totalItemCount;
    }

    public void Reset()
    {
        
    }

    public void Remove(object item)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].Item.Equals(item))
            {
                totalItemCount -= items[i].Weight;
                items.RemoveAt(i);
            }
        }
    }

    public void Use(object item)
    {
        
    }

    public void Clear()
    {
        items.Clear();
        totalItemCount = 0;
    }

    public void InitWithArray(IEnumerable<int> weights)
    {
        int index = 1;
        foreach (var weight in weights)
        {
            Add(index++, weight);
        }
    }

    public void InitWithDoubleArr(IEnumerable<Tuple<object, int>> itemsWithWeights)
    {
        foreach (var item in itemsWithWeights)
        {
            Add(item.Item1, item.Item2);
        }
    }
}