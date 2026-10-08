using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

  public class Node
  {
    public int value;
    public Node next, prev;

    public Node(int _value) { 
      value = _value; 
      next = null;
      prev = null;
    }
  }

  public class LinkedList2
  {
    public Node head;
    public Node tail;

    public LinkedList2()
    {
      head = null;
      tail = null;
    }

    public void AddInTail(Node _item)
    {
      if (head == null) {
      head = _item;
      head.next = null;
      head.prev = null;
      } else {
      tail.next = _item;
      _item.prev = tail;
      }
      tail = _item;
    }

    public Node Find(int _value)
    {
      for (var cursor = head; cursor != null; cursor = cursor.next) {
        if (cursor.value == _value) {
          return cursor;
        }
      }
      return null;
    }

    public List<Node> FindAll(int _value)
    {
      List<Node> nodes = new List<Node>();
      for (var cursor = head; cursor != null; cursor = cursor.next) {
        if (cursor.value == _value) {
          nodes.Add(cursor);
        }
      }
      return nodes;
    }

    public bool Remove(int _value)
    {
      if (head == null) {
        return false;
      }
      var node = Find(_value);
      if (node == null) {
        return false;
      }
      var dummy = new Node(0);
      dummy.next = head;
      head.prev = dummy;
      dummy.prev = tail;
      tail.next = dummy;
      node.prev.next = node.next;
      node.next.prev = node.prev;
      if (dummy.next == dummy) {
        head = null;
        tail = null;
        return true;
      }
      head = dummy.next;
      tail = dummy.prev;
      head.prev = null;
      tail.next = null;
      return true; // если узел был удалён
    }

    public void RemoveAll(int _value)
    {
      if (head == null) {
        return;
      }
      var dummy = new Node(0);
      dummy.next = head;
      head.prev = dummy;
      dummy.prev = tail;
      tail.next = dummy;
      var last = dummy;
      for (var cursor = head; cursor != dummy; cursor = cursor.next) {
        if (cursor.value != _value) {
          cursor.prev = last;
          last.next = cursor;
          last = cursor;
        }
      }
      if (last == dummy) {
        head = null;
        tail = null;
        return;
      }
      head = dummy.next;
      tail = last;
      head.prev = null;
      tail.next = null;
    }

    public void Clear()
    {
      head = null;
      tail = null;
    }

    public int Count()
    {
      var counter = 0;
      for (var cursor = head; cursor != null; cursor = cursor.next) {
        ++counter;
      }
      return counter;
    }

    public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
    {
      if (head == null) {
        head = _nodeToInsert;
        tail = _nodeToInsert;
        return;
      }
      var dummy = new Node(0);
      dummy.next = head;
      head.prev = dummy;
      dummy.prev = tail;
      tail.next = dummy;
      if (_nodeAfter == null) {
        _nodeAfter = dummy;
      }
      _nodeAfter.next.prev = _nodeToInsert;
      _nodeToInsert.next = _nodeAfter.next;
      _nodeAfter.next = _nodeToInsert;
      _nodeToInsert.prev = _nodeAfter;
      head = dummy.next;
      tail = dummy.prev;
      if (_nodeToInsert.prev == dummy) {
        _nodeToInsert.prev = null;
      }
      if (_nodeToInsert.next == dummy) {
        _nodeToInsert.next = null;
      }
    }

  }
}
