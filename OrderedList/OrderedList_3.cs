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
    List<T> values = ToValues(list.GetAll());
    Node<T> x = list.head;
    for (int i = 0; i < values.Count(); ++i) {
      Node<T> y = x;
      for (int n = values.Count(); i < n; n /= 2) {
        Assert.NotNull(y);
        Assert.Equal(values[i], y.value);
        y = y.parent;
      }
      Assert.Null(y);
      x = x.next;
    }
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
    list.Add(50);
    list.Add(10);
    list.Add(40);
    list.Add(30);
    list.Add(80);
    list.Add(70);
    list.Add(60);
    Assert.Equal(10, list.head.value);
    Assert.Equal(80, list.tail.value);
    Assert.Equal(8, list.Count());
    Assert.Equal([10, 20, 30, 40, 50, 60, 70, 80], ToValues(list.GetAll()));
    CheckStructure(list);
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
    a.Add(1); // 0
    a.Add(2); // 1
    a.Add(3); // 2
    a.Add(4); // 3
    a.Add(4); // 4
    a.Add(4); // 5
    a.Add(5); // 6
    a.Add(5); // 7
    a.Add(5); // 8
    a.Add(5); // 9
    a.Add(5); // 10
    a.Add(6); // 11
    a.Add(6); // 12
    a.Add(6); // 13
    a.Add(7); // 14
    a.Add(8); // 15
    // Assert.Equal(-1, a.IndexOf(10));
  }
}
