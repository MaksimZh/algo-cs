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

  [Fact]
  public void TestIntAsc() {
    OrderedList<int> list = new OrderedList<int>(true);
    Assert.Null(list.head);
    Assert.Null(list.tail);
    Assert.Equal(0, list.Count());
    Assert.Null(list.Find(42));
    Assert.Equal([], list.GetAll());

    list.Add(10);
    Assert.Equal(10, list.head.value);
    Assert.Equal(10, list.tail.value);
    Assert.Equal(1, list.Count());
    Assert.Null(list.Find(1));
    Assert.Equal(10, list.Find(10).value);
    Assert.Null(list.Find(42));
    Assert.Equal([10], ToValues(list.GetAll()));
    Assert.Null(list.Find(10).parent);

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
    Assert.Null(list.Find(10).parent);
    Assert.Null(list.Find(20).parent);

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
    Assert.Null(list.Find(10).parent);
    Assert.Equal(20, list.Find(20).parent.value);
    Assert.Null(list.Find(30).parent);

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
    Assert.Null(list.Find(10).parent);
    Assert.Equal(20, list.Find(20).parent.value);
    Assert.Null(list.Find(30).parent);
    Assert.Null(list.Find(40).parent);

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
    Assert.Null(list.Find(10).parent);
    Assert.Equal(20, list.Find(20).parent.value);
    Assert.Null(list.Find(30).parent);
    Assert.Equal(40, list.Find(40).parent.value);
    Assert.Null(list.Find(50).parent);
  }
}
