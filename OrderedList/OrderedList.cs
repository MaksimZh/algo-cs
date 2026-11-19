using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
  public class Node<T>
  {
    public T value;
    public Node<T> next, prev;
    public Node<T> parent, child;
    public int shift;

    public Node(T _value)
    {
      value = _value;
      next = null;
      prev = null;
      parent = null;
      child = null;
      shift = 0;
    }

    public virtual Node<T> Clone() { return new Node<T>(value); }
    public virtual Node<T> Parent() { return parent; }
    public virtual Node<T> Child() { return child; }
  }

  class Sentinel<T> : Node<T> {
    public Sentinel() : base(default(T)) {}
    public override Sentinel<T> Clone() { return new Sentinel<T>(); }
    public override Sentinel<T> Parent() { return (Sentinel<T>)parent; }
    public override Sentinel<T> Child() { return (Sentinel<T>)child; }
  }

  public partial class OrderedList<T>
  {
    public Node<T> head, tail;  // Bottom full list to fit tests if any
    private bool _ascending;
    private Sentinel<T> top_, bottom_;

    public OrderedList(bool asc)
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
      Node<T> prev = FindPosition(value);
      Node<T> node = new Node<T>(value);
      InsertNode(prev, node);
      node.shift = 1;
      Shift(node, +1);
      FixNodeAdd(node);
      head = bottom_.next;
      tail = bottom_.prev;
    }

    public Node<T> Find(T val)
    {
      Node<T> prev = FindPosition(val);
      if (prev.next is Sentinel<T> || Compare(prev.next.value, val) != 0) {
        return null;
      }
      return prev.next;
    }

    public void Delete(T val)
    {
      Node<T> node = Find(val);
      if (node == null) {
        return;
      }
      DeleteNode(node);
    }

    private void DeleteNode(Node<T> node) {
      Node<T> prev = node.prev;
      Node<T> next = node.next;
      for (; node.parent != null; node = node.parent) {}
      Console.WriteLine(node.value);
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

    public List<Node<T>> GetAll() // выдать все элементы упорядоченного 
                                  // списка в виде стандартного списка
    {
      List<Node<T>> r = new List<Node<T>>();
      for (Node<T> node = bottom_.next;
           !(node is Sentinel<T>);
           node = node.next) {
        r.Add(node);
      }
      return r;
    }

    private bool Ordered(T left, T right) {
      int s = Compare(left, right);
      return _ascending ? s <= 0 : s >= 0;
    }

    private Node<T> FindPosition(T val, Node<T> start = null) {
      Node<T> n = start == null ? top_ : start;
      for (;
           !(n.next is Sentinel<T>) && !Ordered(val, n.next.value);
           n = n.next) {}
      if (n.child == null) {
        return n;
      }
      return FindPosition(val, n.child);
    }

    private void InsertNode(Node<T> prev, Node<T> node) {
      prev.next.prev = node;
      node.next = prev.next;
      node.prev = prev;
      prev.next = node;
    }

    private void FixNodeAdd(Node<T> node) {
      Console.WriteLine($"---Fix({node.value})---");
      int left_count = 0;
      Node<T> left = node;
      for (;
           !(left is Sentinel<T>) && left.parent == null;
           left = left.prev) {
        ++left_count;
      }
      int right_count = 0;
      Node<T> right = node;
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
      Console.Write($"{left_count}<->{right_count}");
      if (left_count + right_count < 4) {
        Console.WriteLine(" -");
        return;
      }
      Node<T> middle = left.next.next;
      Console.WriteLine($" raise({middle.value})");
      Node<T> parent = NewParent(middle);
      InsertNode(left.parent, parent);
      parent.shift = SumShift(left.next, middle);
      right.parent.shift = SumShift(middle.next, right);
      FixNodeAdd(parent);
    }

    private void FixNodeDrop(Node<T> prev, Node<T> next) {
      Console.WriteLine($"---FixDrop({prev.value}, {next.value})---");
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
        Console.WriteLine($"{prev.value} [{next.value}]");
        Node<T> right_parent = next.parent.next;
        right_parent.shift += next.parent.shift;
        RemoveNode(next.parent);
        next.parent = null;
        FixNodeAdd(next);
        FixNodeDrop(prev.parent, right_parent);
        return;
      }
      Console.WriteLine($"[{prev.value}] {next.value}");
      Node<T> left_parent = prev.parent.prev;
      prev.parent.next.shift += prev.parent.shift;
      RemoveNode(prev.parent);
      prev.parent = null;
      FixNodeAdd(prev);
      FixNodeDrop(left_parent, next.parent);
    }

    private void FixTop() {
      if (top_.child == null) {
        return;
      }
      Sentinel<T> child = top_.Child();
      if (child.next != child) {
        return;
      }
      child.parent = null;
      top_ = child;
    }

    private static int SumShift(Node<T> first, Node<T> last) {
      int shift = last.shift;
      for (Node<T> n = first; n != last; n = n.next) {
        shift += n.shift;
      }
      return shift;
    }

    private void RemoveNode(Node<T> node) {
      node.prev.next = node.next;
      node.next.prev = node.prev;
    }

    private void Shift(Node<T> node, int delta) {
      Console.Write($"shift({node.value}):");
      for (;
           !(node is Sentinel<T>) && node.parent == null;
           node = node.next) {}
      node = node.parent;
      if (node == null) {
        Console.WriteLine($" -");
        return;
      }
      Console.Write($" -> {node.value} ({node.shift}");
      node.shift += delta;
      Console.WriteLine($":{node.shift})");
      Shift(node, delta);
    }

    static private Node<T> NewChild(Node<T> node) {
      Node<T> child = node.Clone();
      node.child = child;
      child.parent = node;
      return child;
    }

    static private U NewParent<U>(U node) where U : Node<T> {
      U parent = (U)node.Clone();
      parent.child = node;
      node.parent = parent;
      return parent;
    }

    private void FixAfterRemove(Node<T> pos) {
      
    }
  }
}
