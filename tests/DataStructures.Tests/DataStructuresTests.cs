
namespace DataStructures.Tests;

public class DataStructuresTests
{
    #region Stack Tests

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Push_SingleValue_MinAndMaxAreThatElement(int value)
    {
        
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(value);

        Assert.Equal(value, minMaxStack.GetMax());
        Assert.Equal(value, minMaxStack.GetMin());

    }

    [Fact]
    public void Push_AllIdenticalValues_MinAndMaxAreTheSame()
    {
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(1);
        minMaxStack.Push(1);
        
        Assert.Equal(1, minMaxStack.GetMax());
        Assert.Equal(1, minMaxStack.GetMin());
        Assert.Equal(minMaxStack.GetMax(), minMaxStack.GetMin());
    }

    [Theory]
    [InlineData(new[] { 1, 5, 2, 3, 3 }, 1, 5)]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, 1, 5)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 1, 5)]
    public void Push_SequenceHandling_MinAndMax(int[] values, int expectedMin, int expectedMax)
    {
        var  minMaxStack = new MinMaxStack<int>();
        foreach (var value in values)
        {
            minMaxStack.Push(value);
        }
        
        Assert.Equal(expectedMin, minMaxStack.GetMin());
        Assert.Equal(expectedMax, minMaxStack.GetMax());
    }

    [Fact]
    public void Pop_RemovingNonMinMaxValue()
    {
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(10);
        minMaxStack.Push(1);
        minMaxStack.Push(5);
        
        minMaxStack.Pop();
        Assert.Equal(1, minMaxStack.GetMin());
        Assert.Equal(10, minMaxStack.GetMax());
    }

    [Fact]
    public void Push_Pop_WorkInTandem()
    {
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(10);
        Assert.Equal(10, minMaxStack.GetMin());
        Assert.Equal(10, minMaxStack.GetMax());
        
        minMaxStack.Push(5);
        Assert.Equal(5, minMaxStack.GetMin());
        
        minMaxStack.Push(16);
        Assert.Equal(16, minMaxStack.GetMax());

        minMaxStack.Pop();
        Assert.Equal(5, minMaxStack.GetMin());
        Assert.Equal(10, minMaxStack.GetMax());
        
        minMaxStack.Pop();
        Assert.Equal(10, minMaxStack.GetMin());
        Assert.Equal(10, minMaxStack.GetMax());
    }

    [Fact]
    public void Pop_HandlesMinAfterPop()
    {
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(3);
        minMaxStack.Push(2);
        minMaxStack.Push(1);
        
        minMaxStack.Pop();
        Assert.Equal(2, minMaxStack.GetMin());
        
        minMaxStack.Pop();
        Assert.Equal(3, minMaxStack.GetMin());
    }

    [Fact]
    public void Pop_HandlesMaxAfterPop()
    {
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(1);
        minMaxStack.Push(2);
        minMaxStack.Push(3);
        
        minMaxStack.Pop();
        Assert.Equal(2, minMaxStack.GetMax());
        
        minMaxStack.Pop();
        Assert.Equal(1, minMaxStack.GetMax());
    }

    [Fact]
    public void Pop_ReturnsCorrectValue()
    {
        var minMaxStack = new MinMaxStack<int>();
        minMaxStack.Push(1);
        minMaxStack.Push(2);
        
        Assert.Equal(2, minMaxStack.Pop());
        Assert.Equal(1, minMaxStack.Pop());
    }

    [Fact]
    public void GetMin_EmptyStack_ThrowsException()
    {
        var minMaxStack = new MinMaxStack<int>();
        Assert.Throws<InvalidOperationException>(() => minMaxStack.GetMin());
    }

    [Fact]
    public void GetMax_EmptyStack_ThrowsException()
    {
        var minMaxStack = new MinMaxStack<int>();
        Assert.Throws<InvalidOperationException>(() => minMaxStack.GetMax());
    }

    [Fact]
    public void Pop_EmptyStack_ThrowsException()
    {
        var minMaxStack = new MinMaxStack<int>();
        Assert.Throws<InvalidOperationException>(() => minMaxStack.Pop());
    }

    [Fact]
    public void Peak_EmptyStack_ThrowsException()
    {
        var minMaxStack = new MinMaxStack<int>();
        Assert.Throws<InvalidOperationException>(() => minMaxStack.Peek());
    }

    #endregion
    
    #region Circular Buffer

    [Fact]
    public void CircularBuffer_CanEnqueueOnSingleElement()
    {
        var circularBuffer = new CircularBuffer<int>(1);
        circularBuffer.Enqueue(1);
        
        Assert.Equal(1, circularBuffer.Count);
        var peek =  circularBuffer.Peek();
        Assert.Equal(1, peek);
    }

    [Fact]
    public void CircularBuffer_DequeueReturnsInFIFO()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        circularBuffer.Enqueue(1);
        circularBuffer.Enqueue(2);
        circularBuffer.Enqueue(3);
        
        var firstDequeue =  circularBuffer.Dequeue();
        Assert.Equal(1, firstDequeue);
        
        var secondDequeue =  circularBuffer.Dequeue();
        Assert.Equal(2, secondDequeue);
        var thirdDequeue =  circularBuffer.Dequeue();
        Assert.Equal(3, thirdDequeue);
    }

    [Fact]
    public void CircularBuffer_IsFull()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        circularBuffer.Enqueue(1);
        circularBuffer.Enqueue(2);
        circularBuffer.Enqueue(3);
        Assert.True(circularBuffer.IsFull);
    }

    [Fact]
    public void CircularBuffer_IsEmpty()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        Assert.True(circularBuffer.IsEmpty);
    }

    [Fact]
    public void CircularBuffer_ISEmptyAndIsFull_HandleStateChanges()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        Assert.False(circularBuffer.IsFull);
        Assert.True(circularBuffer.IsEmpty);
        
        circularBuffer.Enqueue(1);
        Assert.False(circularBuffer.IsFull);
        Assert.False(circularBuffer.IsEmpty);
        
        circularBuffer.Enqueue(2);
        Assert.False(circularBuffer.IsFull);
        Assert.False(circularBuffer.IsEmpty);
        
        circularBuffer.Enqueue(3);
        Assert.True(circularBuffer.IsFull);
        Assert.False(circularBuffer.IsEmpty);

        circularBuffer.Dequeue();
        Assert.False(circularBuffer.IsEmpty);
        Assert.False(circularBuffer.IsFull);
        
        circularBuffer.Dequeue();
        Assert.False(circularBuffer.IsEmpty);
        Assert.False(circularBuffer.IsFull);
        
        circularBuffer.Dequeue();
        Assert.True(circularBuffer.IsEmpty);
        Assert.False(circularBuffer.IsFull);
    }

    [Fact]
    public void CircularBuffer_OverwriteHead()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        circularBuffer.Enqueue(1);
        circularBuffer.Enqueue(2);
        circularBuffer.Enqueue(3);
        
        var oldHead =  circularBuffer.Peek();
        circularBuffer.Enqueue(4);
        var newHead =  circularBuffer.Peek();
        Assert.Equal(2, newHead);
        Assert.False(oldHead == newHead);
    }

    [Fact]
    public void CircularBuffer_PartiallyFilledBuffer()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        circularBuffer.Enqueue(1);
        circularBuffer.Enqueue(2);
        
        var totalMembers = circularBuffer.Count;
        
        Assert.Equal(2, totalMembers);
    }

    [Fact]
    public void CircularBuffer_Iteration_PartiallyFilledBuffer()
    {
        var circularBuffer = new CircularBuffer<int>(5);
        circularBuffer.Enqueue(1);
        circularBuffer.Enqueue(2);
        
        Assert.False(circularBuffer.IsFull);
        Assert.Equal(new[] {1, 2}, circularBuffer);
    }

    [Fact]
    public void CircularBuffer_Iteration_HandlesWrapAround()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        circularBuffer.Enqueue(1);
        circularBuffer.Enqueue(2);
        circularBuffer.Enqueue(3);
        Assert.True(circularBuffer.IsFull);
        
        circularBuffer.Dequeue();
        circularBuffer.Enqueue(4);
        
        Assert.Equal(new[] { 2, 3, 4}, circularBuffer);
    }

    [Fact]
    public void CircularBuffer_DequeueOnEmptyThrows()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        Assert.Throws<InvalidOperationException>(() => circularBuffer.Dequeue());
    }

    [Fact]
    public void CircularBuffer_PeekOnEmptyThrows()
    {
        var circularBuffer = new CircularBuffer<int>(3);
        Assert.Throws<InvalidOperationException>(() => circularBuffer.Peek());
    }

    [Fact]
    public void CircularBuffer_InvalidCapacityThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CircularBuffer<int>(-1));
    }
    #endregion
}