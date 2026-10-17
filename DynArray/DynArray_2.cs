// Рефлексия: 8. Функция суммирования двух связанных списков.
//
// В рекомендациях предлагается сначала сравнить длины списков.
// Я этого не делал. Если длины не совпадали,
// то вместо суммы возвращался пустой список.
//
// Сравнение длины имеет ту же сложность O(N), что и суммирование.
// Если бы вместо суммирования была какая-то затратная операция, а несовпадение
// длины встречалось достаточно часто, то мой "оптимистичный" подход был бы
// проблемой.
// В случае простой суммы, и если несовпадение длины является исключением,
// предлагаемый "пессимистичный" подход увеличивает сложность вдвое.
//
// Если нам важна длина, то можно хранить её в отдельном поле.
// Это снизит затраты на сравнение длин до O(N).
// На мой взгляд, такого рода кэширование мета-информации о структуре данных
// вполне оправдано: оно не требует больших объёмов памяти и может радикально
// снизить сложность некоторых операций. В эпоху ИИ шансы забыть обновить кэш
// в каком-то из методов минимальны.

namespace AlgorithmsDataStructures;

public class DynArrayB<T>
{
  public T [] array;
  public int count;
  public int capacity;
  public int balance;

  private const int min_capacity = 16;
  private const double min_fill_factor = 0.5;
  private const int extend_factor = 2;
  private const double shrink_divisor = 1.5;
  private const int addition_price = 3;
  private const int deletion_price = 3;
  private const int realloc_price = 1;

  public DynArrayB()
  {
    count = 0;
    MakeArray(min_capacity);
    balance = 0;
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
    balance -= realloc_price * count;
    if (balance < 0) {
      throw new Exception("Banking failure");
    }
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
    balance += addition_price;
  }

  // O(N)
  public void Insert(T itm, int index)
  {
    if (index < 0 || index > count) {
      throw new Exception("Index out of range");
    }
    if (count + 1 == capacity) {
      Extend();
    }
    for (int i = count; i > index; --i) {  // loop => O(N)
      array[i] = array[i - 1];
    }
    array[index] = itm;
    ++count;
    balance += addition_price;
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
    balance += deletion_price;
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

public class NDArray<T> {
  private int[] length;
  private int[] stride;
  private T[] array;

  private int NDims { get { return length.Length; } }

  public NDArray(int[] _length) {
    foreach (int x in _length) {
      if (x < 0) {
        throw new Exception("Invalid length");
      }
    }
    stride = Strides(_length);
    length = _length;
    int size = stride[NDims - 1] * length[NDims - 1]; 
    array = new T[size];
  }

  private void MakeArray(int[] _length) {
    if (_length.Length != NDims) {
      throw new Exception("Invalid length");
    }
    foreach (int x in _length) {
      if (x < 0) {
        throw new Exception("Invalid length");
      }
    }
    int[] _stride = Strides(_length);
    int _size = _stride[NDims - 1] * _length[NDims - 1]; 
    T[] _array = new T[_size];

    int size = stride[NDims - 1] * length[NDims - 1]; 
    int[] indices = new int[NDims];
    Array.Fill(indices, 0);
    int si = 0;
    int di = 0;
    for (int i = 0; i < size; ++i) {
      _array[di] = array[si];
      for (int j = 0; j < NDims; ++j) {
        if (indices[j] == length[j] - 1) {
          si -= stride[j] * indices[j];
          di -= _stride[j] * indices[j];
          indices[j] = 0;
          continue;
        }
        ++indices[j];
        si += stride[j];
        di += _stride[j];
        break;
      }
    }
    array = _array;
    length = _length;
    stride = _stride;
  }

  public int Length(int dim) {
    if (dim < 0 || dim >= NDims) {
      throw new Exception("Dimension out of range");
    }
    return length[dim];
  }

  public T this[params int[] indices] {
    get {
      if (indices.Length != NDims) {
        throw new Exception("Dimension number mismatch");
      }
      return array[Offset(indices)];
    }
    set {
      if (indices.Length != NDims) {
        throw new Exception("Dimension number mismatch");
      }
      bool need_extend = false;
      int[] new_length = new int[NDims];
      for (int i = 0; i < NDims; ++i) {
        if (indices[i] < 0) {
          throw new Exception("Index out of range");
        }
        if (indices[i] < length[i]) {
          continue;
        }
        new_length[i] = indices[i] + 1;
        need_extend = true;
      }
      if (need_extend) {
        MakeArray(new_length);
      }
      array[Offset(indices)] = value;
    }
  }

  static private int[] Strides(int[] sizes) {
    int[] strides = new int[sizes.Length];
    strides[0] = 1;
    for (int i = 1; i < sizes.Length; ++i) {
      strides[i] = sizes[i - 1] * strides[i - 1];
    }
    return strides;
  }

  private int Offset(int[] indices) {
    int offset = 0;
    for (int i = 0; i < NDims; ++i) {
      if (indices[i] < 0 || indices[i] >= length[i]) {
        throw new Exception("Index out of range");
      }
      offset += stride[i] * indices[i];
    }
    return offset;
  }
}
