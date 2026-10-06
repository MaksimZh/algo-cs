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
      // здесь будет ваш код удаления всех узлов по заданному значению
    }

    public void Clear()
    {
      // здесь будет ваш код очистки всего списка
    }

    public int Count()
    {
      return 0; // здесь будет ваш код подсчёта количества элементов в списке
    }

    public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
    {
      // здесь будет ваш код вставки узла после заданного узла

      // если _nodeAfter = null
      // добавьте новый элемент первым в списке 

    }

  }
}
