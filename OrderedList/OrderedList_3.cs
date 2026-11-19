namespace AlgorithmsDataStructures;

public class OrderedListTest
{
  private List<T> ToValues<T>(List<Node<T>> nodes) {
    List<T> result = new List<T>();
    foreach (Node<T> node in nodes) {
      result.Add(node.value);
    }
    return result;
  }

  private void CheckStructure<T>(OrderedList<T> list) {
    // Console.WriteLine("---Check---");
    if (list.head == null) {
      return;
    }
    List<Node<T>> children = new List<Node<T>>();
    Node<T> sentinel = list.head.prev;
    for (Node<T> x = sentinel.next; x != sentinel; x = x.next) {
      children.Add(x);
      Assert.Equal(1, x.shift);
    }
    Assert.Equal(1, sentinel.shift);
    int count = children.Count;
    for (; sentinel.parent != null; sentinel = sentinel.parent) {
      List<Node<T>> parents = new List<Node<T>>();
      int skip = 0;
      int shift = 0;
      for (int i = 0; i < children.Count; ++i) {
        Node<T> x = children[i];
        // Console.Write($" {x.value}({x.shift})");
        shift += x.shift;
        if (x.parent == null) {
          ++skip;
          continue;
        }
        // Console.Write("+");
        Assert.True(skip > 0);
        Assert.True(skip < 3);
        Assert.Equal(shift, x.parent.shift);
        parents.Add(x.parent);
        skip = 0;
        shift = 0;
      }
      // Console.Write($" *({sentinel.shift})");
      shift += sentinel.shift;
      Assert.True(skip > 0);
      Assert.True(skip < 3);
      Assert.Equal(shift, sentinel.parent.shift);
      children = parents;
      // Console.WriteLine();
    }
    // Console.WriteLine($" *({sentinel.shift})");
    Assert.Equal(sentinel.next, sentinel);
    Assert.Equal(count + 1, sentinel.shift);
    Assert.Empty(children);
  }

  [Fact]
  public void TestAdd() {
    OrderedList<int> list = new OrderedList<int>(true);
    Assert.Null(list.head);
    Assert.Null(list.tail);
    Assert.Equal(0, list.Count());
    Assert.Null(list.Find(42));
    Assert.Equal([], list.GetAll());
    CheckStructure(list);

    list.Add(10);
    CheckStructure(list);
    Assert.Equal(10, list.head.value);
    Assert.Equal(10, list.tail.value);
    Assert.Equal(1, list.Count());
    Assert.Null(list.Find(1));
    Assert.Equal(10, list.Find(10).value);
    Assert.Null(list.Find(42));
    Assert.Equal([10], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Add(20);
    Assert.Equal(10, list.head.value);
    Assert.Equal(20, list.tail.value);
    Assert.Equal(2, list.Count());
    Assert.Null(list.Find(1));
    Assert.Equal(10, list.Find(10).value);
    Assert.Null(list.Find(15));
    Assert.Equal(20, list.Find(20).value);
    Assert.Null(list.Find(42));
    Assert.Equal([10, 20], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Add(30);
    Assert.Equal(10, list.head.value);
    Assert.Equal(30, list.tail.value);
    Assert.Equal(3, list.Count());
    Assert.Null(list.Find(1));
    Assert.Equal(10, list.Find(10).value);
    Assert.Null(list.Find(15));
    Assert.Equal(20, list.Find(20).value);
    Assert.Null(list.Find(25));
    Assert.Equal(30, list.Find(30).value);
    Assert.Null(list.Find(42));
    Assert.Equal([10, 20, 30], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Add(40);
    Assert.Equal(10, list.head.value);
    Assert.Equal(40, list.tail.value);
    Assert.Equal(4, list.Count());
    Assert.Null(list.Find(1));
    Assert.Equal(10, list.Find(10).value);
    Assert.Null(list.Find(15));
    Assert.Equal(20, list.Find(20).value);
    Assert.Null(list.Find(25));
    Assert.Equal(30, list.Find(30).value);
    Assert.Null(list.Find(35));
    Assert.Equal(40, list.Find(40).value);
    Assert.Null(list.Find(42));
    Assert.Equal([10, 20, 30, 40], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Add(50);
    Assert.Equal(10, list.head.value);
    Assert.Equal(50, list.tail.value);
    Assert.Equal(5, list.Count());
    Assert.Null(list.Find(1));
    Assert.Equal(10, list.Find(10).value);
    Assert.Null(list.Find(15));
    Assert.Equal(20, list.Find(20).value);
    Assert.Null(list.Find(25));
    Assert.Equal(30, list.Find(30).value);
    Assert.Null(list.Find(35));
    Assert.Equal(40, list.Find(40).value);
    Assert.Null(list.Find(45));
    Assert.Equal(50, list.Find(50).value);
    Assert.Null(list.Find(100));
    Assert.Equal([10, 20, 30, 40, 50], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Add(60);
    list.Add(70);
    list.Add(80);
    Assert.Equal(10, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal(8, list.Count());
    Assert.Equal([10, 20, 30, 40, 50, 60, 70, 80], ToValues(list.GetAll()));
    CheckStructure(list);
  }

  [Fact]
  public void TestUnorderedAdd() {
    OrderedList<int> list = new OrderedList<int>(true);
    list.Add(20);
    CheckStructure(list);
    list.Add(50);
    CheckStructure(list);
    list.Add(10);
    CheckStructure(list);
    list.Add(40);
    CheckStructure(list);
    list.Add(30);
    CheckStructure(list);
    list.Add(80);
    CheckStructure(list);
    list.Add(70);
    CheckStructure(list);
    list.Add(60);
    CheckStructure(list);
    Assert.Equal(10, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal(8, list.Count());
    Assert.Equal([10, 20, 30, 40, 50, 60, 70, 80], ToValues(list.GetAll()));
  }

  [Fact]
  public void TestDelete() {
    OrderedList<int> list = new OrderedList<int>(true);
    list.Add(20);
    list.Add(50);
    list.Add(10);
    list.Add(40);
    list.Add(30);
    list.Add(80);
    list.Add(70);
    list.Add(60);

    list.Delete(40);
    Assert.Equal(7, list.Count());
    Assert.Equal(10, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal([10, 20, 30, 50, 60, 70, 80], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(10);
    Assert.Equal(6, list.Count());
    Assert.Equal(20, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal([20, 30, 50, 60, 70, 80], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(50);
    Assert.Equal(5, list.Count());
    Assert.Equal(20, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal([20, 30, 60, 70, 80], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(20);
    Assert.Equal(4, list.Count());
    Assert.Equal(30, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal([30, 60, 70, 80], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(80);
    Assert.Equal(3, list.Count());
    Assert.Equal(30, list.head.value);
    Assert.Equal(70, list.tail.value);
    Assert.Equal([30, 60, 70], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(60);
    Assert.Equal(2, list.Count());
    Assert.Equal(30, list.head.value);
    Assert.Equal(70, list.tail.value);
    Assert.Equal([30, 70], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(30);
    Assert.Equal(1, list.Count());
    Assert.Equal(70, list.head.value);
    Assert.Equal(70, list.tail.value);
    Assert.Equal([70], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(70);
    Assert.Equal(0, list.Count());
    Assert.Null(list.head);
    Assert.Null(list.tail);
  }

  [Fact]
  public void TestDescending() {
    OrderedList<int> list = new OrderedList<int>(false);
    list.Add(20);
    list.Add(50);
    list.Add(10);
    list.Add(40);
    list.Add(30);
    list.Add(80);
    list.Add(70);
    list.Add(60);
    Assert.Equal(8, list.Count());
    Assert.Equal(80, list.head.value);
    Assert.Equal(10, list.tail.value);
    Assert.Equal([80, 70, 60, 50, 40, 30, 20, 10], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete(40);
    list.Delete(10);
    list.Delete(80);
    list.Delete(50);
    list.Delete(20);
    Assert.Equal(3, list.Count());
    Assert.Equal(70, list.head.value);
    Assert.Equal(30, list.tail.value);
    Assert.Equal([70, 60, 30], ToValues(list.GetAll()));
    CheckStructure(list);
  }

  [Fact]
  public void TestString() {
    OrderedList<string> list = new OrderedList<string>(false);
    list.Add("20");
    list.Add("50");
    list.Add("10");
    list.Add("40");
    list.Add("30");
    list.Add("80");
    list.Add("70");
    list.Add("60");
    Assert.Equal(8, list.Count());
    Assert.Equal("80", list.head.value);
    Assert.Equal("10", list.tail.value);
    Assert.Equal(["80", "70", "60", "50", "40", "30", "20", "10"], ToValues(list.GetAll()));
    CheckStructure(list);

    list.Delete("40");
    list.Delete("10");
    list.Delete("80");
    list.Delete("50");
    list.Delete("20");
    Assert.Equal(3, list.Count());
    Assert.Equal("70", list.head.value);
    Assert.Equal("30", list.tail.value);
    Assert.Equal(["70", "60", "30"], ToValues(list.GetAll()));
    CheckStructure(list);
  }

  [Fact]
  public void TestDuplicates() {
    OrderedList<int> list = new OrderedList<int>(true);
    list.Add(10);
    list.Add(10);
    list.Add(10);
    list.Add(10);
    list.Add(10);
    list.Add(10);
    list.Add(10);
    list.Add(10);
    Assert.Equal(8, list.Count());
    Assert.Equal(10, list.head.value);
    Assert.Equal(10, list.tail.value);
    CheckStructure(list);
  }

  [Fact]
  public void TestRemoveDuplicates() {
    OrderedList<int> list = new OrderedList<int>(true);
    list.Add(10);
    list.Add(20);
    list.Add(30);
    list.Add(40);
    list.Add(20);
    list.Add(30);
    list.Add(40);
    list.Add(20);
    list.Add(30);
    list.Add(30);
    list.RemoveDuplicates();
    Assert.Equal(4, list.Count());
    Assert.Equal(10, list.head.value);
    Assert.Equal(40, list.tail.value);
    Assert.Equal([10, 20, 30, 40], ToValues(list.GetAll()));
    CheckStructure(list);
  }

  [Fact]
  public void TestMerge() {
    OrderedList<int> a = new OrderedList<int>(true);
    OrderedList<int> b = new OrderedList<int>(true);
    OrderedList<int> x;
    x = a.Merge(b);
    Assert.Equal([], ToValues(x.GetAll()));

    a.Add(1);
    a.Add(3);
    a.Add(7);
    x = a.Merge(b);
    Assert.Equal([1, 3, 7], ToValues(x.GetAll()));
    CheckStructure(x);

    b.Add(4);
    b.Add(5);
    b.Add(7);
    b.Add(8);
    x = a.Merge(b);
    Assert.Equal([1, 3, 4, 5, 7, 7, 8], ToValues(x.GetAll()));
    CheckStructure(x);
  }

  [Fact]
  public void TestSublist() {
    OrderedList<int> a = new OrderedList<int>(true);
    a.Add(1);
    a.Add(2);
    a.Add(3);
    a.Add(4);
    a.Add(4);
    a.Add(4);
    a.Add(5);
    a.Add(6);
    a.Add(6);
    a.Add(6);
    a.Add(7);
    a.Add(8);

    OrderedList<int> b = new OrderedList<int>(true);
    b.Add(2);
    b.Add(5);
    b.Add(6);
    b.Add(6);
    b.Add(8);
    Assert.True(a.HasSublist(b));

    b.Clear(true);
    b.Add(2);
    b.Add(5);
    b.Add(5);
    b.Add(8);
    CheckStructure(a);
    Assert.False(a.HasSublist(b));
  }

  [Fact]
  public void TestMostFrequent() {
    OrderedList<int> a = new OrderedList<int>(true);
    a.Add(1);
    a.Add(2);
    a.Add(3);
    a.Add(4);
    a.Add(4);
    a.Add(4);
    a.Add(5);
    a.Add(5);
    a.Add(5);
    a.Add(5);
    a.Add(5);
    a.Add(6);
    a.Add(6);
    a.Add(6);
    a.Add(7);
    a.Add(8);
    Assert.Equal(5, a.MostFrequent());
  }

  [Fact]
  public void TestIndexOf() {
    OrderedList<int> a = new OrderedList<int>(true);
    a.Add(10); // 0
    a.Add(20); // 1
    a.Add(30); // 2
    a.Add(40); // 3
    a.Add(40); // 4
    a.Add(40); // 5
    a.Add(50); // 6
    a.Add(50); // 7
    a.Add(50); // 8
    a.Add(50); // 9
    a.Add(50); // 10
    a.Add(60); // 11
    a.Add(60); // 12
    a.Add(60); // 13
    a.Add(70); // 14
    a.Add(80); // 15
    CheckStructure(a);
    Assert.Equal(-1, a.IndexOf(5));
    Assert.Equal(0, a.IndexOf(10));
    Assert.Equal(-1, a.IndexOf(15));
    Assert.Equal(1, a.IndexOf(20));
    Assert.Equal(-1, a.IndexOf(25));
    Assert.Equal(2, a.IndexOf(30));
    Assert.Equal(-1, a.IndexOf(35));
    Assert.Contains(a.IndexOf(40), new SortedSet<int>([3, 4, 5]));
    Assert.Equal(-1, a.IndexOf(45));
    Assert.Contains(a.IndexOf(50), new SortedSet<int>([6, 7, 8, 9, 10]));
    Assert.Equal(-1, a.IndexOf(55));
    Assert.Contains(a.IndexOf(60), new SortedSet<int>([11, 12, 13]));
    Assert.Equal(-1, a.IndexOf(65));
    Assert.Equal(14, a.IndexOf(70));
    Assert.Equal(-1, a.IndexOf(75));
    Assert.Equal(15, a.IndexOf(80));
    Assert.Equal(-1, a.IndexOf(85));
  }
}
