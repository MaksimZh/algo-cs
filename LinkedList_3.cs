using Xunit;

namespace AlgorithmsDataStructures {
  public class LinkedListTest {
      [Fact]
      public void RemoveFromEmpty() {
        var list = new LinkedList();
        Assert.Equal(false, list.Remove(42));
      }

      [Fact]
      public void RemoveMissing() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        Assert.Equal(false, list.Remove(42));
      }

      [Fact]
      public void RemoveSingle() {
        var list = new LinkedList();
        list.AddInTail(new Node(42));
        Assert.Equal(true, list.Remove(42));
        Assert.Null(list.Find(42));
      }

      [Fact]
      public void RemoveHead() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        Assert.Equal(true, list.Remove(1));
        Assert.Null(list.Find(1));
        Assert.NotNull(list.Find(2));
        Assert.NotNull(list.Find(3));
      }

      [Fact]
      public void RemoveMiddle() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        Assert.Equal(true, list.Remove(2));
        Assert.NotNull(list.Find(1));
        Assert.Null(list.Find(2));
        Assert.NotNull(list.Find(3));
      }

      [Fact]
      public void RemoveTail() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        Assert.Equal(true, list.Remove(3));
        Assert.NotNull(list.Find(1));
        Assert.NotNull(list.Find(2));
        Assert.Null(list.Find(3));
      }
  }
}
