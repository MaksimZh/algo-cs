using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
  public class Queue<T>
  {
    private LinkedList<T> storage_;

    public Queue()
    {
      storage_ = new LinkedList<T>();
    } 

    // Сложность O(1), потому что используется 2-связный список
    public void Enqueue(T item)
    {
      storage_.AddLast(item);
    }

    // Сложность O(1), потому что используется 2-связный список
    public T Dequeue()
    {
      if (storage_.Count == 0) {
        return default(T); // если очередь пустая
      }
      T item = storage_.First.Value;
      storage_.RemoveFirst();
      return item;
    }

    public int Size()
    {
      return storage_.Count; // размер очереди
    }

  }
}
