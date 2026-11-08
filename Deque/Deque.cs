using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

  class Deque<T>
  {
    private LinkedList<T> storage_;

    public Deque()
    {
      storage_ = new LinkedList<T>();
    }

    public void AddFront(T item)
    {
      storage_.AddFirst(item);
    }

    public void AddTail(T item)
    {
      storage_.AddLast(item);
    }

    public T RemoveFront()
    {
      if (storage_.Count == 0) {
        return default(T);
      }
      T item = storage_.First.Value;
      storage_.RemoveFirst();
      return item;
    }

    public T RemoveTail()
    {
      if (storage_.Count == 0) {
        return default(T);
      }
      T item = storage_.Last.Value;
      storage_.RemoveLast();
      return item;
    }

    public int Size()
    {
     return storage_.Count; // размер очереди
    }
  }

}
