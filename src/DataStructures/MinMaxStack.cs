namespace DataStructures;

public class MinMaxStack<T> where T : IComparable<T>
{
    private readonly Stack<(T Value, T Min, T Max)> _stack = new();
    
    private int Count => _stack.Count;

    public void Push(T? value)
    {
        if (value is null)
            throw new InvalidOperationException("No value. Cannot push null.");

        if (Count == 0)
        {
            _stack.Push((value, value, value));
            return;
        }
        
        var current = _stack.Peek();
        var newMin = value.CompareTo(current.Min) < 0 ? value : current.Min;
        var newMax = value.CompareTo(current.Max) > 0 ? value : current.Max;
            
        _stack.Push((value, newMin,  newMax));
    }

    public T Pop()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is empty. Unable to pop from empty stack.");
        
        return  (_stack.Pop().Value);
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is null, cannot Peek at null value");
        
        return _stack.Peek().Value;
    }
    
    public T GetMin()
    {
        if (_stack.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        return _stack.Peek().Min;
    }

    public T GetMax()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is null. No value to get");
        
        return _stack.Peek().Max;
    }
}