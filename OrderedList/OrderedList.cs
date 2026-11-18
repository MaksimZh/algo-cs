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

    public virtual Node<T> Clone() {
      return new Node<T>(value);
    }
  }

  class DummyNode<T> : Node<T> {
    public int count;

    public DummyNode() : base(default(T)) {
      count = 0;
    }

    public override DummyNode<T> Clone() {
      return new DummyNode<T>();
    }
  }

  public partial class OrderedList<T>
  {
    public Node<T> head, tail;  // Bottom full list to fit tests if any
    private bool _ascending;
    private DummyNode<T> root_;

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
      Node<T> node = new Node<T>(value);
      (Node<T> dummy, Node<T> current) = FindPosition(node.value);
      InsertNode(dummy, current, node);
      if (head == null) {
        head = node;
        tail = node;
      }
      for (dummy = dummy.child;
           dummy != null;
           dummy = dummy.child) {
        current = current.child;
        node = NewChild(node);
        InsertNode(dummy, current, node);
      }
      if (!(head.prev is DummyNode<T>)) {
        head = head.prev;
      }
      if (!(tail.next is DummyNode<T>)) {
        tail = tail.next;
      }
      FixLists();
    }

    public Node<T> Find(T val)
    {
      (Node<T> dummy, Node<T> current) = FindPosition(val);
      if (Compare(current.value, val) != 0) {
        return null;
      }
      for (;
           current.child != null;
           current = current.child) {}
      return current;
    }

    public void Delete(T val)
    {
      (Node<T> dummy, Node<T> current) = FindPosition(val);
      if (Compare(current.value, val) != 0) {
        return;
      }
      for (;
           current.parent != null;
           current = current.parent) {
        dummy = dummy.parent;
      }
      for (;
           dummy.child != null;
           dummy = dummy.child) {
        RemoveNode(dummy, current);
        current = current.child;
      }
      if (current == head) {
        head = (current.next == dummy) ? null : current.next;
      }
      if (current == tail) {
        tail = (current.prev == dummy) ? null : current.prev;
      }
      RemoveNode(dummy, current);
      FixLists();
    }

    public void Clear(bool asc)
    {
      _ascending = asc;
      head = null;
      tail = null;
      root_ = new DummyNode<T>();
      root_.prev = root_;
      root_.next = root_;
    }

    public int Count()
    {
      if (head == null) {
        return 0;
      }
      return ((DummyNode<T>)head.prev).count;
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

    private void LoadSorted(List<T> source) {
      Clear(_ascending);
      for (int i = 0; i < source.Count; ++i) {
        InsertNode(root_, root_.prev, new Node<T>(source[i]));
      }
      head = root_.next;
      tail = root_.prev;
      FixLists();
    }

    private bool Ordered(T left, T right) {
      int s = Compare(left, right);
      return _ascending ? s <= 0 : s >= 0;
    }

    private (Node<T> dummy, Node<T> prev) FindPosition(T val) {
      if (head == null) {
        return (root_, root_);
      }
      Node<T> dummy = root_;
      for (;
           Ordered(dummy.prev.value, val) && dummy.child != null;
           dummy = dummy.child) {}
      Node<T> current = dummy.prev;
      for (;
           !(current is DummyNode<T>) && !Ordered(current.value, val);
           current = current.prev) {}
      return (dummy, current);
    }

    private void InsertNode(Node<T> dummy, Node<T> prev, Node<T> node) {
      ++((DummyNode<T>)dummy).count;
      prev.next.prev = node;
      node.next = prev.next;
      node.prev = prev;
      prev.next = node;
    }

    private void RemoveNode(Node<T> dummy, Node<T> node) {
      node.prev.next = node.next;
      node.next.prev = node.prev;
      if (node.parent != null) {
        node.parent.child = null;
      }
      if (node.child != null) {
        node.child.parent = null;
      }
      --((DummyNode<T>)dummy).count;
    }

    private void RemoveNodeWithParents(Node<T> dummy, Node<T> node) {
      for (Node<T> parent = node.parent;
           parent != null;
           parent = parent.parent) {
        node = node.parent;
        RemoveNode(dummy, parent.child);
        dummy = dummy.parent;
      }
      RemoveNode(dummy, node);
    }

    private void FixLists() {
      if (head == null) {
        Clear(_ascending);
        return;
      }
      for (Node<T> dummy = head.prev.parent;
           dummy != null;
           dummy = dummy.parent) {
        int count = ((DummyNode<T>)dummy.child).count;
        int subcount = ((DummyNode<T>)dummy).count;
        for (int i = count / 2; i < subcount; ++i) {
          // Mass removal is possible when removing duplicates
          RemoveNode(dummy, dummy.prev);
        }
        for (int i = subcount; i < count / 2; ++i) {
          // Mass addition is possible when loading sorted array
          InsertNode(dummy, dummy.prev, NewParent(dummy.prev.child.next));
        }
      }
      while (root_.count > 1) {
        // Mass addition is possible when loading sorted array
        DummyNode<T> new_root = (DummyNode<T>)NewParent(root_);
        new_root.next = new_root;
        new_root.prev = new_root;
        Node<T> current = root_.next;
        for (int i = 0; i < root_.count / 2; ++i) {
          InsertNode(new_root, new_root.prev, NewParent(current));
          current = current.next;
        }
        root_ = new_root;
      }
      for (;
           root_.count == 0 && root_.child != null;
           root_ = (DummyNode<T>)root_.child) {
        // Mass removal is possible when removing duplicates
      }
      root_.parent = null;
    }

    static private Node<T> NewChild(Node<T> node) {
      Node<T> child = node.Clone();
      node.child = child;
      child.parent = node;
      return child;
    }

    static private Node<T> NewParent(Node<T> node) {
      Node<T> parent = node.Clone();
      parent.child = node;
      node.parent = parent;
      return parent;
    }
  }
}
