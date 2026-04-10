using System.Collections;
using System.ComponentModel;

namespace DataStructures;

public class CircularBuffer<T> : IEnumerable<T>
{
    // support enqueue, dequeue, peek, count, capacity, and isFull/empty

    public CircularBuffer(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        
        Capacity = capacity;
        _buffer = new T[Capacity];
    }
    
    private readonly T[] _buffer;
    private int _head = 0;
    private int _tail = 0;
    public int Capacity { get; }
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;
    public bool IsFull => Count == Capacity;

    public void Enqueue(T item)
    {
        if (IsFull)
            _head = (_head + 1) % Capacity;
        else
            Count++;
        
        _buffer[_tail] = item;
        _tail = (_tail + 1) % Capacity;

    }

    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Buffer is empty. Cannot dequeue");
        
        var item  = _buffer[_head];
        _buffer[_head] = default;
        _head = (_head + 1) % Capacity;
        Count--;

        return item;

    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Buffer is empty. Cannot peek");
            
        return _buffer[_head];
    }


    public IEnumerator<T> GetEnumerator()
    {
        for(var i = 0; i < Count; i++)
            yield return _buffer[(_head + i) % Capacity];
    }

    IEnumerator IEnumerable.GetEnumerator()
    { 
        return  GetEnumerator();
    }
}