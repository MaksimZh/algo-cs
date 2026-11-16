using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
  public class Node<T>
  {
    public T value;
    public Node<T> next, prev;
    public Node<T> parent, child;

    public Node(T _value)
    {
      value = _value;
      next = null;
      prev = null;
      parent = null;
      child = null;
    }

    public Node<T> Clone() {
      return new Node<T>(value);
    }
  }

  class DummyNode<T> : Node<T> {
    public DummyNode() : base(default(T)) {}
    public DummyNode<T> Clone() {
      return new DummyNode<T>();
    }
  }

  public class OrderedList<T>
  {
    public Node<T> head, tail;  // Bottom full list to fit tests if any
    private bool _ascending;
    private DummyNode<T> dummy_;

    public OrderedList(bool asc)
    {
      head = null;
      tail = null;
      _ascending = asc;
      dummy_ = new DummyNode<T>();
      dummy_.prev = dummy_;
      dummy_.next = dummy_;
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
      Node<T> node = new Node<T>(value);
      Node<T> current = FindPosition(node.value);
      Console.WriteLine(current.value);
      Node<T> next = current.next;
      current.next = node;
      node.prev = current;
      node.next = next;
      next.prev = node;
      if (node.prev is DummyNode<T>) {
        head = node;
      }
      if (node.next is DummyNode<T>) {
        tail = node;
      }
      FixNodeTree(node);
    }

    public Node<T> Find(T val)
    {
      if (head == null) {
        return null;
      }
      Node<T> prev = FindPosition(val);
      if (prev == null && Compare(head.value, val) == 0) {
        return head;
      }
      if (prev != null && prev.next != null &&
          Compare(prev.next.value, val) == 0) {
        return prev.next;
      }
      return null;
    }

    public void Delete(T val)
    {
      Node<T> node = Find(val);
      if (node == null) {
        return;
      }
    }

    public void Clear(bool asc)
    {
      _ascending = asc;
      head = null;
      tail = null;
      dummy_ = new DummyNode<T>();
      dummy_.prev = dummy_;
      dummy_.next = dummy_;
    }

    public int Count()
    {
      if (head == null) {
        return 0;
      }
      int count = 0;
      for (Node<T> current = head;
           !(current is DummyNode<T>);
           current = current.next) {
        ++count;
      }
      return count;
    }

    public List<Node<T>> GetAll() // выдать все элементы упорядоченного 
                                  // списка в виде стандартного списка
    {
      List<Node<T>> r = new List<Node<T>>();
      if (head == null) {
        return r;
      }
      for (Node<T> node = head;
           !(node is DummyNode<T>);
           node = node.next) {
        r.Add(node);
      }
      return r;
    }

    private bool Ordered(T left, T right) {
      int s = Compare(left, right);
      return _ascending ? s <= 0 : s >= 0;
    }

    private Node<T> FindPosition(T val) {
      Console.WriteLine("FindPosition");
      Node<T> current = dummy_;
      while (true) {
        Console.WriteLine(current.value);
        if (!(current.next is DummyNode<T>) &&
            !Ordered(val, current.next.value)) {
          // We are on the left of the correct position.
          current = current.next;
          Console.WriteLine("next");
          continue;
        }
        if (current.child == null) {
          // We are on the correct position at the bottom level.
          return current;
        }
        // The correct position is below.
        current = current.child;
        Console.WriteLine("child");
      }
      return current;
    }

    private void FixNodeTree(Node<T> node) {
      Node<T> left = node;
      int left_shift = 0;
      for (;
           left.parent == null && !(left is DummyNode<T>);
           left = left.prev) {
        ++left_shift;
      }
      Node<T> right = node;
      int right_shift = 0;
      for (;
           right.parent == null && !(right is DummyNode<T>);
           right = right.next) {
        ++right_shift;
      }
      if (left_shift + right_shift < 4) {
        return;
      }
      // There is a group of 5 elements. Raise the middle one.
      if (left.parent == null) {
        DummyNode<T> dummy_parent = dummy_.Clone();
        dummy_parent.child = dummy_;
        dummy_.parent = dummy_parent;
        dummy_parent.prev = dummy_parent;
        dummy_parent.next = dummy_parent;
        dummy_ = dummy_parent;
      }
      Node<T> middle = left.next.next;
      Node<T> parent = middle.Clone();
      parent.child = middle;
      middle.parent = parent;
      Node<T> left_parent = left.parent;
      Node<T> right_parent = right.parent;
      left_parent.next = parent;
      parent.prev = left_parent;
      right_parent.prev = parent;
      parent.next = right_parent;
      FixNodeTree(parent);
    }
  }
}
