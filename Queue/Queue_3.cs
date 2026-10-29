namespace AlgorithmsDataStructures;

public class QueueTest {
  [Fact]
  public void Test() {
    Queue<int> q = new Queue<int>();
    Assert.Equal(0, q.Size());
    Assert.Equal(0, q.Dequeue());
    q.Enqueue(1);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(0, q.Size());
    q.Enqueue(1);
    q.Enqueue(2);
    q.Enqueue(3);
    Assert.Equal(3, q.Size());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(2, q.Size());
    Assert.Equal(2, q.Dequeue());
    Assert.Equal(1, q.Size());
    Assert.Equal(3, q.Dequeue());
    Assert.Equal(0, q.Size());
  }
}

public class QueueToolsTest {
  [Fact]
  public void RotateEmpty() {
    Queue<int> q = new Queue<int>();
    QueueTools.Rotate(q, 2);
    Assert.Equal(0, q.Size());
  }

  [Fact]
  public void RotateSingle() {
    Queue<int> q = new Queue<int>();
    q.Enqueue(1);
    QueueTools.Rotate(q, 2);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.Dequeue());
  }

  [Fact]
  public void RotateMany() {
    Queue<int> q = new Queue<int>();
    q.Enqueue(1);
    q.Enqueue(2);
    q.Enqueue(3);
    q.Enqueue(4);
    q.Enqueue(5);
    QueueTools.Rotate(q, 2);
    Assert.Equal(5, q.Size());
    Assert.Equal(3, q.Dequeue());
    Assert.Equal(4, q.Dequeue());
    Assert.Equal(5, q.Dequeue());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(2, q.Dequeue());
  }

  [Fact]
  public void RotateManyBack() {
    Queue<int> q = new Queue<int>();
    q.Enqueue(1);
    q.Enqueue(2);
    q.Enqueue(3);
    q.Enqueue(4);
    q.Enqueue(5);
    QueueTools.Rotate(q, -2);
    Assert.Equal(5, q.Size());
    Assert.Equal(4, q.Dequeue());
    Assert.Equal(5, q.Dequeue());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(2, q.Dequeue());
    Assert.Equal(3, q.Dequeue());
  }

  [Fact]
  public void InverseEmpty() {
    Queue<int> q = new Queue<int>();
    QueueTools.Inverse(q);
    Assert.Equal(0, q.Size());
  }

  [Fact]
  public void InverseSingle() {
    Queue<int> q = new Queue<int>();
    q.Enqueue(1);
    QueueTools.Inverse(q);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.Dequeue());
  }

  [Fact]
  public void InverseMany() {
    Queue<int> q = new Queue<int>();
    q.Enqueue(1);
    q.Enqueue(2);
    q.Enqueue(3);
    QueueTools.Inverse(q);
    Assert.Equal(3, q.Size());
    Assert.Equal(3, q.Dequeue());
    Assert.Equal(2, q.Dequeue());
    Assert.Equal(1, q.Dequeue());
  }
}

public class Queue2StackTest {
  [Fact]
  public void Test() {
    Queue2Stack<int> q = new Queue2Stack<int>();
    Assert.Equal(0, q.Size());
    Assert.Equal(0, q.Dequeue());
    q.Enqueue(1);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(0, q.Size());
    q.Enqueue(1);
    q.Enqueue(2);
    q.Enqueue(3);
    Assert.Equal(3, q.Size());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(2, q.Size());
    Assert.Equal(2, q.Dequeue());
    Assert.Equal(1, q.Size());
    Assert.Equal(3, q.Dequeue());
    Assert.Equal(0, q.Size());
  }
}

public class QueueBufferTest {
  [Fact]
  public void Test() {
    QueueBuffer<int> q = new QueueBuffer<int>(3);
    Assert.Equal(0, q.Size());
    Assert.Equal(0, q.Dequeue());
    q.Enqueue(1);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(0, q.Size());
    q.Enqueue(1);
    q.Enqueue(2);
    q.Enqueue(3);
    Assert.Equal(3, q.Size());
    q.Enqueue(4);
    Assert.Equal(3, q.Size());
    Assert.Equal(1, q.Dequeue());
    Assert.Equal(2, q.Size());
    Assert.Equal(2, q.Dequeue());
    Assert.Equal(1, q.Size());
    Assert.Equal(3, q.Dequeue());
    Assert.Equal(0, q.Size());
  }
}
