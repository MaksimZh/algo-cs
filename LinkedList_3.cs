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

    [Fact]
    public void ClearEmpty() {
      var list = new LinkedList();
      list.Clear();
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void Clear() {
      var list = new LinkedList();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.Clear();
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void Count() {
      var list = new LinkedList();
      Assert.Equal(0, list.Count());
      list.AddInTail(new Node(1));
      Assert.Equal(1, list.Count());
      list.AddInTail(new Node(2));
      Assert.Equal(2, list.Count());
      list.AddInTail(new Node(3));
      Assert.Equal(3, list.Count());
      list.Clear();
      Assert.Equal(0, list.Count());
    }

    [Fact]
    public void FindAllEmpty() {
      var list = new LinkedList();
      Assert.Equal(new List<Node>{}, list.FindAll(0));
    }

    [Fact]
    public void FindAllMissing() {
      var list = new LinkedList();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.Equal(new List<Node>{}, list.FindAll(0));
    }

    [Fact]
    public void FindAll() {
      var list = new LinkedList();
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
    public void InsertAfterEmpty() {
      var list = new LinkedList();
      var x = new Node(42);
      list.InsertAfter(null, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.head);
      Assert.Equal(x, list.tail);
    }

    [Fact]
    public void InsertAfterNewHead() {
      var list = new LinkedList();
      var x = new Node(42);
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.InsertAfter(null, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.head);
      Assert.Equal(1, x.next.value);
    }

    [Fact]
    public void InsertAfterNewTail() {
      var list = new LinkedList();
      var x = new Node(42);
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.InsertAfter(list.tail, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.tail);
    }

    [Fact]
    public void InsertAfterMiddle() {
      var list = new LinkedList();
      var x = new Node(42);
      var y = new Node(2);
      list.AddInTail(new Node(1));
      list.AddInTail(y);
      list.AddInTail(new Node(3));
      list.InsertAfter(y, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, y.next);
    }
  }

  public class LinkedListToolsTest {
    [Fact]
    public void ByElementAddEmpty() {
      var a = new LinkedList();
      var b = new LinkedList();
      var c = LinkedListTools.ByElementAdd(a, b);
      Assert.Equal(0, c.Count());
    }

    [Fact]
    public void ByElementAddBadShape() {
      var a = new LinkedList();
      a.AddInTail(new Node(1));
      a.AddInTail(new Node(2));
      var b = new LinkedList();
      b.AddInTail(new Node(10));
      b.AddInTail(new Node(20));
      b.AddInTail(new Node(30));
      var c = LinkedListTools.ByElementAdd(a, b);
      Assert.Equal(0, c.Count());
    }

    [Fact]
    public void ByElementAdd() {
      var a = new LinkedList();
      a.AddInTail(new Node(1));
      a.AddInTail(new Node(2));
      a.AddInTail(new Node(3));
      var b = new LinkedList();
      b.AddInTail(new Node(10));
      b.AddInTail(new Node(20));
      b.AddInTail(new Node(30));
      var c = LinkedListTools.ByElementAdd(a, b);
      Assert.Equal(3, c.Count());
      Assert.Equal(11, c.head.value);
      Assert.Equal(22, c.head.next.value);
      Assert.Equal(33, c.tail.value);
    }
  }
}
