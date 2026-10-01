using Xunit;

namespace AlgorithmsDataStructures {
  public class LinkedListTest {
      [Fact]
      public void RemoveFromEmpty() {
        var list = new LinkedList();
        Assert.False(list.Remove(42));
      }

      [Fact]
      public void RemoveMissing() {
        var list = new LinkedList();
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
        var list = new LinkedList();
        list.AddInTail(new Node(42));
        Assert.True(list.Remove(42));
        Assert.Null(list.Find(42));
        Assert.Null(list.head);
        Assert.Null(list.tail);
      }

      [Fact]
      public void RemoveHead() {
        var list = new LinkedList();
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
        var list = new LinkedList();
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
        var list = new LinkedList();
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

      [Fact]
      public void RemoveAllFromEmpty() {
        var list = new LinkedList();
        list.RemoveAll(42);
      }

      [Fact]
      public void RemoveAllMissing() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        list.RemoveAll(42);
        Assert.NotNull(list.Find(1));
        Assert.NotNull(list.Find(2));
        Assert.NotNull(list.Find(3));
      }
      
      [Fact]
      public void RemoveAllSingle() {
        var list = new LinkedList();
        list.AddInTail(new Node(42));
        Assert.True(list.Remove(42));
        Assert.Null(list.Find(42));
        Assert.Null(list.head);
        Assert.Null(list.tail);
      }

      [Fact]
      public void RemoveAllHead() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        list.RemoveAll(1);
        Assert.Null(list.Find(1));
        Assert.NotNull(list.Find(2));
        Assert.NotNull(list.Find(3));
        Assert.Equal(2, list.head.value);
        Assert.Equal(3, list.tail.value);
      }

      [Fact]
      public void RemoveAllMiddle() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        list.RemoveAll(2);
        Assert.NotNull(list.Find(1));
        Assert.Null(list.Find(2));
        Assert.NotNull(list.Find(3));
        Assert.Equal(1, list.head.value);
        Assert.Equal(3, list.tail.value);
      }

      [Fact]
      public void RemoveAllTail() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        list.RemoveAll(3);
        Assert.NotNull(list.Find(1));
        Assert.NotNull(list.Find(2));
        Assert.Null(list.Find(3));
        Assert.Equal(1, list.head.value);
        Assert.Equal(2, list.tail.value);
      }

      [Fact]
      public void RemoveAllTwo() {
        var list = new LinkedList();
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.RemoveAll(0);
        Assert.Null(list.Find(0));
        Assert.Null(list.head);
        Assert.Null(list.tail);
      }

      [Fact]
      public void RemoveAllThree() {
        var list = new LinkedList();
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.RemoveAll(0);
        Assert.Null(list.Find(0));
        Assert.Null(list.head);
        Assert.Null(list.tail);
      }

      [Fact]
      public void RemoveAllEnds() {
        var list = new LinkedList();
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        list.AddInTail(new Node(0));
        list.RemoveAll(0);
        Assert.Null(list.Find(0));
        Assert.NotNull(list.Find(2));
        Assert.NotNull(list.Find(3));
        Assert.Equal(2, list.head.value);
        Assert.Equal(3, list.tail.value);
      }

      [Fact]
      public void RemoveAllTwoMiddle() {
        var list = new LinkedList();
        list.AddInTail(new Node(1));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(4));
        list.RemoveAll(0);
        Assert.Null(list.Find(0));
        Assert.NotNull(list.Find(1));
        Assert.NotNull(list.Find(4));
        Assert.Equal(1, list.head.value);
        Assert.Equal(4, list.tail.value);
      }

      [Fact]
      public void RemoveAllMany() {
        var list = new LinkedList();
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(2));
        list.AddInTail(new Node(3));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(6));
        list.AddInTail(new Node(7));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(10));
        list.AddInTail(new Node(11));
        list.AddInTail(new Node(0));
        list.AddInTail(new Node(0));
        list.RemoveAll(0);
        Assert.Null(list.Find(0));
        Assert.NotNull(list.Find(2));
        Assert.NotNull(list.Find(3));
        Assert.NotNull(list.Find(6));
        Assert.NotNull(list.Find(7));
        Assert.NotNull(list.Find(10));
        Assert.NotNull(list.Find(11));
        Assert.Equal(2, list.head.value);
        Assert.Equal(11, list.tail.value);
      }
  }
}
