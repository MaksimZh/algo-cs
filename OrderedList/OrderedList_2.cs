// Рефлексия
//
// 3. Вращение очереди по кругу на N элементов
// Если рассматривать очередь как АТД (фиксированный интерфейс, непрозрачная
// внутрянка), то да, не остаётся ничего другого, как вытаскивать элементы с
// одного конца, и заталкивать в другой. Если же делать очередь с встроенной
// поддержкой быстрого вращения, то возможны варианты. Например, можно
// использовать процессорные инструкции для быстрого копирования блоков памяти,
// или "переподключать" узлы в списке массивов.
//
// 4. Очередь с помощью двух стеков
// Для односторонней очереди сложность будет в среднем O(1), потому что каждый
// элемент один раз копируется между стеками. Для двусторонней очереди
// копирований может быть и больше, но только для части элементов. В среднем,
// кажется, будет всё то же O(1).
//
// 5. Обращение всех элементов в очереди в обратном порядке
// Для обращения стек - очевидный выбор, сложность которого будет O(N). Для
// очереди на основе массива есть довольно экзотический вариант - поменять
// местами голову и хвост, сложность будет O(1).
//
// 6. Циклическая буферную очередь на базе статического массива фиксированного размера
// Мне не очень нравился процесс перескакивания через границу массива, поэтому,
// чтобы минимизировать его вероятность, я вначале устанавливал голову и хвост
// на середину буфера. Если добавление и удаление имеет примерно одинаковую
// вероятность с обоих концов, то большой шанс, что до границы мы долго не
// доберёмся. Из курса физики, правда, известно, что броуновское движение,
// расталкивает частицы со скоростью квадратный корень из времени, так что
// вероятность упереться в границу ненулевая даже при абсолютно равновероятном,
// хаотическом взаимодействии с очередью.

namespace AlgorithmsDataStructures;

public class OrderedListX<T> : OrderedList<T> {
  public OrderedListX(bool asc) : base(asc) {}

  // 8.*
  public void RemoveDuplicates() {
    if (head == null) {
      return;
    }
    for (Node<T> current = head;
         !(current.next is Sentinel<T>);
         current = current.next) {
      for (;
           !(current.next is Sentinel<T>)
             && Compare(current.value, current.next.value) == 0;
        DeleteNode(current.next)) {}
    }
  }

  // 9.*
  public OrderedListX<T> Merge(OrderedListX<T> other) {
    if (other._ascending != _ascending) {
      return new OrderedListX<T>(_ascending);
    }
    OrderedListX<T> result = new OrderedListX<T>(_ascending);

    // Head may be null. Find the dummy nodes instead.
    Node<T> a = bottom_.next;
    Node<T> b = other.bottom_.next;

    for (int i = 0; i < Count() + other.Count(); ++i) {
      if (b is Sentinel<T> ||
          (!(a is Sentinel<T>) && Ordered(a.value, b.value))) {
        result.Add(a.value);
        a = a.next;
        continue;
      }
      result.Add(b.value);
      b = b.next;
    }

    return result;
  }

  // 10.*
  public bool HasSublist(OrderedListX<T> other) {
    if (other.head == null) {
      // Empty sublist fits everywhere.
      return true;
    }
    if (head == null) {
      // No list - no sublists.
      return false;
    }
    Node<T> a = top_;
    for (Node<T> b = other.head; !(b is Sentinel<T>); b = b.next) {
      a = Scan(a, b.value);
      if (a == null || Compare(a.value, b.value) != 0) {
        return false;
      }
    }
    return true;
  }

  // 11.*
  public T MostFrequent() {
    if (head == null) {
      return default(T);
    }
    (T, int)? most_vf = null;
    (T, int) vf = (head.value, 0);
    for (Node<T> current = head;
         !(current is Sentinel<T>);
         current = current.next) {
      if (Compare(vf.Item1, current.value) == 0) {
        ++vf.Item2;
        continue;
      }
      if (most_vf == null || most_vf.Value.Item2 < vf.Item2) {
        most_vf = vf;
      }
      vf = (current.value, 1);
    }
    return most_vf.Value.Item1;
  }

  // 12.*
  public int IndexOf(T val) {
    return ScanIndex(0, top_, val);
  }

  private Node<T> Scan(Node<T> start, T val) {
    Node<T> current = start;
    for (;
         !(current.next is Sentinel<T>) && !Ordered(val, current.next.value);
         current = current.next) {
      for (; current.parent != null; current = current.parent) {}
    }
    if (!(current.next is Sentinel<T>)
          && Compare(val, current.next.value) == 0) {
      return current.next;
    }
    if (current.child == null) {
      return null;
    }
    return Scan(current.child, val);
  }

  private int ScanIndex(int count, Node<T> start, T val) {
    // Console.WriteLine($"---Scan({count}, {start.value}, {val})---");
    Node<T> current = start;
    for (;
         !(current.next is Sentinel<T>) && !Ordered(val, current.next.value);
         current = current.next) {
      count += current.next.shift;
      // Console.WriteLine($"{current.value}--{current.next.value}: {count}");
    }
    if (!(current.next is Sentinel<T>)
          && Compare(val, current.next.value) == 0) {
      // Console.WriteLine($"{current.next.value}: {count + current.next.shift - 1}");
      return count + current.next.shift - 1;
    }
    if (current.child == null) {
      // Console.WriteLine($"{current.value}");
      return -1;
    }
    return ScanIndex(count, current.child, val);
  }
}
