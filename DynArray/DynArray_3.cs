namespace AlgorithmsDataStructures;

public class DynArrayTest {
    [Fact]
    public void MakeArray() {
      DynArray<int> a = new DynArray<int>();
      a.MakeArray(32);
      Assert.Equal(32, a.array.Length);
    }
}
