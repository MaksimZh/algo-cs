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

    public void Inverse() {
      for (var cursor = dummy.next; cursor != dummy;
           // prev is the former next
           cursor = cursor.prev) {
        var tmp = cursor.next;
        cursor.next = cursor.prev;
        cursor.prev = tmp;
      }
      var tmp2 = dummy.next;
      dummy.next = dummy.prev;
      dummy.prev = tmp2;
    }

    public bool HasLoops() {
      var visited = new HashSet<Node>();
      for (var cursor = dummy.next; cursor != dummy; cursor = cursor.next) {
        if (visited.Contains(cursor)) {
          return true;
        }
        visited.Add(cursor);
      }
      return false;
    }

    public void Sort() {
      for (int chunk_size = 1; ; chunk_size *= 2) {
        if (!MergeSortStep(chunk_size)) {
          break;
        }
      }
    }

    static public LinkedList2X Merge(LinkedList2X x, LinkedList2X y) {
      x.Sort();
      y.Sort();
      var z = new LinkedList2X();
      z.dummy.prev = MergeSorted(z.dummy,
                                 x.dummy.next, x.dummy,
                                 y.dummy.next, y.dummy);
      z.dummy.prev.next = z.dummy;
      return z;
    }

    private bool MergeSortStep(int chunk_size) {
      var chunks = new Chunks(chunk_size, dummy.next, dummy);
      if (chunks.mid == dummy) {
        return false;
      }
      var tail = dummy;
      for (; chunks.start != dummy;
           chunks = new Chunks(chunk_size, chunks.stop, dummy)) {
        var buffer = new Node(-1);
        chunks.mid.prev.next = buffer;
        buffer.prev = chunks.mid.prev;
        chunks.mid.prev = buffer;
        buffer.next = chunks.mid;
        tail = MergeSorted(tail,
                           chunks.start, buffer,
                           chunks.mid, chunks.stop);
      }
      dummy.prev = tail;
      tail.next = dummy;
      return true;
    }

    private static Node MergeSorted(Node tail,
                                    Node left, Node left_stop,
                                    Node right, Node right_stop) {
      while (left != left_stop && right != right_stop) {
        (tail, left) = LoadLE(right.value, tail, left, left_stop);
        if (left == left_stop) {
          break;
        }
        (tail, right) = LoadLE(left.value, tail, right, right_stop);
      }
      if (left != left_stop) {
        tail.next = left;
        left.prev = tail;
        tail = left_stop.prev;
      }
      if (right != right_stop) {
        tail.next = right;
        right.prev = tail;
        tail = right_stop.prev;
      }
      return tail;
    }

    private static (Node, Node) LoadLE(int cap, Node tail,
                                       Node source, Node stop) {
      var cursor = source;
      for (; cursor != stop && cursor.value <= cap; cursor = cursor.next) {}
      if (cursor == source) {
        return (tail, source);
      }
      tail.next = source;
      source.prev = tail;
      return (cursor.prev, cursor);
    }
  }

  class Chunks {
    public Node start;
    public Node mid;
    public Node stop;

    public Chunks(int size, Node _start, Node _stop) {
      start = _start;
      mid = start;
      for (int i = 0; mid != _stop && i < size; ++i) {
        mid = mid.next;
      }
      stop = mid;
      for (int i = 0; stop != _stop && i < size; ++i) {
        stop = stop.next;
      }
    }
  }
}
