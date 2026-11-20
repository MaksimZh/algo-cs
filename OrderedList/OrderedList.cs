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
      Node<T> next = head;
      for (; next != null; next = next.next) {
        int comparison = Compare(value, next.value);
        if (_ascending ? comparison <= 0 : comparison >= 0) {
          break;
        }
      }
      Node<T> node = new Node<T>(value);
      node.prev = next == null ? tail : next.prev;
      node.next = next;
      if (node.prev == null) {
        head = node;
      } else {
        node.prev.next = node;
      }
      if (next == null) {
        tail = node;
      } else {
        next.prev = node;
      }
    }

    public Node<T> Find(T val)
    {
      for (Node<T> node = head; node != null; node = node.next) {
        int comparison = Compare(node.value, val);
        if (comparison == 0) {
          return node;
        }
        if (_ascending ? comparison > 0 : comparison < 0) {
          return null;
        }
      }
      return null;
    }

    public void Delete(T val)
    {
      Node<T> node = Find(val);
      if (node == null) {
        return;
      }
      if (node.prev == null) {
        head = node.next;
      } else {
        node.prev.next = node.next;
      }
      if (node.next == null) {
        tail = node.prev;
      } else {
        node.next.prev = node.prev;
      }
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
