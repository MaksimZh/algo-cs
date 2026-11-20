using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
  public class Node<T>
  {
    public T value;
    public Node<T> next, prev;

    public Node(T _value)
    {
      value = _value;
      next = null;
      prev = null;
    }
  }

  public class OrderedList<T>
  {
    public Node<T> head, tail;
    private bool _ascending;

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
      if (head == null) {
        InsertHead(node);
        return;
      }
      Node<T> previous = FindPosition(value);
      if (previous == null) {
        InsertHead(node);
        return;
      }
      InsertAfter(previous, node);
    }

    public Node<T> Find(T val)
    {
      Node<T> previous = FindPosition(val);
      Node<T> current = previous == null ? head : previous.next;
      if (current == null || Compare(current.value, val) != 0) {
        return null;
      }
      return current;
    }

    public bool Ordered(T left, T right)
    {
      int comparison = Compare(left, right);
      return _ascending ? comparison <= 0 : comparison >= 0;
    }

    public void Delete(T val)
    {
      Node<T> node = Find(val);
      if (node == null) {
        return;
      }
      if (node.prev == null) {
        RemoveHead();
        return;
      }
      if (node.next == null) {
        RemoveTail();
        return;
      }
      RemoveMiddle(node);
    }

    private Node<T> FindPosition(T value)
    {
      Node<T> previous = null;
      for (Node<T> current = head;
           current != null && !Ordered(value, current.value);
           current = current.next) {
        previous = current;
      }
      return previous;
    }

    private void InsertAfter(Node<T> previous, Node<T> node)
    {
      node.prev = previous;
      node.next = previous.next;
      previous.next = node;
      if (node.next == null) {
        tail = node;
        return;
      }
      node.next.prev = node;
    }

    private void InsertHead(Node<T> node)
    {
      node.prev = null;
      node.next = head;
      head = node;
      if (node.next == null) {
        tail = node;
        return;
      }
      node.next.prev = node;
    }

    private void RemoveMiddle(Node<T> node)
    {
      node.prev.next = node.next;
      node.next.prev = node.prev;
    }

    private void RemoveHead()
    {
      head = head.next;
      if (head == null) {
        tail = null;
        return;
      }
      head.prev = null;
    }

    private void RemoveTail()
    {
      tail = tail.prev;
      if (tail == null) {
        head = null;
        return;
      }
      tail.next = null;
    }

    public void Clear(bool asc)
    {
      _ascending = asc;
      head = null;
      tail = null;
    }

    public int Count()
    {
      int count = 0;
      for (Node<T> node = head; node != null; node = node.next) {
        ++count;
      }
      return count;
    }

    public List<Node<T>> GetAll()
    {
      List<Node<T>> result = new List<Node<T>>();
      for (Node<T> node = head; node != null; node = node.next) {
        result.Add(node);
      }
      return result;
    }
  }
}
