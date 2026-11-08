namespace AlgorithmsDataStructures;

public class DequeTest {
  [Fact]
  public void Test1() {
    Deque<int> q = new Deque<int>();
    Assert.Equal(0, q.Size());
    Assert.Equal(0, q.RemoveFront());
    Assert.Equal(0, q.RemoveTail());
    q.AddFront(1);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.RemoveFront());
    Assert.Equal(0, q.Size());
    q.AddTail(2);
    Assert.Equal(1, q.Size());
    Assert.Equal(2, q.RemoveTail());
    Assert.Equal(0, q.Size());
    q.AddFront(1);
    q.AddTail(2);
    q.AddFront(3);
    q.AddTail(4);
    Assert.Equal(4, q.Size());
    Assert.Equal(3, q.RemoveFront());
    Assert.Equal(3, q.Size());
    Assert.Equal(1, q.RemoveFront());
    Assert.Equal(2, q.Size());
    Assert.Equal(2, q.RemoveFront());
    Assert.Equal(1, q.Size());
    Assert.Equal(4, q.RemoveFront());
    Assert.Equal(0, q.Size());
    q.AddFront(1);
    q.AddTail(2);
    q.AddFront(3);
    q.AddTail(4);
    Assert.Equal(4, q.Size());
    Assert.Equal(4, q.RemoveTail());
    Assert.Equal(3, q.Size());
    Assert.Equal(2, q.RemoveTail());
    Assert.Equal(2, q.Size());
    Assert.Equal(1, q.RemoveTail());
    Assert.Equal(1, q.Size());
    Assert.Equal(3, q.RemoveTail());
    Assert.Equal(0, q.Size());
  }
}

public class DequeUtilsTest {
  [Fact]
  public void TestIsPalindrom() {
    Assert.True(DequeUtils.IsPalindrom(""));
    Assert.True(DequeUtils.IsPalindrom("a"));
    Assert.True(DequeUtils.IsPalindrom("aa"));
    Assert.True(DequeUtils.IsPalindrom("aba"));
    Assert.True(DequeUtils.IsPalindrom("abba"));
    Assert.False(DequeUtils.IsPalindrom("ab"));
    Assert.False(DequeUtils.IsPalindrom("abca"));
  }
}

public class DequeXTest {
  [Fact]
  public void Test1() {
    DequeX<int> q = new DequeX<int>(6);
    Assert.Equal(0, q.Size());
    Assert.Equal(0, q.RemoveFront());
    Assert.Equal(0, q.RemoveTail());
    q.AddFront(1);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.RemoveFront());
    Assert.Equal(0, q.Size());
    q.AddTail(2);
    Assert.Equal(1, q.Size());
    Assert.Equal(2, q.RemoveTail());
    Assert.Equal(0, q.Size());
    q.AddFront(1);
    q.AddTail(2);
    q.AddFront(3);
    q.AddTail(4);
    Assert.Equal(4, q.Size());
    Assert.Equal(3, q.RemoveFront());
    Assert.Equal(3, q.Size());
    Assert.Equal(1, q.RemoveFront());
    Assert.Equal(2, q.Size());
    Assert.Equal(2, q.RemoveFront());
    Assert.Equal(1, q.Size());
    Assert.Equal(4, q.RemoveFront());
    Assert.Equal(0, q.Size());
    q.AddFront(1);
    q.AddTail(2);
    q.AddFront(3);
    q.AddTail(4);
    Assert.Equal(4, q.Size());
    Assert.Equal(4, q.RemoveTail());
    Assert.Equal(3, q.Size());
    Assert.Equal(2, q.RemoveTail());
    Assert.Equal(2, q.Size());
    Assert.Equal(1, q.RemoveTail());
    Assert.Equal(1, q.Size());
    Assert.Equal(3, q.RemoveTail());
    Assert.Equal(0, q.Size());
  }
}


public class DequeIntMinTest {
  [Fact]
  public void Test1() {
    DequeIntMin q = new DequeIntMin();
    Assert.Equal(0, q.Size());
    Assert.Null(q.Minimal());
    Assert.Null(q.RemoveFront());
    Assert.Null(q.RemoveTail());
    q.AddFront(1);
    Assert.Equal(1, q.Size());
    Assert.Equal(1, q.Minimal());
    Assert.Equal(1, q.RemoveFront());
    Assert.Equal(0, q.Size());
    Assert.Null(q.Minimal());
    q.AddTail(2);
    Assert.Equal(1, q.Size());
    Assert.Equal(2, q.Minimal());
    Assert.Equal(2, q.RemoveTail());
    Assert.Equal(0, q.Size());
    Assert.Null(q.Minimal());
    q.AddFront(1);
    q.AddTail(2);
    q.AddFront(3);
    q.AddTail(4);
    Assert.Equal(4, q.Size());
    Assert.Equal(1, q.Minimal());
    Assert.Equal(3, q.RemoveFront());
    Assert.Equal(3, q.Size());
    Assert.Equal(1, q.RemoveFront());
    Assert.Equal(2, q.Size());
    Assert.Equal(2, q.Minimal());
    Assert.Equal(2, q.RemoveFront());
    Assert.Equal(1, q.Size());
    Assert.Equal(4, q.Minimal());
    Assert.Equal(4, q.RemoveFront());
    Assert.Equal(0, q.Size());
    Assert.Null(q.Minimal());
    q.AddFront(1);
    q.AddTail(2);
    q.AddFront(3);
    q.AddTail(4);
    Assert.Equal(4, q.Size());
    Assert.Equal(1, q.Minimal());
    Assert.Equal(4, q.RemoveTail());
    Assert.Equal(3, q.Size());
    Assert.Equal(2, q.RemoveTail());
    Assert.Equal(2, q.Size());
    Assert.Equal(1, q.RemoveTail());
    Assert.Equal(1, q.Size());
    Assert.Equal(3, q.Minimal());
    Assert.Equal(3, q.RemoveTail());
    Assert.Equal(0, q.Size());
    Assert.Null(q.Minimal());
  }
}
