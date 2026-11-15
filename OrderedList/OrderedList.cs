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
  }

  public class OrderedList<T>
  {
    public Node<T> head, tail;  // Bottom full list to fit tests if any
    private bool _ascending;
    private Node<T> head_, tail_;  // Top level

    public OrderedList(bool asc)
    {
      head = null;
      tail = null;
      _ascending = asc;
      head_ = null;
      tail_ = null;
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
      if (head_ == null) {
        head = node;
        tail = node;
        head_ = node;
        tail_ = node;
        return;
      }
      if (Ordered(node.value, head_.value)) {
        ReplaceHead(node);
        return;
      }
      Node<T> current = FindPosition(node.value);
      if (current.next == null) {
        ReplaceTail(node);
        return;
      }
      Node<T> next = current.next;
      current.next = node;
      node.next = next;
      FixNodeTree(node);
    }

    public Node<T> Find(T val)
    {
      if (head == null) {
        return null;
      }
      Node<T> prev = FindPosition(val);
      if (prev.next != null && Compare(prev.next.value, val) == 0) {
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
      if (node == head) {
        DeleteHead();
        return;
      }
      if (node == tail) {
        DeleteTail();
        return;
      }
    }

    public void Clear(bool asc)
    {
      _ascending = asc;
      head = null;
      tail = null;
      head_ = null;
      tail_ = null;
    }

    public int Count()
    {
      int count = 0;
      for (Node<T> current = head; current != null; current = current.next) {
        ++count;
      }
      return count;
    }

    List<Node<T>> GetAll() // выдать все элементы упорядоченного 
                           // списка в виде стандартного списка
    {
      List<Node<T>> r = new List<Node<T>>();
      Node<T> node = head_;
      while(node != null)
      {
        r.Add(node);
        node = node.next;
      }
      return r;
    }

    private bool Ordered(T left, T right) {
      int s = Compare(left, right);
      return _ascending ? s < 0 : s > 0;
    }

    private Node<T> FindPosition(T val) {
      Node<T> current = head_;
      for (;
           current.next != null;
           current = current.next) {
        if (Ordered(val, current.value)) {
          // We are on the left of the correct position.
          continue;
        }
        if (current.child == null) {
          // We are on the correct position at the bottom level.
          return current;
        }
        // The correct position is below.
        current = current.child;
      }
      // The correct position is the very end.
      for (;
           current.child != null;
           current = current.child) {}
      return current;
    }

    private void FixNodeTree(Node<T> node) {
      Node<T> left = node;
      int left_shift = 0;
      for (;
           left.parent == null && left.prev != null;
           left = left.prev) {
        ++left_shift;
      }
      Node<T> right = node;
      int right_shift = 0;
      for (;
           right.parent == null && right.next != null;
           right = right.next) {
        ++right_shift;
      }
      if (left_shift + right_shift <= 4) {
        return;
      }
      // We have 3 elements in a row without parents.
      // Raise the middle one.
      Node<T> middle = left.next.next;
      Node<T> parent = new Node<T>(node.value);
      parent.child = middle;
      middle.parent = parent; 
      Node<T> left_parent = left.parent;
      Node<T> right_parent = right.parent;
      if (left_parent == null && right_parent == tail_) {
        head_ = new Node<T>(head_.value);
        left_parent = head_;
        tail_ = new Node<T>(tail_.value);
        right_parent = tail_;
      }
      left_parent.child = left;
      left.parent = left_parent;
      left_parent.next = parent;
      parent.prev = left_parent;
      right_parent.child = right;
      right.parent = right_parent;
      right_parent.prev = parent;
      parent.next = right_parent;
      FixNodeTree(parent);
    }

    private void ReplaceHead(Node<T> node) {
      node.next = head;
      head.prev = node;
      head = node;
      Node<T> right = head.parent;
      for (; right != null; right = right.parent) {
        Node<T> parent = new Node<T>(node.value);
        parent.child = node;
        node.parent = parent;
        parent.next = right;
        right.prev = parent;
        node = parent;
      }
      head_ = right;
    }

    private void ReplaceTail(Node<T> node) {
      node.prev = tail;
      tail.next = node;
      tail = node;
      Node<T> left = tail.parent;
      for (; left != null; left = left.parent) {
        Node<T> parent = new Node<T>(node.value);
        parent.child = node;
        node.parent = parent;
        parent.prev = left;
        left.next = parent;
        node = parent;
      }
      tail_ = left;
    }

    private void DeleteHead() {}

    private void DeleteTail() {}
  }
}
