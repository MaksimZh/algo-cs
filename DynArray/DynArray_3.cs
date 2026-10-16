namespace AlgorithmsDataStructures;

public class DynArrayTest {
    [Fact]
    public void MakeArray() {
      DynArray<int> a = new DynArray<int>();
      a.MakeArray(32);
      Assert.Equal(32, a.array.Length);
      Assert.Equal(0, a.count);
    }

    [Fact]
    public void MakeArrayCopy() {
      DynArray<int> a = new DynArray<int>();
      a.Append(1);
      a.Append(2);
      a.MakeArray(32);
      Assert.Equal(32, a.array.Length);
      Assert.Equal(2, a.count);
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(2, a.GetItem(1));
    }

    [Fact]
    public void GetItem() {
      DynArray<int> a = new DynArray<int>();
      Assert.Throws<Exception>(() => a.GetItem(0));
      a.Append(1);
      Assert.Throws<Exception>(() => a.GetItem(-1));
      Assert.Equal(1, a.GetItem(0));
      Assert.Throws<Exception>(() => a.GetItem(1));
      a.Append(2);
      a.Append(3);
      Assert.Throws<Exception>(() => a.GetItem(-1));
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(2, a.GetItem(1));
      Assert.Equal(3, a.GetItem(2));
      Assert.Throws<Exception>(() => a.GetItem(3));
    }

    [Fact]
    public void AppendExtend() {
      DynArray<int> a = new DynArray<int>();
      for (int i = 0; i < 16; ++i) {
        a.Append(i);
      }
      Assert.Equal(16, a.array.Length);
      Assert.Equal(16, a.count);
      a.Append(16);
      Assert.Equal(32, a.array.Length);
      Assert.Equal(17, a.count);
      for (int i = 17; i < 32; ++i) {
        a.Append(i);
      }
      Assert.Equal(32, a.array.Length);
      Assert.Equal(32, a.count);
      a.Append(32);
      Assert.Equal(64, a.array.Length);
      Assert.Equal(33, a.count);
    }

    [Fact]
    public void Insert() {
      DynArray<int> a = new DynArray<int>();
      Assert.Throws<Exception>(() => a.Insert(42, -1));
      Assert.Throws<Exception>(() => a.Insert(42, 1));
      Assert.Equal(0, a.count);
      a.Insert(1, 0);
      Assert.Equal(1, a.count);
      Assert.Equal(1, a.GetItem(0));
      a.Insert(2, 0);
      Assert.Equal(2, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(1, a.GetItem(1));
      a.Insert(3, 1);
      Assert.Equal(3, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      Assert.Equal(1, a.GetItem(2));
      a.Insert(4, 3);
      Assert.Equal(4, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      Assert.Equal(1, a.GetItem(2));
      Assert.Equal(4, a.GetItem(3));
      Assert.Throws<Exception>(() => a.Insert(42, -1));
      Assert.Throws<Exception>(() => a.Insert(42, 6));
      Assert.Equal(16, a.array.Length);
      for (int i = 5; i < 17; ++i) {
        a.Insert(42, 0);
      }
      Assert.Equal(32, a.array.Length);
    }

    [Fact]
    public void Remove() {
      DynArray<int> a = new DynArray<int>();
      Assert.Throws<Exception>(() => a.Remove(-1));
      Assert.Throws<Exception>(() => a.Remove(0));
      Assert.Equal(0, a.count);
      a.Append(1);
      Assert.Throws<Exception>(() => a.Remove(-1));
      Assert.Throws<Exception>(() => a.Remove(1));
      a.Remove(0);
      Assert.Equal(0, a.count);
      a.Append(1);
      a.Append(2);
      a.Append(3);
      Assert.Throws<Exception>(() => a.Remove(-1));
      Assert.Throws<Exception>(() => a.Remove(3));
      a.Remove(0);
      Assert.Equal(2, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      a.Insert(1, 0);
      a.Remove(1);
      Assert.Equal(2, a.count);
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      a.Insert(2, 1);
      a.Remove(2);
      Assert.Equal(2, a.count);
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(2, a.GetItem(1));
    }

    [Fact]
    public void RemoveShrink() {
      DynArray<int> a = new DynArray<int>();
      for (int i = 0; i < 65; ++i) {
        a.Append(i);
      }
      Assert.Equal(65, a.count);
      Assert.Equal(128, a.array.Length);
      a.Remove(64);
      Assert.Equal(64, a.count);
      Assert.Equal(85, a.array.Length);
      for (int i = 0; i < 64; ++i) {
        Assert.Equal(i, a.GetItem(i));
      }

      for (int i = 63; i > 42; --i) {
        a.Remove(i);
      }
      Assert.Equal(43, a.count);
      Assert.Equal(85, a.array.Length);
      for (int i = 0; i < 43; ++i) {
        Assert.Equal(i, a.GetItem(i));
      }

      a.Remove(42);
      Assert.Equal(42, a.count);
      Assert.Equal(56, a.array.Length);

      for (int i = 41; i > 28; --i) {
        a.Remove(i);
      }
      Assert.Equal(29, a.count);
      Assert.Equal(56, a.array.Length);

      a.Remove(28);
      Assert.Equal(28, a.count);
      Assert.Equal(37, a.array.Length);

      for (int i = 27; i > 18; --i) {
        a.Remove(i);
      }
      Assert.Equal(19, a.count);
      Assert.Equal(37, a.array.Length);

      a.Remove(18);
      Assert.Equal(18, a.count);
      Assert.Equal(24, a.array.Length);

      for (int i = 17; i > 12; --i) {
        a.Remove(i);
      }
      Assert.Equal(13, a.count);
      Assert.Equal(24, a.array.Length);

      a.Remove(12);
      Assert.Equal(12, a.count);
      Assert.Equal(16, a.array.Length);

      for (int i = 11; i >= 0; --i) {
        a.Remove(i);
        Assert.Equal(i, a.count);
        Assert.Equal(16, a.array.Length);
      }
    }

    [Fact]
    public void RemoveShrinkTooMuch() {
      DynArray<int> a = new DynArray<int>();
      a.MakeArray(17);
      a.Append(1);
      a.Remove(0);
      Assert.Equal(0, a.count);
      Assert.Equal(16, a.array.Length);
    }
}

public class DynArrayBTest {
    [Fact]
    public void MakeArray() {
      DynArrayB<int> a = new DynArrayB<int>();
      a.MakeArray(32);
      Assert.Equal(32, a.array.Length);
      Assert.Equal(0, a.count);
    }

    [Fact]
    public void MakeArrayCopy() {
      DynArrayB<int> a = new DynArrayB<int>();
      a.Append(1);
      a.Append(2);
      a.MakeArray(32);
      Assert.Equal(32, a.array.Length);
      Assert.Equal(2, a.count);
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(2, a.GetItem(1));
    }

    [Fact]
    public void GetItem() {
      DynArrayB<int> a = new DynArrayB<int>();
      Assert.Throws<Exception>(() => a.GetItem(0));
      a.Append(1);
      Assert.Throws<Exception>(() => a.GetItem(-1));
      Assert.Equal(1, a.GetItem(0));
      Assert.Throws<Exception>(() => a.GetItem(1));
      a.Append(2);
      a.Append(3);
      Assert.Throws<Exception>(() => a.GetItem(-1));
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(2, a.GetItem(1));
      Assert.Equal(3, a.GetItem(2));
      Assert.Throws<Exception>(() => a.GetItem(3));
    }

    [Fact]
    public void AppendExtend() {
      DynArrayB<int> a = new DynArrayB<int>();
      for (int i = 0; i < 16; ++i) {
        a.Append(i);
      }
      Assert.Equal(16, a.array.Length);
      Assert.Equal(16, a.count);
      a.Append(16);
      Assert.Equal(32, a.array.Length);
      Assert.Equal(17, a.count);
      for (int i = 17; i < 32; ++i) {
        a.Append(i);
      }
      Assert.Equal(32, a.array.Length);
      Assert.Equal(32, a.count);
      a.Append(32);
      Assert.Equal(64, a.array.Length);
      Assert.Equal(33, a.count);
    }

    [Fact]
    public void Insert() {
      DynArrayB<int> a = new DynArrayB<int>();
      Assert.Throws<Exception>(() => a.Insert(42, -1));
      Assert.Throws<Exception>(() => a.Insert(42, 1));
      Assert.Equal(0, a.count);
      a.Insert(1, 0);
      Assert.Equal(1, a.count);
      Assert.Equal(1, a.GetItem(0));
      a.Insert(2, 0);
      Assert.Equal(2, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(1, a.GetItem(1));
      a.Insert(3, 1);
      Assert.Equal(3, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      Assert.Equal(1, a.GetItem(2));
      a.Insert(4, 3);
      Assert.Equal(4, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      Assert.Equal(1, a.GetItem(2));
      Assert.Equal(4, a.GetItem(3));
      Assert.Throws<Exception>(() => a.Insert(42, -1));
      Assert.Throws<Exception>(() => a.Insert(42, 6));
      Assert.Equal(16, a.array.Length);
      for (int i = 5; i < 17; ++i) {
        a.Insert(42, 0);
      }
      Assert.Equal(32, a.array.Length);
    }

    [Fact]
    public void Remove() {
      DynArrayB<int> a = new DynArrayB<int>();
      Assert.Throws<Exception>(() => a.Remove(-1));
      Assert.Throws<Exception>(() => a.Remove(0));
      Assert.Equal(0, a.count);
      a.Append(1);
      Assert.Throws<Exception>(() => a.Remove(-1));
      Assert.Throws<Exception>(() => a.Remove(1));
      a.Remove(0);
      Assert.Equal(0, a.count);
      a.Append(1);
      a.Append(2);
      a.Append(3);
      Assert.Throws<Exception>(() => a.Remove(-1));
      Assert.Throws<Exception>(() => a.Remove(3));
      a.Remove(0);
      Assert.Equal(2, a.count);
      Assert.Equal(2, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      a.Insert(1, 0);
      a.Remove(1);
      Assert.Equal(2, a.count);
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(3, a.GetItem(1));
      a.Insert(2, 1);
      a.Remove(2);
      Assert.Equal(2, a.count);
      Assert.Equal(1, a.GetItem(0));
      Assert.Equal(2, a.GetItem(1));
    }

    [Fact]
    public void RemoveShrink() {
      DynArrayB<int> a = new DynArrayB<int>();
      for (int i = 0; i < 65; ++i) {
        a.Append(i);
      }
      Assert.Equal(65, a.count);
      Assert.Equal(128, a.array.Length);
      a.Remove(64);
      Assert.Equal(64, a.count);
      Assert.Equal(85, a.array.Length);
      for (int i = 0; i < 64; ++i) {
        Assert.Equal(i, a.GetItem(i));
      }

      for (int i = 63; i > 42; --i) {
        a.Remove(i);
      }
      Assert.Equal(43, a.count);
      Assert.Equal(85, a.array.Length);
      for (int i = 0; i < 43; ++i) {
        Assert.Equal(i, a.GetItem(i));
      }

      a.Remove(42);
      Assert.Equal(42, a.count);
      Assert.Equal(56, a.array.Length);

      for (int i = 41; i > 28; --i) {
        a.Remove(i);
      }
      Assert.Equal(29, a.count);
      Assert.Equal(56, a.array.Length);

      a.Remove(28);
      Assert.Equal(28, a.count);
      Assert.Equal(37, a.array.Length);

      for (int i = 27; i > 18; --i) {
        a.Remove(i);
      }
      Assert.Equal(19, a.count);
      Assert.Equal(37, a.array.Length);

      a.Remove(18);
      Assert.Equal(18, a.count);
      Assert.Equal(24, a.array.Length);

      for (int i = 17; i > 12; --i) {
        a.Remove(i);
      }
      Assert.Equal(13, a.count);
      Assert.Equal(24, a.array.Length);

      a.Remove(12);
      Assert.Equal(12, a.count);
      Assert.Equal(16, a.array.Length);

      for (int i = 11; i >= 0; --i) {
        a.Remove(i);
        Assert.Equal(i, a.count);
        Assert.Equal(16, a.array.Length);
      }
    }

    [Fact]
    public void RemoveShrinkTooMuch() {
      DynArrayB<int> a = new DynArrayB<int>();
      a.MakeArray(17);
      a.Append(1);
      a.Remove(0);
      Assert.Equal(0, a.count);
      Assert.Equal(16, a.array.Length);
    }
}
