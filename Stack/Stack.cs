using System;
using System.Collections.Generic;

// Задание 3:
// ```
// while (stack.size() > 0)
//    stack.pop()
//    stack.pop()
// ```
// Очистит стек. Для стека нечётного размера текущая реализация также
// отработает без сбоев, хотя последний `pop` пытается извлечь элемент из
// пустого стека.

namespace AlgorithmsDataStructures
{

  public class Stack<T>
  {
    public Stack()
    {
      storage_ = new LinkedList<T>();
    } 

    public int Size() 
    {
      return storage_.Count;
    }

    public T Pop()
    {
      if (Size() == 0) {
        return default(T); // null, если стек пустой
      }
      T value = storage_.First.Value;
      storage_.RemoveFirst();
      return value;
    }
  
    public void Push(T val)
    {
      storage_.AddFirst(val);
    }

    public T Peek()
    {
      if (Size() == 0) {
        return default(T); // null, если стек пустой
      }
      return storage_.First.Value;
    }

    private LinkedList<T> storage_;
  }

}
