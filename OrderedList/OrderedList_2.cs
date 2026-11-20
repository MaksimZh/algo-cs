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

using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
  public class NodeX<T>
  {
    public T value;
    public NodeX<T> next, prev;
    public NodeX<T> parent, child;
    public int shift;

    public NodeX(T _value)
    {
      value = _value;
      next = null;
      prev = null;
      parent = null;
      child = null;
      shift = 0;
    }

    public virtual NodeX<T> Clone() { return new NodeX<T>(value); }
  }

  public class Sentinel<T> : NodeX<T> {
    public Sentinel() : base(default(T)) {}
    public override NodeX<T> Clone() { return new Sentinel<T>(); }
  }

  public class OrderedListX<T>
  {
    public NodeX<T> head, tail;
    private bool _ascending;
    private Sentinel<T> top_, bottom_;

    public OrderedListX(bool asc)
    {
      Clear(asc);
    }

    public int Compare(T v1, T v2)
    {
      if (typeof(T) == typeof(String)) {
        return String.Compare(v1.ToString(), v2.ToString());
      }
      Comparer<T> comparer = Comparer<T>.Default;
      return comparer.Compare(v1, v2);
    }

    public void Add(T value)
    {
      NodeX<T> prev = FindPosition(value);
      NodeX<T> node = new NodeX<T>(value);
      InsertNode(prev, node);
      node.shift = 1;
      Shift(node, +1);
      FixNodeAdd(node);
      head = bottom_.next;
      tail = bottom_.prev;
    }

    public NodeX<T> Find(T val)
    {
      NodeX<T> prev = FindPosition(val);
      if (prev.next is Sentinel<T> || Compare(prev.next.value, val) != 0) {
        return null;
      }
      return prev.next;
    }

    public void Delete(T val)
    {
      NodeX<T> node = Find(val);
      if (node == null) {
        return;
      }
      DeleteNode(node);
    }

    public void DeleteNode(NodeX<T> node) {
      NodeX<T> prev = node.prev;
      NodeX<T> next = node.next;
      for (; node.parent != null; node = node.parent) {}
      // Console.WriteLine(node.value);
      Shift(node, -1);
      for (; node != null; node = node.child) {
        node.next.shift += node.shift - 1;
        RemoveNode(node);
      }
      FixNodeDrop(prev, next);
      head = bottom_.next == bottom_ ? null : bottom_.next;
      tail = bottom_.prev == bottom_ ? null : bottom_.prev;
    }

    public void Clear(bool asc)
    {
      _ascending = asc;
      head = null;
      tail = null;
      top_ = new Sentinel<T>();
      top_.prev = top_;
      top_.next = top_;
      top_.shift = 1;
      bottom_ = top_;
    }

    public int Count()
    {
      return top_.shift - 1;
    }

    public List<NodeX<T>> GetAll() // выдать все элементы упорядоченного 
                                  // списка в виде стандартного списка
    {
      List<NodeX<T>> r = new List<NodeX<T>>();
      for (NodeX<T> node = bottom_.next;
           !(node is Sentinel<T>);
           node = node.next) {
        r.Add(node);
      }
      return r;
    }

    public bool Ordered(T left, T right) {
      int s = Compare(left, right);
      return _ascending ? s <= 0 : s >= 0;
    }

    public NodeX<T> FindPosition(T val, NodeX<T> start = null) {
      NodeX<T> n = start == null ? top_ : start;
      for (;
           !(n.next is Sentinel<T>) && !Ordered(val, n.next.value);
           n = n.next) {}
      if (n.child == null) {
        return n;
      }
      return FindPosition(val, n.child);
    }

    public void InsertNode(NodeX<T> prev, NodeX<T> node) {
      prev.next.prev = node;
      node.next = prev.next;
      node.prev = prev;
      prev.next = node;
    }

    public void FixNodeAdd(NodeX<T> node) {
      // Console.WriteLine($"---Fix({node.value})---");
      int left_count = 0;
      NodeX<T> left = node;
      for (;
           !(left is Sentinel<T>) && left.parent == null;
           left = left.prev) {
        ++left_count;
      }
      int right_count = 0;
      NodeX<T> right = node;
      for (;
           !(right is Sentinel<T>) && right.parent == null;
           right = right.next) {
        ++right_count;
      }
      if (left.parent == null) {
        top_ = NewParent(top_);
        top_.next = top_;
        top_.prev = top_;
        top_.shift = SumShift(left.next, right);
      }
      // Console.Write($"{left_count}<->{right_count}");
      if (left_count + right_count < 4) {
        // Console.WriteLine(" -");
        return;
      }
      NodeX<T> middle = left.next.next;
      // Console.WriteLine($" raise({middle.value})");
      NodeX<T> parent = NewParent(middle);
      InsertNode(left.parent, parent);
      parent.shift = SumShift(left.next, middle);
      right.parent.shift = SumShift(middle.next, right);
      FixNodeAdd(parent);
    }

    public void FixNodeDrop(NodeX<T> prev, NodeX<T> next) {
      // Console.WriteLine($"---FixDrop({prev.value}, {next.value})---");
      if (prev.next != next) {
        return;
      }
      if (prev == next) {
        // Both top sentinel
        FixTop();
        return;
      }
      if (prev.parent == null) {
        FixNodeAdd(prev);
        FixTop();
        return;
      }
      if (next.parent == null) {
        FixNodeAdd(next);
        FixTop();
        return;
      }
      if (prev is Sentinel<T> || prev.parent.parent != null) {
        // Console.WriteLine($"{prev.value} [{next.value}]");
        NodeX<T> right_parent = next.parent.next;
        right_parent.shift += next.parent.shift;
        RemoveNode(next.parent);
        next.parent = null;
        FixNodeAdd(next);
        FixNodeDrop(prev.parent, right_parent);
        return;
      }
      // Console.WriteLine($"[{prev.value}] {next.value}");
      NodeX<T> left_parent = prev.parent.prev;
      prev.parent.next.shift += prev.parent.shift;
      RemoveNode(prev.parent);
      prev.parent = null;
      FixNodeAdd(prev);
      FixNodeDrop(left_parent, next.parent);
    }

    public void FixTop() {
      if (top_.child == null) {
        return;
      }
      Sentinel<T> child = (Sentinel<T>)top_.child;
      if (child.next != child) {
        return;
      }
      child.parent = null;
      top_ = child;
    }

    public static int SumShift(NodeX<T> first, NodeX<T> last) {
      int shift = last.shift;
      for (NodeX<T> n = first; n != last; n = n.next) {
        shift += n.shift;
      }
      return shift;
    }

    public void RemoveNode(NodeX<T> node) {
      node.prev.next = node.next;
      node.next.prev = node.prev;
    }

    public void Shift(NodeX<T> node, int delta) {
      // Console.Write($"shift({node.value}):");
      for (;
           !(node is Sentinel<T>) && node.parent == null;
           node = node.next) {}
      node = node.parent;
      if (node == null) {
        // Console.WriteLine($" -");
        return;
      }
      // Console.Write($" -> {node.value} ({node.shift}");
      node.shift += delta;
      // Console.WriteLine($":{node.shift})");
      Shift(node, delta);
    }

    static public NodeX<T> NewChild(NodeX<T> node) {
      NodeX<T> child = node.Clone();
      node.child = child;
      child.parent = node;
      return child;
    }

    static public U NewParent<U>(U node) where U : NodeX<T> {
      U parent = (U)node.Clone();
      parent.child = node;
      node.parent = parent;
      return parent;
    }

    // 8.*
    public void RemoveDuplicates() {
      if (head == null) {
        return;
      }
      for (NodeX<T> current = head;
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
      NodeX<T> a = bottom_.next;
      NodeX<T> b = other.bottom_.next;

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
      NodeX<T> a = top_;
      for (NodeX<T> b = other.head; !(b is Sentinel<T>); b = b.next) {
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
      for (NodeX<T> current = head;
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

    private NodeX<T> Scan(NodeX<T> start, T val) {
      NodeX<T> current = start;
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

    private int ScanIndex(int count, NodeX<T> start, T val) {
      // Console.WriteLine($"---Scan({count}, {start.value}, {val})---");
      NodeX<T> current = start;
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
}
