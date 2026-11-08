// Рефлексия
//
// 4-5. Баланс открывающих и закрывающих скобок
// Помню, на одной из школьных олимпиад было похожее задание, но я не смог его
// решить, потому что не владел базовыми АСД и запутался в своём эвристическом
// решении. Тогда я ходил на доп. курсы для подготовки к олимпиадам, и мы там
// даже изучали алгоритмы (сортировку, например), но совсем не трогали никакие
// структуры данных кроме массивов и графов. До сих пор загадка, почему многие
// так восхищаются тем преподавателем. При смене профессии знание АСД обеспечило
// мне примерно 30% успеха входа в IT сразу на уровень миддл+.
//
// В этой задаче я использовал и стек и словарь для типов скобок, только ключи
// в словаре были закрывающими скобками, чтобы понять, что читать из стека.
// Поскольку моё решение допускает появление в строке и других символов, для
// открывающих скобок использовалось множество. Стек, словарь, множество в одной
// программе... В школе такое сделать вообще шансов не было.
//
// 6. Минимальный элемент в стеке за O(1)
// Тут я не догадался. Сразу было понятно, что с одним стеком такое за O(1)
// никак не сделать. Где-то на первой странице поиска была фраза "нужен второй".
// Этого оказалось достаточно, чтобы пазл сложился, и я сделал второй стек
// из минимумов.
//
// 7. Среднее значение в стеке за O(1)
// Вот тут я ошибся, сделал второй стек из средних со сложной формулой пересчёта,
// вместо того, чтобы ещё подумать и использовать сумму. Это как раз тот случай,
// когда с молотком в руках весь крепёж кажется гвоздями. Медленное мышление не
// включается. В итоге - расход памяти O(N).
//
// 8. Постфиксная запись
// Тут я осознанно использовал словарь с лямбдами для разных операций и
// интуитивно задал строгий порядок извлечения аргументов из стека. Всё же
// pop - это команда, меняющая состояние, а их всегда хочется выносить в
// отдельные операторы. Исключение я делаю только для всяких ++ и --, может и
// зря. Теперь понятно, почему по канону команды не должны возвращать значение -
// так не будет соблазна втыкать их во всякие выражения с неопределённым
// порядком вызова.
//
// Самым сложным в этом задании было избежать вложенных if-ов: логика ветвилась,
// и тут либо if-ы (у нас в проде всегда так делают), или state-machine. Я
// реализовал упрощённый вариант второго - через неявную монаду Maybe+, которая
// различает "ошибка - выходим" и "ничего не делай - уже готово".

namespace AlgorithmsDataStructures;

public static class DequeUtils {
  // 4.*
  public static bool IsPalindrom(string s) {
    Deque<char> dq = new Deque<char>();
    foreach (char c in s) {
      dq.AddTail(c);
    }
    for (int i = 0; i < s.Length / 2; ++i) {
      if (dq.RemoveFront() != dq.RemoveTail()) {
        return false;
      }
    }
    return true;
  }
}

// 6.*
class DequeX<T> {
  private T[] storage_;
  private int count_;
  private int head_;
  private int tail_;

  public DequeX(int capacity) {
    storage_ = new T[capacity];
    count_ = 0;
    head_ = capacity / 2;
    tail_ = head_ - 1;
  }

  public void AddFront(T item) {
    if (count_ == storage_.Length) {
      return;
    }
    head_ = head_ > 0 ? head_ - 1 : storage_.Length - 1;
    storage_[head_] = item;
    ++count_;
  }

  public void AddTail(T item) {
    if (count_ == storage_.Length) {
      return;
    }
    tail_ = tail_ < storage_.Length - 1 ? tail_ + 1 : 0;
    storage_[tail_] = item;
    ++count_;
  }

  public T RemoveFront() {
    if (count_ == 0) {
      return default(T);
    }
    T item = storage_[head_];
    head_ = head_ < storage_.Length - 1 ? head_ + 1 : 0;
    --count_;
    return item;
  }

  public T RemoveTail() {
    if (count_ == 0) {
      return default(T);
    }
    T item = storage_[tail_];
    tail_ = tail_ > 0 ? tail_ - 1 : storage_.Length - 1;
    --count_;
    return item;
  }

  public int Size()
  {
    return count_;
  }
}

class StackXInt {
  private Stack<int> data_;
  private Stack<int> mins_;

  public StackXInt() {
    data_ = new Stack<int>();
    mins_ = new Stack<int>();
  }

  public int Size() {
    return data_.Count;
  }

  public int? Minimal() {
    if (Size() == 0) {
      return null;
    }
    return mins_.Peek();
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
    return data_.Pop();
  }

  public void Push(int val) {
    int n = Size();
    data_.Push(val);
    if (n == 0) {
      mins_.Push(val);
      return;
    }
    mins_.Push(val < mins_.Peek() ? val : mins_.Peek());
  }
}

class DequeIntMin {
  private StackXInt head_;
  private StackXInt tail_;

  public DequeIntMin() {
    head_ = new StackXInt();
    tail_ = new StackXInt();
  }

  public void AddFront(int item) {
    head_.Push(item);
  }

  public void AddTail(int item) {
    tail_.Push(item);
  }

  public int? RemoveFront() {
    if (head_.Size() == 0) {
      for (; tail_.Size() > 0; head_.Push(tail_.Pop().Value));
    }
    return head_.Pop();
  }

  public int? RemoveTail() {
    if (tail_.Size() == 0) {
      for (; head_.Size() > 0; tail_.Push(head_.Pop().Value));
    }
    return tail_.Pop();
  }

  public int Size()
  {
    return head_.Size() + tail_.Size();
  }

  // 5.*
  public int? Minimal() {
    int? hm = head_.Minimal();
    int? tm = tail_.Minimal();
    if (hm == null) {
      return tm;
    }
    if (tm == null) {
      return hm;
    }
    return hm < tm ? hm : tm;
  }
}

