namespace AlgorithmsDataStructures;

public class OrderedListTest
{
  [Fact]
  public void TestIntAsc() {
    OrderedList<int> list = new OrderedList<int>(true);
    Assert.Null(list.head);
    Assert.Null(list.tail);
    Assert.Equal(0, list.Count());
    Assert.Null(list.Find(42));
  }
}
