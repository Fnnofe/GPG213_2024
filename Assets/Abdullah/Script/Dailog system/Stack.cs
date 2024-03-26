
using System;
using static UnityEditor.Experimental.GraphView.Port;

public class Stack<T>
{

    T[] items;
    private int topItem=0;
    public bool IsEmpty()
    {
        return topItem == -1;
    }   
    
    // Start is called before the first frame update
    public Stack(int size)
    {

        items = new T[size];
        topItem = -1;

    }
    public void Push(T item)
    {
        //resize
        if (topItem == items.Length - 1)
        {
            Array.Resize(ref items, topItem*2);
        }

        //add item
        items[++topItem] = item;

    }
    public T Pop()
    {

        T popped = items[topItem];
        items[topItem--] = default;
        return popped;
    }
    public void Clear()
    {
        items= new T[0];
        topItem=0;
    }
    public T ElmentAt(int index)
    {
        return items[index];
    }
    public T Peek() {
        if (IsEmpty())
        {
            return default;
        }

        return items[topItem]; 
    }
}
