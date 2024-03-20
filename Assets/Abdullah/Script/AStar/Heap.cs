using System;


// What does that mean exactly ?
public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    //number nodes in the heap
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];


    }
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;


    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);

        return firstItem;
    }

    //in case we wants to change a pirority of an item.like with the Fcost
    public void UpdateItem(T item)
    {
        SortUp(item);

    }
    public int Count
    {
        get { return currentItemCount; }

    }
    //check if contain the specific item.
    public bool Contains(T item)
    {

        return Equals(items[item.HeapIndex], item);
    }
    void SortDown(T item)
    {
        while (true)
        {
            int ChildIndexLeft = (item.HeapIndex * 2 + 1);
            int ChildIndexRight = (item.HeapIndex * 2 + 2);
            int swapIndex = 0;

            if (ChildIndexLeft < currentItemCount)
            {
                swapIndex = ChildIndexLeft;

                if (ChildIndexRight < currentItemCount)
                {
                    if (items[ChildIndexLeft].CompareTo(items[ChildIndexRight]) < 0)
                    {
                        swapIndex = ChildIndexRight;
                    }

                }
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);

                }
                else
                {
                    return;
                }

            }
            else
            {
                return;
            }
        }
    }
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);

            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;



    }



}


public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }




}

