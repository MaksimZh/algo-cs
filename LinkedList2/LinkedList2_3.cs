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

    [Fact]
    public void RemoveAllFromEmpty() {
      var list = new LinkedList2();
      list.RemoveAll(42);
    }

    [Fact]
    public void RemoveAllMissing() {
      var list = new LinkedList2();
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
      var list = new LinkedList2();
      list.AddInTail(new Node(42));
      Assert.True(list.Remove(42));
      Assert.Null(list.Find(42));
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void RemoveAllHead() {
      var list = new LinkedList2();
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
      var list = new LinkedList2();
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
      var list = new LinkedList2();
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
      var list = new LinkedList2();
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(0));
      list.RemoveAll(0);
      Assert.Null(list.Find(0));
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void RemoveAllThree() {
      var list = new LinkedList2();
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
      var list = new LinkedList2();
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
      var list = new LinkedList2();
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
      var list = new LinkedList2();
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
    public void InsertAfterEmpty() {
      var list = new LinkedList2();
      var x = new Node(42);
      list.InsertAfter(null, x);
      Assert.Null(list.head.prev);
      Assert.Null(list.tail.next);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.head);
      Assert.Equal(x, list.tail);
    }

    [Fact]
    public void InsertAfterNewHead() {
      var list = new LinkedList2();
      var x = new Node(42);
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.InsertAfter(null, x);
      Assert.Null(list.head.prev);
      Assert.Null(list.tail.next);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.head);
      Assert.Null(x.prev);
      Assert.Equal(1, x.next.value);
    }

    [Fact]
    public void InsertAfterNewTail() {
      var list = new LinkedList2();
      var x = new Node(42);
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.InsertAfter(list.tail, x);
      Assert.Null(list.head.prev);
      Assert.Null(list.tail.next);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.tail);
      Assert.Equal(3, x.prev.value);
      Assert.Null(x.next);
    }

    [Fact]
    public void InsertAfterMiddle() {
      var list = new LinkedList2();
      var x = new Node(42);
      var y = new Node(2);
      list.AddInTail(new Node(1));
      list.AddInTail(y);
      list.AddInTail(new Node(3));
      list.InsertAfter(y, x);
      Assert.Null(list.head.prev);
      Assert.Null(list.tail.next);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, y.next);
      Assert.Equal(y, x.prev);
      Assert.Equal(3, x.next.value);
    }

    [Fact]
    public void ClearEmpty() {
      var list = new LinkedList2();
      list.Clear();
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void Clear() {
      var list = new LinkedList2();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.Clear();
      Assert.Null(list.head);
      Assert.Null(list.tail);
    }

    [Fact]
    public void Count() {
      var list = new LinkedList2();
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
  }

  public class LinkedList2XTest {
    [Fact]
    public void FindEmpty() {
      var list = new LinkedList2X();
      Assert.Null(list.Find(42));
    }

    [Fact]
    public void FindSingle() {
      var list = new LinkedList2X();
      var x = new Node(42);
      list.AddInTail(x);
      Assert.Equal(x, list.Find(42));
    }

    [Fact]
    public void Find() {
      var list = new LinkedList2X();
      var x = new Node(2);
      list.AddInTail(new Node(1));
      list.AddInTail(x);
      list.AddInTail(new Node(3));
      Assert.Equal(x, list.Find(2));
    }

    [Fact]
    public void FindAllEmpty() {
      var list = new LinkedList2X();
      Assert.Equal(new List<Node>{}, list.FindAll(0));
    }

    [Fact]
    public void FindAllMissing() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.Equal(new List<Node>{}, list.FindAll(0));
    }

    [Fact]
    public void FindAll() {
      var list = new LinkedList2X();
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
      var list = new LinkedList2X();
      Assert.False(list.Remove(42));
    }

    [Fact]
    public void RemoveMissing() {
      var list = new LinkedList2X();
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
      var list = new LinkedList2X();
      list.AddInTail(new Node(42));
      Assert.True(list.Remove(42));
      Assert.Null(list.Find(42));
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void RemoveHead() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.True(list.Remove(1));
      Assert.Null(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(2, list.Head().value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void RemoveMiddle() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.True(list.Remove(2));
      Assert.NotNull(list.Find(1));
      Assert.Null(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(1, list.Head().value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void RemoveTail() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.True(list.Remove(3));
      Assert.NotNull(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.Null(list.Find(3));
      Assert.Equal(1, list.Head().value);
      Assert.Equal(2, list.Tail().value);
    }

    [Fact]
    public void RemoveAllFromEmpty() {
      var list = new LinkedList2X();
      list.RemoveAll(42);
    }

    [Fact]
    public void RemoveAllMissing() {
      var list = new LinkedList2X();
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
      var list = new LinkedList2X();
      list.AddInTail(new Node(42));
      Assert.True(list.Remove(42));
      Assert.Null(list.Find(42));
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void RemoveAllHead() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.RemoveAll(1);
      Assert.Null(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(2, list.Head().value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void RemoveAllMiddle() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.RemoveAll(2);
      Assert.NotNull(list.Find(1));
      Assert.Null(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(1, list.Head().value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void RemoveAllTail() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.RemoveAll(3);
      Assert.NotNull(list.Find(1));
      Assert.NotNull(list.Find(2));
      Assert.Null(list.Find(3));
      Assert.Equal(1, list.Head().value);
      Assert.Equal(2, list.Tail().value);
    }

    [Fact]
    public void RemoveAllTwo() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(0));
      list.RemoveAll(0);
      Assert.Null(list.Find(0));
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void RemoveAllThree() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(0));
      list.RemoveAll(0);
      Assert.Null(list.Find(0));
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void RemoveAllEnds() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.AddInTail(new Node(0));
      list.RemoveAll(0);
      Assert.Null(list.Find(0));
      Assert.NotNull(list.Find(2));
      Assert.NotNull(list.Find(3));
      Assert.Equal(2, list.Head().value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void RemoveAllTwoMiddle() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(0));
      list.AddInTail(new Node(4));
      list.RemoveAll(0);
      Assert.Null(list.Find(0));
      Assert.NotNull(list.Find(1));
      Assert.NotNull(list.Find(4));
      Assert.Equal(1, list.Head().value);
      Assert.Equal(4, list.Tail().value);
    }

    [Fact]
    public void RemoveAllMany() {
      var list = new LinkedList2X();
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
      Assert.Equal(2, list.Head().value);
      Assert.Equal(11, list.Tail().value);
    }

    [Fact]
    public void InsertAfterEmpty() {
      var list = new LinkedList2X();
      var x = new Node(42);
      list.InsertAfter(null, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.Head());
      Assert.Equal(x, list.Tail());
    }

    [Fact]
    public void InsertAfterNewHead() {
      var list = new LinkedList2X();
      var x = new Node(42);
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.InsertAfter(null, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.Head());
      Assert.Equal(1, x.next.value);
    }

    [Fact]
    public void InsertAfterNewTail() {
      var list = new LinkedList2X();
      var x = new Node(42);
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.InsertAfter(list.Tail(), x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, list.Tail());
      Assert.Equal(3, x.prev.value);
    }

    [Fact]
    public void InsertAfterMiddle() {
      var list = new LinkedList2X();
      var x = new Node(42);
      var y = new Node(2);
      list.AddInTail(new Node(1));
      list.AddInTail(y);
      list.AddInTail(new Node(3));
      list.InsertAfter(y, x);
      Assert.NotNull(list.Find(42));
      Assert.Equal(x, y.next);
      Assert.Equal(y, x.prev);
      Assert.Equal(3, x.next.value);
    }

    [Fact]
    public void ClearEmpty() {
      var list = new LinkedList2X();
      list.Clear();
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void Clear() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.Clear();
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void Count() {
      var list = new LinkedList2X();
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
    public void InverseEmpty() {
      var list = new LinkedList2X();
      list.Inverse();
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void InverseSingle() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.Inverse();
      Assert.Equal(1, list.Head().value);
      Assert.Equal(1, list.Tail().value);
    }

    [Fact]
    public void Inverse() {
      var list = new LinkedList2X();
      var a = new Node(1);
      var b = new Node(2);
      var c = new Node(3);
      list.AddInTail(a);
      list.AddInTail(b);
      list.AddInTail(c);
      list.Inverse();
      Assert.Equal(c, list.Head());
      Assert.Equal(b, c.next);
      Assert.Equal(c, b.prev);
      Assert.Equal(a, b.next);
      Assert.Equal(b, a.prev);
      Assert.Equal(a, list.Tail());
    }

    [Fact]
    public void HasLoopsEmpty() {
      var list = new LinkedList2X();
      Assert.False(list.HasLoops());
    }

    [Fact]
    public void HasLoopsFalse() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      Assert.False(list.HasLoops());
    }

    [Fact]
    public void HasLoopsTrue() {
      var list = new LinkedList2X();
      var a = new Node(1);
      var b = new Node(2);
      var c = new Node(3);
      list.AddInTail(a);
      list.AddInTail(b);
      list.AddInTail(c);
      c.next = b;
      Assert.True(list.HasLoops());
    }

    [Fact]
    public void SortEmpty() {
      var list = new LinkedList2X();
      list.Sort();
      Assert.Null(list.Head());
      Assert.Null(list.Tail());
    }

    [Fact]
    public void SortSingle() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.Sort();
      Assert.Equal(1, list.Head().value);
      Assert.Equal(1, list.Tail().value);
    }

    [Fact]
    public void SortSorted2() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.Sort();
      Assert.Equal(1, list.Head().value);
      Assert.Equal(2, list.Tail().value);
    }

    [Fact]
    public void Sort2() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(1));
      list.Sort();
      Assert.Equal(1, list.Head().value);
      Assert.Equal(2, list.Tail().value);
    }

    [Fact]
    public void SortSorted3() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(3));
      list.Sort();
      Assert.Equal(1, list.Head().value);
      Assert.Equal(2, list.Head().next.value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void Sort3() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(3));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(1));
      list.Sort();
      Assert.Equal(1, list.Head().value);
      Assert.Equal(2, list.Head().next.value);
      Assert.Equal(3, list.Tail().value);
    }

    [Fact]
    public void SortMany() {
      var list = new LinkedList2X();
      list.AddInTail(new Node(3));
      list.AddInTail(new Node(2));
      list.AddInTail(new Node(7));
      list.AddInTail(new Node(1));
      list.AddInTail(new Node(5));
      list.AddInTail(new Node(6));
      list.AddInTail(new Node(8));
      list.AddInTail(new Node(4));
      list.Sort();
      var cursor = list.Head();
      for (int i = 1; i <= 8; ++i) {
        Assert.Equal(i, cursor.value);
        cursor = cursor.next;
      }
    }

    [Fact]
    public void MergeEmptyEmpty() {
      var x = new LinkedList2X();
      var y = new LinkedList2X();
      var z = LinkedList2X.Merge(x, y);
      Assert.Equal(0, z.Count());
    }

    [Fact]
    public void MergeEmptyFull() {
      var x = new LinkedList2X();
      var y = new LinkedList2X();
      y.AddInTail(new Node(2));
      y.AddInTail(new Node(1));
      y.AddInTail(new Node(3));
      var z = LinkedList2X.Merge(x, y);
      Assert.Equal(3, z.Count());
      Assert.Equal(1, z.Head().value);
      Assert.Equal(2, z.Head().next.value);
      Assert.Equal(3, z.Tail().value);
    }

    [Fact]
    public void MergeFullEmpty() {
      var x = new LinkedList2X();
      x.AddInTail(new Node(2));
      x.AddInTail(new Node(1));
      x.AddInTail(new Node(3));
      var y = new LinkedList2X();
      var z = LinkedList2X.Merge(x, y);
      Assert.Equal(3, z.Count());
      Assert.Equal(1, z.Head().value);
      Assert.Equal(2, z.Head().next.value);
      Assert.Equal(3, z.Tail().value);
    }

    [Fact]
    public void MergeFullFull() {
      var x = new LinkedList2X();
      x.AddInTail(new Node(5));
      x.AddInTail(new Node(2));
      var y = new LinkedList2X();
      x.AddInTail(new Node(4));
      x.AddInTail(new Node(1));
      x.AddInTail(new Node(3));
      var z = LinkedList2X.Merge(x, y);
      Assert.Equal(5, z.Count());
      var cursor = z.Head();
      for (int i = 1; i <= 5; ++i) {
        Assert.Equal(i, cursor.value);
        cursor = cursor.next;
      }
    }
  }
}
