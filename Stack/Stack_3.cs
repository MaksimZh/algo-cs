namespace AlgorithmsDataStructures;

public class StackTest {
  [Fact]
  public void TestEmpty() {
    Stack<int> s = new Stack<int>();
    Assert.Equal(0, s.Size());
    Assert.Equal(0, s.Peek());
    Assert.Equal(0, s.Pop());
  }

  [Fact]
  public void TestSingle() {
    Stack<int> s = new Stack<int>();
    Assert.Equal(0, s.Size());
    s.Push(42);
    Assert.Equal(1, s.Size());
    Assert.Equal(42, s.Peek());
    Assert.Equal(1, s.Size());
    Assert.Equal(42, s.Pop());
    Assert.Equal(0, s.Size());
  }

  [Fact]
  public void TestMany() {
    Stack<int> s = new Stack<int>();
    Assert.Equal(0, s.Size());
    s.Push(10);
    Assert.Equal(1, s.Size());
    s.Push(20);
    Assert.Equal(2, s.Size());
    s.Push(30);
    Assert.Equal(3, s.Size());
    Assert.Equal(30, s.Peek());
    Assert.Equal(30, s.Pop());
    Assert.Equal(2, s.Size());
    Assert.Equal(20, s.Peek());
    Assert.Equal(20, s.Pop());
    Assert.Equal(1, s.Size());
    Assert.Equal(10, s.Peek());
    Assert.Equal(10, s.Pop());
    Assert.Equal(0, s.Size());
  }
}

public class StackUtilsTest {
  [Fact]
  void TestAreBalancedParentheses() {
    Assert.True(StackUtils.AreBalancedParentheses(""));
    Assert.True(StackUtils.AreBalancedParentheses("()"));
    Assert.True(StackUtils.AreBalancedParentheses("(())"));
    Assert.True(StackUtils.AreBalancedParentheses("(()((())()))"));
    Assert.False(StackUtils.AreBalancedParentheses("("));
    Assert.False(StackUtils.AreBalancedParentheses("))(("));
    Assert.False(StackUtils.AreBalancedParentheses("(()()(()"));

    Assert.True(StackUtils.AreBalancedParentheses("([{}])"));
    Assert.True(StackUtils.AreBalancedParentheses("([{}]){[()]}[()]"));
    Assert.False(StackUtils.AreBalancedParentheses("]})({["));
    Assert.False(StackUtils.AreBalancedParentheses("({[{}]){[()]}[()]"));
  }

  [Fact]
  void TestEvalPostfixEmpty() {
    Stack<string> source = new Stack<string>();
    Assert.Null(StackUtils.EvalPostfix(source));
  }

  [Fact]
  void TestEvalPostfixReady() {
    Stack<string> source = new Stack<string>();
    source.Push("42");
    Assert.Equal(42, StackUtils.EvalPostfix(source));
  }

  [Fact]
  void TestEvalPostfixSum() {
    Stack<string> source = new Stack<string>();
    source.Push("+");
    source.Push("40");
    source.Push("2");
    Assert.Equal(42, StackUtils.EvalPostfix(source));
  }

  [Fact]
  void TestEvalPostfixSumMul() {
    Stack<string> source = new Stack<string>();
    source.Push("*");
    source.Push("3");
    source.Push("+");
    source.Push("2");
    source.Push("1");
    Assert.Equal(9, StackUtils.EvalPostfix(source));
  }

  [Fact]
  void TestEvalPostfixError() {
    Stack<string> source = new Stack<string>();
    source.Push("*");
    source.Push("3");
    source.Push("+");
    source.Push("foo");
    source.Push("1");
    Assert.Null(StackUtils.EvalPostfix(source));
  }
}

public class StackXIntTest {
  [Fact]
  void Test() {
    StackXInt stack = new StackXInt();
    Assert.Equal(0, stack.Size());
    Assert.Null(stack.Minimal());
    Assert.Null(stack.Average());
    Assert.Null(stack.Peek());
    Assert.Null(stack.Pop());
    stack.Push(10);
    Assert.Equal(1, stack.Size());
    Assert.Equal(10, stack.Minimal());
    Assert.Equal(10, stack.Average().GetValueOrDefault(0), 1e-5);
    Assert.Equal(10, stack.Peek());
    stack.Push(40);
    Assert.Equal(2, stack.Size());
    Assert.Equal(10, stack.Minimal());
    Assert.Equal(25, stack.Average().GetValueOrDefault(0), 1e-5);
    Assert.Equal(40, stack.Peek());
    stack.Push(4);
    Assert.Equal(3, stack.Size());
    Assert.Equal(4, stack.Minimal());
    Assert.Equal(18, stack.Average().GetValueOrDefault(0), 1e-5);
    Assert.Equal(4, stack.Peek());
    Assert.Equal(4, stack.Pop());
    Assert.Equal(40, stack.Pop());
    Assert.Equal(10, stack.Pop());
  }
}
