// Рефлексия
//
// 9. Метод, который "переворачивает" связный список
// Здесь я использовал список с одним dummy-узлом, который замыкает его в кольцо
// Можно было бы просто пройти по кольцу и поменять местами prev и next, но
// как тогда найти выход из цикла?
// Так что, пришлось классическим способом пробегать список и потом отдельно
// менять местами head и tail, которые у меня next и prev у dummy.
//
// 10. Проверка, имеются ли циклы внутри списка.
// Цикл for по элементам до длины списка, и если конечным узлом не будет хвост,
// значит в списке есть цикл.
// Я до этого не додумался, использовал множество посещённых элементов и
// проверял не упрёмся ли мы в него снова. А почему? А потому, что не хранил
// длину списка. В моём списке при наличии циклов её вообще нельзя вычислить.
// Циклов, конечно, быть не должно, но как их запретишь, если все узлы торчат
// наружу, и их можно поменять вне списка?
//
// 11. Сортировка списка.
// Внезапно, пузырьковая сортировка - это не упражнение для школьников, а
// вполне жизнеспособный вариант для некоторых структур данных.
// Я, правда, заморочился, и сделал сортировку за O(N log N) методом быстрого
// слияния. Это один из немногих алгоритмов сортировки помимо пузырька,
// которому не требуется доступ к элементу по индексу. Он также пробегает весь
// список последовательно, только log(N), а не N раз.
//
// 12. Слияние списков.
// Я делал слияние двух списков. При сортировке потом оно пригодилось.
// Идея с кучей для слияния отсортированных списков очень интересная.
// Для двух списков разницы нет, но для M списков сложность поиска минимума в
// куче будет O(1), а добавления следующего элемента взамен взятого: O(log M).
// Обычный поиск при этом требует O(M) операций.
//
// 13. Dummy.
// Я использовал как раз круговой список с одним dummy-узлом.
// Вместо сравнения типов применялось сравнение ссылок. Это позволяет обойтись
// без модификации Node или создания его потомков. Так и не понял чем сравнение
// типов лучше, чем сравнение ссылок. Если только перейти к функциональному
// стилю, где в основном работают с самими значениями, а не их идентичностью
// (ссылками, указателями).

namespace AlgorithmsDataStructures;

public static class StackUtils {
  private static HashSet<char> left_parens =
    new HashSet<char>() {'(', '[', '{'};
  private static Dictionary<char, char> right_parens =
    new Dictionary<char, char> {
      {')', '('},
      {']', '['},
      {'}', '{'},
    };

  public static bool AreBalancedParentheses(string text) {
    Stack<char> stack = new Stack<char>();
    foreach (char c in text) {
      if (left_parens.Contains(c)) {
        stack.Push(c);
        continue;
      }
      if (!right_parens.ContainsKey(c)) {
        continue;
      }
      if (stack.Size() == 0) {
        return false;
      }
      if (stack.Pop() != right_parens[c]) {
        return false;
      }
    }
    return stack.Size() == 0;
  }
}

public class StackXInt {
  private Stack<int> data_;
  private Stack<int> mins_;
  private Stack<int> avgs_;

  public StackXInt() {
    data_ = new Stack<int>();
    mins_ = new Stack<int>();
    avgs_ = new Stack<int>();
  }

  public int Size() {
    return data_.Size();
  }

  public int? Minimal() {
    if (Size() == 0) {
      return null;
    }
    return mins_.Peek();
  }

  public float? Average() {
    if (Size() == 0) {
      return null;
    }
    return avgs_.Peek();
  }

  public int? Peek() {
    if (Size() == 0) {
      return null;
    }
    return data_.Peek();
  }

  public int? Pop() {
    if (Size() == 0) {
      return null;
    }
    mins_.Pop();
    avgs_.Pop();
    return data_.Pop();
  }

  public void Push(int val) {
    int n = Size();
    data_.Push(val);
    if (n == 0) {
      mins_.Push(val);
      avgs_.Push(val);
      return;
    }
    mins_.Push(val < mins_.Peek() ? val : mins_.Peek());
    avgs_.Push((avgs_.Peek() * n + val) / (n + 1));
  }
}
