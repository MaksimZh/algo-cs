using Xunit;

namespace AlgorithmsDataStructures {
  public class LinkedList2Test {
    [Fact]
    public void FindEmpty() {
      var list = new LinkedList2();
      Assert.Null(list.Find(42));
    }

    [Fact]
    public void FindSingle() {
      var list = new LinkedList2();
      var x = new Node(42);
      list.AddInTail(x);
      Assert.Equal(x, list.Find(42));
    }

    [Fact]
    public void Find() {
      var list = new LinkedList2();
      var x = new Node(2);
      list.AddInTail(new Node(1));
      list.AddInTail(x);
      list.AddInTail(new Node(3));
      Assert.Equal(x, list.Find(2));
    }

    [Fact]
    public void FindAllEmpty() {
      var list = new LinkedList2();
      Assert.Equal(new List<Node>{}, list.FindAll(0));
    }

    [Fact]
    public void FindAllMissing() {
      var list = new LinkedList2();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.Equal(new List<Node>{}, list.FindAll(0));
    }

    [Fact]
    public void FindAll() {
      var list = new LinkedList2();
      var a = new Node(0);
      var b = new Node(0);
      var c = new Node(0);
      list.AddInTail(a);
      list.AddInTail(new Node(1));
      list.AddInTail(b);
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.AddInTail(c);
      Assert.Equal(new List<Node>{a, b, c}, list.FindAll(0));
    }

    [Fact]
    public void RemoveFromEmpty() {
      var list = new LinkedList2();
      Assert.False(list.Remove(42));
    }

    [Fact]
    public void RemoveMissing() {
      var list = new LinkedList2();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.False(list.Remove(42));
      Assert.NotNull(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.NotNull(list.Find(3));
    }

    [Fact]
    public void RemoveSingle() {
      var list = new LinkedList2();
      list.AddInTail(new Node(42));
      Assert.True(list.Remove(42));
      Assert.Null(list.Find(42));
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void RemoveHead() {
      var list = new LinkedList2();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.True(list.Remove(1));
      Assert.Null(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(2, list.head.value);
      Assert.Equal(3, list.tail.value);
    }

    [Fact]
    public void RemoveMiddle() {
      var list = new LinkedList2();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.True(list.Remove(2));
      Assert.NotNull(list.Find(1));
      Assert.Null(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(1, list.head.value);
      Assert.Equal(3, list.tail.value);
    }

    [Fact]
    public void RemoveTail() {
      var list = new LinkedList2();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.True(list.Remove(3));
      Assert.NotNull(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.Null(list.Find(3));
      Assert.Equal(1, list.head.value);
      Assert.Equal(2, list.tail.value);
    }

  }
}
