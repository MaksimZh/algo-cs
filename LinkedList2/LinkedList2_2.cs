using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

  // Node уже определён в этом неймспейсе

  public class LinkedList2X
  {
    public Node dummy;

    public LinkedList2X()
    {
      dummy = new Node(0);
      dummy.prev = dummy;
      dummy.next = dummy;
    }

    public Node Head() {
      if (dummy.next == dummy) {
        return null;
      }
      return dummy.next;
    }

    public Node Tail() {
      if (dummy.prev == dummy) {
        return null;
      }
      return dummy.prev;
    }

    public void AddInTail(Node _item)
    {
      var tail = dummy.prev;
      tail.next = _item;
      _item.prev = tail;
      _item.next = dummy;
      dummy.prev = _item;
    }

    public Node Find(int _value)
    {
      for (var cursor = dummy.next; cursor != dummy; cursor = cursor.next) {
        if (cursor.value == _value) {
          return cursor;
        }
      }
      return null;
    }

    public List<Node> FindAll(int _value)
    {
      List<Node> nodes = new List<Node>();
      for (var cursor = dummy.next; cursor != dummy; cursor = cursor.next) {
        if (cursor.value == _value) {
          nodes.Add(cursor);
        }
      }
      return nodes;
    }

    public bool Remove(int _value)
    {
      var node = Find(_value);
      if (node == null) {
        return false;
      }
      node.prev.next = node.next;
      node.next.prev = node.prev;
      return true; // если узел был удалён
    }

    public void RemoveAll(int _value)
    {
      var last = dummy;
      for (var cursor = dummy.next; cursor != dummy; cursor = cursor.next) {
        if (cursor.value != _value) {
          cursor.prev = last;
          last.next = cursor;
          last = cursor;
        }
      }
      dummy.prev = last;
      last.next = dummy;
    }

    public void Clear()
    {
      dummy.next = dummy;
      dummy.prev = dummy;
    }

    public int Count()
    {
      var counter = 0;
      for (var cursor = dummy.next; cursor != dummy; cursor = cursor.next) {
        ++counter;
      }
      return counter;
    }

    public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
    {
      if (_nodeAfter == null) {
        _nodeAfter = dummy;
      }
      _nodeAfter.next.prev = _nodeToInsert;
      _nodeToInsert.next = _nodeAfter.next;
      _nodeAfter.next = _nodeToInsert;
      _nodeToInsert.prev = _nodeAfter;
    }
  }
}
