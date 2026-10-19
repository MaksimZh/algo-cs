using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
  public class DynArray<T>
  {
    public T [] array;
    public int count;
    public int capacity;

    private const int min_capacity = 16;
    private const double min_fill_factor = 0.5;
    private const int extend_factor = 2;
    private const double shrink_divisor = 1.5;

    public DynArray()
    {
      count = 0;
      MakeArray(min_capacity);
    }

    public void MakeArray(int new_capacity)
    {
      if (new_capacity < count) {
        throw new Exception("Capacity is too small");
      }
      T[] dest = new T[new_capacity];
      for (int i = 0; i < count; ++i) {
        dest[i] = array[i];
      }
      array = dest;
      capacity = new_capacity;
    }

    public T GetItem(int index)
    {
      if (index < 0 || index >= count) {
        throw new Exception("Index out of range");
      }
      return array[index];
    }

    public void Append(T itm)
    {
      if (count == capacity) {
        Extend();
      }
      array[count++] = itm;
    }

    // O(N)
    public void Insert(T itm, int index)
    {
      if (index < 0 || index > count) {
        throw new Exception("Index out of range");
      }
      if (count == capacity) {
        Extend();
      }
      for (int i = count; i > index; --i) {  // loop => O(N)
        array[i] = array[i - 1];
      }
      array[index] = itm;
      ++count;
    }

    // O(N)
    public void Remove(int index)
    {
      if (index < 0 || index >= count) {
        throw new Exception("Index out of range");
      }
      for (int i = index; i < count - 1; ++i) {  // loop => O(N)
        array[i] = array[i + 1];
      }
      --count;
      MaybeShrink();
    }

    private void Extend() {
      MakeArray(capacity * extend_factor);
    }

    private void MaybeShrink() {
      if (capacity * min_fill_factor < count) {
        return;
      }
      int new_capacity = (int)(capacity / 1.5);
      if (new_capacity < min_capacity) {
        new_capacity = min_capacity;
      }
      if (new_capacity == capacity) {
        return;
      }
      MakeArray(new_capacity);
    }
  }
}
