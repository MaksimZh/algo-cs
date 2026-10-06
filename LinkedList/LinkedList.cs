using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

  public class Node
  {
    public int value;
    public Node next;
    public Node(int _value) { value = _value; }
  }

  public class LinkedList
  {
    public Node head;
    public Node tail;

    public LinkedList()
    {
      head = null;
      tail = null;
    }

    public void AddInTail(Node _item)
    {
      if (head == null) head = _item;
      else              tail.next = _item;
      tail = _item;
    }

    public Node Find(int _value)
    {
      Node node = head;
      while (node != null)
      {
        if (node.value == _value) return node;
        node = node.next;
      }
      return null;
    }

    public List<Node> FindAll(int _value)
    {
      List<Node> nodes = new List<Node>();
      var cursor = head;
      while (cursor != null) {
        if (cursor.value == _value) {
          nodes.Add(cursor);
        }
        cursor = cursor.next;
      }
      return nodes;
    }

    public bool Remove(int _value)
    {
      // Add node over the head to unify search and removal
      var hat = new Node(0);
      hat.next = head;
      var cursor = hat;
      while (cursor.next != null && cursor.next.value != _value) {
        cursor = cursor.next;
      }
      if (cursor.next == null) {
        return false;
      }
      var next = cursor.next.next;
      cursor.next = next;
      if (cursor == hat) {
        // We have dropped the head
        head = next;
        cursor = null;
      }
      if (next == null) {
        // We have dropped the tail
        // cursor is null if it was the only node (head)
        tail = cursor;
      }
      return true; // если узел был удалён
    }

    public void RemoveAll(int _value)
    {
      // Add node over the head to unify search and removal
      var hat = new Node(0);
      hat.next = head;
      var last = hat;
      var cursor = head;
      while (cursor != null) {
        if (cursor.value != _value) {
          last.next = cursor;
          last = cursor;
          cursor = cursor.next;
          continue;
        }
        last.next = null;
        cursor = cursor.next;
      }
      head = hat.next;
      tail = last;
      if (tail == hat) {
        tail = null;
      }
    }

    public void Clear()
    {
      head = null;
      tail = null;
    }

    public int Count()
    {
      var cursor = head;
      int counter = 0;
      while (cursor != null) {
        cursor = cursor.next;
        ++counter;
      }
      return counter;
    }

    public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
    {
      if (_nodeAfter == null) {
        // Add node over the head to unify insertion
        _nodeAfter = new Node(0);
        _nodeAfter.next = head;
        head = _nodeToInsert;
      }
      _nodeToInsert.next = _nodeAfter.next;
      _nodeAfter.next = _nodeToInsert;
      if (_nodeToInsert.next == null) {
        tail = _nodeToInsert;
      }
    }

  }
}
